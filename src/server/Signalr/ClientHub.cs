using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;

using TwitchLib.Client.Events;

namespace Bivrost.Web.Signalr
{

  public interface IClientHub
  {
    Task ReceiveChatMessage(OnMessageReceivedArgs message);
  }
  public class ClientHub : Hub<IClientHub>
  {
    public async Task BroadcastChatMessage(OnMessageReceivedArgs message)
    {
        await Clients.All.ReceiveChatMessage(message);
    }
  }
}
