using System.Threading;
using System.Threading.Tasks;
using Bivrost.Web.Signalr;
using Bivrost.Web.Twitch;
using Bivrost.Web.Twitch.Notifications;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Bivrost.Web.Models.Chat
{
  public class ChatReflectionNotificationHandler : INotificationHandler<RecievedChatMessageNotification>
  {
    public UserCache Cache { get; }
    public IHubContext<ClientHub> HubContext { get; }
    public ILogger<ChatReflectionNotificationHandler> Logger { get; }
    public ChatReflectionNotificationHandler(UserCache cache,
                                            IHubContext<ClientHub> hubContext,
                                            ILogger<ChatReflectionNotificationHandler> logger)
    {
      Cache = cache ?? throw new System.ArgumentNullException(nameof(cache));
      HubContext = hubContext ?? throw new System.ArgumentNullException(nameof(hubContext));
      Logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
    }


    public async Task Handle(RecievedChatMessageNotification notification, CancellationToken cancellationToken)
    {
      var user = await Cache.GetUserAsync(notification.Message.UserId);
      await HubContext.Clients.All.SendAsync("ReceiveChatMessage",
        new { User= new {
                      user.Id,
                      user.DisplayName,
                      user.ProfileImageUrl,
                      user.Type,
                      notification.Message.IsBroadcaster,
                      notification.Message.IsModerator,
                      notification.Message.IsSubscriber,
                      notification.Message.Badges,
                      notification.Message.Bits
                    },
              notification.Message.Id,
              notification.Message.Message,
              notification.Message.RoomId
                });
    }
  }
}
