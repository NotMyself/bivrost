using System;
using System.IO;
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


      services.AddLogging(builder =>
      {
        // we only want to use serilog
        builder.ClearProviders();
        builder.AddSerilog();
      });

      services.AddTwitchClient(Configuration);
      //services.AddTwitchBot(Configuration);

      services.AddMvc();
      // In production, the Vue files will be served
      //  from this directory
      services.AddSpaStaticFiles(configuration =>
      {
          configuration.RootPath = Configuration["Client"];
      });
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions {
            HotModuleReplacement = true,
            ProjectPath = Path.Combine(env.ContentRootPath, Configuration["ClientProjectPath"]),
            ConfigFile = Path.Combine(env.ContentRootPath, Configuration["ClientProjectConfigPath"])
        });
      }

      app.UseStaticFiles();
      app.UseSpaStaticFiles(new StaticFileOptions {

      });
      app.UseDefaultFiles();
      // app.UseMvc(routes =>
      // {
      //     routes.MapRoute(
      //         name: "default",
      //         template: "{controller=Home}/{action=Index}/{id?}");

      //     routes.MapSpaFallbackRoute(
      //         name: "spa-fallback",
      //         defaults: new { controller = "Home", action = "Index" });
      // });
    }
  }
}
