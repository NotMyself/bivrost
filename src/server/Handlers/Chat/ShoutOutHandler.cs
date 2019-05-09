using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Bivrost.Web.Twitch.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;
using TwitchLib.Client;

namespace Bivrost.Web.Handlers.Chat
{
  public class ShoutOutHandler : INotificationHandler<RecievedChatMessageNotification>
  {
    public TwitchClient Client { get; }

    public ILogger<ShoutOutHandler> Logger { get; }

    public Regex CommandRegex { get; } = new Regex(@"^!so (\w*)\s*?$");

    public ShoutOutHandler(TwitchClient client, ILogger<ShoutOutHandler> logger)
    {
      Client = client ?? throw new System.ArgumentNullException(nameof(client));
      Logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
    }

    public Task Handle(RecievedChatMessageNotification notification, CancellationToken cancellationToken)
    {
      return Task.Run(() =>
      {
        if (Client.IsConnected && notification.Message.Message.StartsWith("!so"))
        {
          var match = CommandRegex.Match(notification.Message.Message);
          if (match.Success)
          {
            Client.SendMessage(notification.Message.Channel,
            $"Please checkout our friend {match.Groups[1].Value}'s stream at https://www.twitch.tv/{match.Groups[1].Value}");
          }
          else
          {
            Client.SendWhisper(notification.Message.Username,
              @"I am sorry, but I did not understand your shoutout command.");
            Client.SendWhisper(notification.Message.Username,
            @"Please use the format: !so username");
          }
        }
        else if (!Client.IsConnected)
        {
          Logger.LogWarning("{@Event}",
          new
          {
            Event = "Shout Out Failed",
            Reason = "Twitch Client Not Connected",
            UserId = notification.Message.UserId,
            DisplayName = notification.Message.DisplayName,
            Message = notification.Message.Message
          });
        }
      });

    }
  }
}
