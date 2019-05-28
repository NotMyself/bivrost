using Microsoft.Extensions.Configuration;

using TwitchLib.Api;
using TwitchLib.Client;
using TwitchLib.Client.Models;

using Bivrost.Web.Twitch;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class TwitchDependencyInjectionExtensions
  {
    public static void AddTwitchClient(this IServiceCollection services,
                                        IConfiguration Configuration)
    {
      services.AddSingleton<ConnectionCredentials>(c =>
        new ConnectionCredentials(Configuration["BIVROST_TWITCH_BOT_USER_NAME"],
                                  Configuration["BIVROST_TWITCH_BOT_ACCESS_TOKEN"]));

      services.AddSingleton<TwitchClient>(c =>
      {
        var client = new TwitchClient();

        client.Initialize(c.GetService<ConnectionCredentials>(),
                          Configuration["BIVROST_TWITCH_BOT_CHANNEL"]);

        return client;
      });

      services.AddSingleton<TwitchAPI>(c => {
        var api = new TwitchAPI();

        api.Settings.ClientId = Configuration["BIVROST_TWITCH_CLIENT_ID"];
        api.Settings.AccessToken = Configuration["BIVROST_TWITCH_CLIENT_SECRET"];

        return api;
      });
    }

    public static void AddTwitchBot(this IServiceCollection services,
                                        IConfiguration Configuration)
    {
      services.AddHostedService<Bot>();
    }
  }
}
