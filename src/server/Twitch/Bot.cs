using System;
using System.Threading;
using System.Threading.Tasks;
using Bivrost.Web.Signalr;
using Bivrost.Web.Twitch.Notifications;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Communication.Events;

namespace Bivrost.Web.Twitch
{
  public class Bot : IHostedService
  {
    public ILogger<Bot> Logger { get; }
    private TwitchClient Client { get; }
    public IMediator Mediator { get; }
    public IHubContext<ClientHub> HubContext { get; }

    public Bot(TwitchClient client, IMediator mediator, IHubContext<ClientHub> hubContext, ILogger<Bot> logger)
    {
      Client = client ?? throw new System.ArgumentNullException(nameof(client));
      Mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));;
      HubContext = hubContext ?? throw new System.ArgumentNullException(nameof(hubContext));
      Logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
      return Task.Run(() => {
        Client.OnConnected += OnConnected;
        Client.OnDisconnected += OnDisconnected;
        Client.OnError += OnError;
        Client.OnJoinedChannel += OnJoinedChannel;
        Client.OnMessageReceived += OnMessageReceived;
        Client.OnRaidNotification += OnRaidNotification;
        Client.OnUserJoined += OnUserJoined;
        Client.OnWhisperReceived += OnWhisperReceived;

        Logger.LogInformation("{@Event}",
          new { EventArgs="Bot Starting",
                Bot=Client.ConnectionCredentials.TwitchUsername });

        Client.Connect();
      });
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      return Task.Run(() => {
        Logger.LogInformation("{@Event}",
          new { EventArgs="Bot Stopping",
                Bot=Client.ConnectionCredentials.TwitchUsername });

        Client.Disconnect();
      });
    }

    private void OnConnected(object sender, OnConnectedArgs e)
    {
      Logger.LogInformation("{@Event}",
      new { Event="Bot Connected",
            Bot=Client.ConnectionCredentials.TwitchUsername });
    }

    private void OnDisconnected(object sender, OnDisconnectedEventArgs e)
    {
      Logger.LogWarning("{@Event}",
        new { Event="Bot Disconnected",
              Bot=Client.ConnectionCredentials.TwitchUsername });
    }

    private void OnError(object sender, OnErrorEventArgs error)
    {
      Logger.LogError(error.Exception, "{@Event}",
        new { Event="Error",
              Type=error.Exception.GetType(),
              Message=error.Exception.Message });
    }

    private void OnJoinedChannel(object sender, OnJoinedChannelArgs e)
    {
      Logger.LogInformation("{@Event}",
        new { Event="Bot Joined Channel", e.BotUsername, e.Channel });

      Client.SendMessage(e.Channel, "auth0bHype Bivrost has joined. auth0bHype");
    }

    private void OnUserJoined(object sender, OnUserJoinedArgs e)
    {
      Logger.LogInformation("{@Event}",
        new { EventArgs="User Joined Channel", e.Username, e.Channel });
    }

    private void OnMessageReceived(object sender, OnMessageReceivedArgs e)
    {
      Logger.LogInformation("{@Event}",
        new { EventArgs="Chat Message", e.ChatMessage.DisplayName, e.ChatMessage.Message });

        Mediator.Publish(new RecievedChatMessageNotification(e.ChatMessage));
    }

    private void OnWhisperReceived(object sender, OnWhisperReceivedArgs e)
    {
      Logger.LogInformation("{@Event}",
        new { EventArgs="Whisper Message", e.WhisperMessage.DisplayName, e.WhisperMessage.Message });
    }

    private void OnRaidNotification(object sender, OnRaidNotificationArgs e)
    {
      Logger.LogInformation("{@Event}",
        new { Event="Raid", e.RaidNotificaiton.DisplayName, e.RaidNotificaiton.MsgParamViewerCount });
    }
  }
}
