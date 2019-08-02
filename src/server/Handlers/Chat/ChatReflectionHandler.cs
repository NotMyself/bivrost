using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

using MediatR;

using Bivrost.Web.Signalr;
using Bivrost.Web.Twitch;
using Bivrost.Web.Twitch.Notifications;

namespace Bivrost.Web.Handlers.Chat
{
  public class ChatReflectionHandler : INotificationHandler<RecievedChatMessageNotification>
  {
    public UserCache Cache { get; }
    public IHubContext<ClientHub> HubContext { get; }
    public ILogger<ChatReflectionHandler> Logger { get; }
    public ChatReflectionHandler(UserCache cache,
                                            IHubContext<ClientHub> hubContext,
                                            ILogger<ChatReflectionHandler> logger)
    {
      Cache = cache ?? throw new System.ArgumentNullException(nameof(cache));
      HubContext = hubContext ?? throw new System.ArgumentNullException(nameof(hubContext));
      Logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
    }


    public async Task Handle(RecievedChatMessageNotification notification,
                              CancellationToken cancellationToken)
    {
      Logger.LogInformation("{@Event}",
        new { Event = "Execute Handler", Type = nameof(ChatReflectionHandler) });

      // do not reflect chat commands
      if (notification.Message.Message.StartsWith("!"))
        return;

      var user = await Cache.GetUserAsync(notification.Message.UserId);

      try
      {
        await HubContext.Clients.All.SendAsync("ReceiveChatMessage",
          new
          {
            User = new
            {
              user.Id,
              user.DisplayName,
              user.ProfileImageUrl,
              notification.Message.IsBroadcaster,
              notification.Message.IsModerator,
              notification.Message.IsSubscriber,
              notification.Message.Badges,
              notification.Message.Bits
            },
            notification.Message.Id,
            notification.Message.Message,
            notification.Message.EmoteSet.Emotes,
            notification.Message.Bits,
            notification.Message.RoomId
          });
      }
      catch (Exception e)
      {
        Logger.LogError(e, "{@Event}",
        new {
          Event = "Execute Handler Error",
          Type = nameof(ChatReflectionHandler),
          Message = e.Message
        });
      }

    }
  }
}
