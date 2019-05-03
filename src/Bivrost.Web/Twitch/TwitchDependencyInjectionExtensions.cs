using Bivrost.Web.Twitch;
using Microsoft.Extensions.Configuration;
using TwitchLib.Client;
using TwitchLib.Client.Models;

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

      services.AddSingleton<TwitchClient>((c) =>
      {
        var client = new TwitchClient();

        client.Initialize(c.GetService<ConnectionCredentials>(),
                          Configuration["BIVROST_TWITCH_BOT_CHANNEL"]);

        return client;
      });
    }

    public static void AddTwitchBot(this IServiceCollection services,
                                        IConfiguration Configuration)
    {
      services.AddHostedService<Bot>();
    }
  }
}
