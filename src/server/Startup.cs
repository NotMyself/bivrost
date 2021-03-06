﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MediatR;

using Bivrost.Web.Signalr;
using Bivrost.Web.Twitch;

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

      services.AddTwitchClient(Configuration);
      services.AddTwitchBot(Configuration);

      services.AddMediatR(typeof(Startup).Assembly);

      services.AddControllers();
      services.AddSignalR(config =>
      {
        config.EnableDetailedErrors =
          Configuration.GetValue("Signalr_Errors", false);
      });
      services.AddSpaStaticFiles(configuration =>
      {
          configuration.RootPath = Configuration["Client"];
      });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      //set up serverside development handler
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseCors(builder =>
        {
          builder.WithOrigins(
            "http://localhost", // local docker
            "http://localhost:8080", // local docker explicit
            "http://localhost:5000" // local bare metal
            )
              .AllowAnyHeader()
              .WithMethods("GET", "POST")
              .AllowCredentials();
        });


      }

      app.UseRouting();
      app.UseStaticFiles();
      app.UseSpaStaticFiles();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapHub<ClientHub>("/client-hub");
        endpoints.MapControllers();
      });

      app.UseSpa(spa =>
      {
        spa.Options.SourcePath = Configuration["ClientProjectPath"];
      });

    }
  }
}
