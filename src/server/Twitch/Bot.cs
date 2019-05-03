using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Communication.Events;

namespace Bivrost.Web.Twitch
{
  public class Bot : IHostedService
  {
    private TwitchClient Client { get; }
    public ILogger<Bot> Logger { get; }

    public Bot(TwitchClient client, ILogger<Bot> logger)
    {
      Client = client ?? throw new System.ArgumentNullException(nameof(client));
      Logger = logger;
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

        Logger.LogInformation("Connecting to Twitch as {User}",
                              Client.ConnectionCredentials.TwitchUsername);
        Client.Connect();
      });
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      throw new System.NotImplementedException();
    }

    private void OnConnected(object sender, OnConnectedArgs e)
    {
      Logger.LogInformation("Connected to Twitch as {User}",
                            Client.ConnectionCredentials.TwitchUsername);
    }

    private void OnDisconnected(object sender, OnDisconnectedEventArgs e)
    {
      Logger.LogWarning("Disconnected from Twitch as {User}",
                          Client.ConnectionCredentials.TwitchUsername);
    }

    private void OnError(object sender, OnErrorEventArgs error)
    {
      Logger.LogError(error.Exception, error.Exception.Message);
    }

    private void OnJoinedChannel(object sender, OnJoinedChannelArgs e)
    {
      Logger.LogInformation("Joined Channel {@Event}", new { e.BotUsername, e.Channel });
    }

    private void OnUserJoined(object sender, OnUserJoinedArgs e)
    {
      Logger.LogInformation("User Joined Channel {@Event}", new { e.Username, e.Channel });
    }

    private void OnMessageReceived(object sender, OnMessageReceivedArgs e)
    {
      Logger.LogInformation("Message {@Event}",
        new { e.ChatMessage.DisplayName, e.ChatMessage.Message });
    }

    private void OnWhisperReceived(object sender, OnWhisperReceivedArgs e)
    {
      Logger.LogInformation("Whisper {@Event}",
        new { e.WhisperMessage.DisplayName, e.WhisperMessage.Message });
    }

    private void OnRaidNotification(object sender, OnRaidNotificationArgs e)
    {
      Logger.LogInformation("Raid {@Event}",
        new { e.RaidNotificaiton.DisplayName, e.RaidNotificaiton.MsgParamViewerCount });
    }
  }
}
