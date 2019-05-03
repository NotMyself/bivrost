using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Communication.Events;

namespace Bivrost.Web.Twitch
{
  public class Bot : IHostedService
  {
    private TwitchClient Client { get; }

    public Bot(TwitchClient client)
    {
      Client = client ?? throw new System.ArgumentNullException(nameof(client));
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
      return Task.Run(() => {
        Client.OnConnected += OnConnected;
        Client.OnDisconnected += OnDisconnected;
        Client.OnError += OnError;
        Client.OnMessageReceived += OnMessageReceived;
        Client.OnRaidNotification += OnRaidNotification;
        Client.OnUserJoined += OnUserJoined;
        Client.OnWhisperReceived += OnWhisperReceived;

        Client.Connect();
      });
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      throw new System.NotImplementedException();
    }

    private void OnConnected(object sender, OnConnectedArgs e)
    {
      Console.WriteLine("Connected");
    }

    private void OnDisconnected(object sender, OnDisconnectedEventArgs e)
    {
      Console.WriteLine("Disconnected");
    }

    private void OnError(object sender, OnErrorEventArgs error)
    {
      Console.WriteLine(error.Exception.Message);
    }

    private void OnUserJoined(object sender, OnUserJoinedArgs e)
    {

    }

    private void OnMessageReceived(object sender, OnMessageReceivedArgs e)
    {
      Console.WriteLine(e.ChatMessage.Message);
    }

    private void OnWhisperReceived(object sender, OnWhisperReceivedArgs e)
    {

    }

    private void OnRaidNotification(object sender, OnRaidNotificationArgs e)
    {

    }
  }
}
