using System;
using System.IO;
using Bivrost.Web.Signalr;
using Bivrost.Web.Twitch;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Bivrost.Web
{
  public class Startup
  {
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMemoryCache();
      services.AddSingleton<UserCache>();

      services.AddLogging(builder =>
      {
        // we only want to use serilog
        builder.ClearProviders();
        builder.AddSerilog();
      });

      services.AddTwitchClient(Configuration);
      services.AddTwitchBot(Configuration);

      services.AddMediatR(typeof(Startup).Assembly);

      services.AddMvc();
      services.AddSignalR(config =>
      {
          config.EnableDetailedErrors =
            Configuration.GetValue("Signalr_Errors", false);
      });
      // In production, the Vue files will be served
      //  from this directory
      services.AddSpaStaticFiles(configuration =>
      {
          configuration.RootPath = Configuration["Client"];
      });
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      //set up serverside development handler
      if(env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      //set up signalr routing
      app.UseSignalR(routes =>
      {
          routes.MapHub<ClientHub>("/client-hub");
      });

      //set up default mvc routing
      app.UseMvc(routes =>
      {
        routes.MapRoute("default", "api/{controller=Home}/{action=Index}/{id?}");
      });

      //setup spa routing for both dev and prod
      if (env.IsDevelopment())
      {
        app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions {
            HotModuleReplacement = true,
            ProjectPath = Path.Combine(env.ContentRootPath, Configuration["ClientProjectPath"]),
            ConfigFile = Path.Combine(env.ContentRootPath, Configuration["ClientProjectConfigPath"])
        });
      }
      else
      {
        app.UseWhen(context => !context.Request.Path.Value.StartsWith("/api")
          && !context.Request.Path.Value.StartsWith("/client-hub"),
          builder => {
            app.UseSpaStaticFiles();
            app.UseSpa(spa => {
              spa.Options.DefaultPage = "/index.html";
            });

            app.UseMvc(routes => {
              routes.MapSpaFallbackRoute(
                  name: "spa-fallback",
                  defaults: new { controller = "Fallback", action = "Index" });
            });
          });

      }
    }
  }
}
