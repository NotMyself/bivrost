using MediatR;
using TwitchLib.Api.Helix.Models.Users;
using TwitchLib.Client.Models;

namespace Bivrost.Web.Twitch.Notifications
{
  public class RecievedChatMessageNotification: INotification
  {
    public RecievedChatMessageNotification(ChatMessage message)
    {
      Message = message ?? throw new System.ArgumentNullException(nameof(message));
    }

    public ChatMessage Message { get; }
  }
}
