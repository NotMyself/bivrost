using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using Microsoft.AspNetCore.SpaServices;
using Nyami.AspNetCore.VueCliServices.Util;
using Nyami.AspNetCore.VueCliServices.Npm;
using System.Collections.Generic;

namespace Nyami.AspNetCore.VueCliServices
{
    internal static class VueCliMiddleware
    {
        private const string LogCategoryName = "Nyami.AspNetCore.SpaServices";
        private static TimeSpan RegexMatchTimeout = TimeSpan.FromSeconds(5); // This is a development-time only feature, so a very long timeout is fine

        public static void Attach(
            ISpaBuilder spaBuilder,
            string npmScriptName)
        {
            var sourcePath = spaBuilder.Options.SourcePath;
            if (string.IsNullOrEmpty(sourcePath))
            {
                throw new ArgumentException("Cannot be null or empty", nameof(sourcePath));
            }

            if (string.IsNullOrEmpty(npmScriptName))
            {
                throw new ArgumentException("Cannot be null or empty", nameof(npmScriptName));
            }

            // Start Vue CLI and attach to middleware pipeline
            var appBuilder = spaBuilder.ApplicationBuilder;
            var logger = LoggerFinder.GetOrCreateLogger(appBuilder, LogCategoryName);
            var vueCliServerInfoTask = StartVueCliServerAsync(sourcePath, npmScriptName, logger);

            var targetUriTask = vueCliServerInfoTask.ContinueWith(
                task => new UriBuilder(task.Result.Scheme, task.Result.Host, task.Result.Port).Uri);

            SpaProxyingExtensions.UseProxyToSpaDevelopmentServer(spaBuilder, () =>
            {
                // On each request, we create a separate startup task with its own timeout. That way, even if
                // the first request times out, subsequent requests could still work.
                var timeout = spaBuilder.Options.StartupTimeout;
                return targetUriTask.WithTimeout(timeout,
                    $"The Vue CLI process did not start listening for requests " +
                    $"within the timeout period of {timeout.Seconds} seconds. " +
                    $"Check the log output for error information.");
            });
        }

        private static async Task<VueCliServerInfo> StartVueCliServerAsync(
            string sourcePath, string npmScriptName, ILogger logger)
        {
            var portNumber = 8080;//default port for vue cli: 8080
            if (portNumber < 80)
            {
                portNumber = TcpPortFinder.FindAvailablePort();
            }
            else
            {
                // if the port we want to use is occupied, terminate the process utilizing that port.
                // this occurs when "stop" is used from the debugger and the middleware does not have the opportunity to kill the process
                PidUtils.KillPort((ushort)portNumber);
            }

            logger.LogInformation($"Starting @Vue/cli on port {portNumber}...");

            var envVars = new Dictionary<string, string>
            {
                { "PORT", portNumber.ToString() },
                { "DEV_SERVER_PORT", portNumber.ToString() }, // vue cli 3 uses --port {number}, included below
                { "BROWSER", "none" }, // We don't want vue-cli to open its own extra browser window pointing to the internal dev server port
            };

            var npmScriptRunner = new NpmScriptRunner(
                sourcePath, npmScriptName, $"--port {portNumber}", envVars);
            AppDomain.CurrentDomain.DomainUnload += (s, e) => npmScriptRunner?.Kill();
            AppDomain.CurrentDomain.ProcessExit += (s, e) => npmScriptRunner?.Kill();
            AppDomain.CurrentDomain.UnhandledException += (s, e) => npmScriptRunner?.Kill();
            npmScriptRunner.AttachToLogger(logger);

            Match openBrowserLine;
            using (var stdErrReader = new EventedStreamStringReader(npmScriptRunner.StdErr))
            {
                try
                {
                    openBrowserLine = await npmScriptRunner.StdOut.WaitForMatch(
                         new Regex("  - Local:   (http\\S+)", RegexOptions.None, RegexMatchTimeout));
                }
                catch (EndOfStreamException ex)
                {
                    throw new InvalidOperationException(
                        $"The NPM script '{npmScriptName}' exited without indicating that the " +
                        $"Vue CLI was listening for requests. The error output was: " +
                        $"{stdErrReader.ReadAsString()}", ex);
                }
            }

            var uri = new Uri(openBrowserLine.Groups[1].Value);
            var serverInfo = new VueCliServerInfo { Port = uri.Port, Host = uri.Host, Scheme = uri.Scheme };

            // Even after the Vue CLI claims to be listening for requests, there may be a short
            // period where it will give an error if you make a request too quickly
            await WaitForVueCliServerToAcceptRequests(uri);

            return serverInfo;
        }

        private static async Task WaitForVueCliServerToAcceptRequests(Uri cliServerUri)
        {
            // To determine when it's actually ready, try making HEAD requests to '/'. If it
            // produces any HTTP response (even if it's 404) then it's ready. If it rejects the
            // connection then it's not ready. We keep trying forever because this is dev-mode
            // only, and only a single startup attempt will be made, and there's a further level
            // of timeouts enforced on a per-request basis.
            var timeoutMilliseconds = 1000;
            using (var client = new HttpClient())
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                while (true)
                {
                    try
                    {
                        // If we get any HTTP response, the CLI server is ready
                        await client.SendAsync(
                            new HttpRequestMessage(HttpMethod.Head, cliServerUri),
                            new CancellationTokenSource(timeoutMilliseconds).Token);
                        return;
                    }
                    catch (Exception)
                    {
                        await Task.Delay(500);

                        // Depending on the host's networking configuration, the requests can take a while
                        // to go through, most likely due to the time spent resolving 'localhost'.
                        // Each time we have a failure, allow a bit longer next time (up to a maximum).
                        // This only influences the time until we regard the dev server as 'ready', so it
                        // doesn't affect the runtime perf (even in dev mode) once the first connection is made.
                        // Resolves https://github.com/aspnet/JavaScriptServices/issues/1611
                        if (timeoutMilliseconds < 10000)
                        {
                            timeoutMilliseconds += 3000;
                        }
                    }
                }
            }
        }

        class VueCliServerInfo
        {
            public int Port { get; set; }

            public string Scheme { get; set; }

            public string Host { get; set; }
        }
    }
}
