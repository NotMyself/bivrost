using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using TwitchLib.Client.Events;

namespace Bivrost.Web.Signalr
{

  public class ClientHub : Hub
  {
    public ILogger<ClientHub> Logger { get; }
    public ClientHub(ILogger<ClientHub> logger)
    {
      Logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
    }
    public async Task BroadcastChatMessage(OnMessageReceivedArgs message)
    {
      Logger.LogInformation("{@Event}", new { Event = "SignalR Send", Type = "BroadcastChatMessage" });

      await Clients.All.SendAsync("receiveChatMessage", message);
    }
  }
}
