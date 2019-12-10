using Nyami.AspNetCore.VueCliServices.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.AspNetCore.SpaServices.Prerendering;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Nyami.AspNetCore.VueCliServices.Npm;

namespace Nyami.AspNetCore.VueCliServices
{
    /// <summary>
    /// Provides an implementation of <see cref="ISpaPrerendererBuilder"/> that can build
    /// a Vue application by invoking the Vue CLI.
    /// </summary>
    public class VueCliBuilder : ISpaPrerendererBuilder
    {
        private static TimeSpan RegexMatchTimeout = TimeSpan.FromSeconds(5); // This is a development-time only feature, so a very long timeout is fine

        private readonly string _npmScriptName;

        /// <summary>
        /// Constructs an instance of <see cref="VueCliBuilder"/>.
        /// </summary>
        /// <param name="npmScript">The name of the script in your package.json file that builds the server-side bundle for your Vue application.</param>
        public VueCliBuilder(string npmScript)
        {
            if (string.IsNullOrEmpty(npmScript))
            {
                throw new ArgumentException("Cannot be null or empty.", nameof(npmScript));
            }

            _npmScriptName = npmScript;
        }

        /// <inheritdoc />
        public async Task Build(ISpaBuilder spaBuilder)
        {
            var sourcePath = spaBuilder.Options.SourcePath;
            if (string.IsNullOrEmpty(sourcePath))
            {
                throw new InvalidOperationException($"To use {nameof(VueCliBuilder)}, you must supply a non-empty value for the {nameof(SpaOptions.SourcePath)} property of {nameof(SpaOptions)} when calling {nameof(SpaApplicationBuilderExtensions.UseSpa)}.");
            }

            var logger = LoggerFinder.GetOrCreateLogger(
                spaBuilder.ApplicationBuilder,
                nameof(VueCliBuilder));
            var npmScriptRunner = new NpmScriptRunner(
                sourcePath,
                _npmScriptName,
                "--watch",
                null);
            npmScriptRunner.AttachToLogger(logger);

            using (var stdOutReader = new EventedStreamStringReader(npmScriptRunner.StdOut))
            using (var stdErrReader = new EventedStreamStringReader(npmScriptRunner.StdErr))
            {
                try
                {
                    await npmScriptRunner.StdOut.WaitForMatch(
                        new Regex("Date", RegexOptions.None, RegexMatchTimeout));
                }
                catch (EndOfStreamException ex)
                {
                    throw new InvalidOperationException(
                        $"The NPM script '{_npmScriptName}' exited without indicating success.\n" +
                        $"Output was: {stdOutReader.ReadAsString()}\n" +
                        $"Error output was: {stdErrReader.ReadAsString()}", ex);
                }
                catch (OperationCanceledException ex)
                {
                    throw new InvalidOperationException(
                        $"The NPM script '{_npmScriptName}' timed out without indicating success. " +
                        $"Output was: {stdOutReader.ReadAsString()}\n" +
                        $"Error output was: {stdErrReader.ReadAsString()}", ex);
                }
            }
        }
    }
}
