import { HubConnectionBuilder } from '@aspnet/signalr';

const client = new HubConnectionBuilder()
  .configureLogging(process.env.VUE_APP_SIGNALR_LOG_LEVEL)
  .withUrl(process.env.VUE_APP_SIGNALR_HUB_URL)
  .build();

client.start();

export default function createWebSocketPlugin() {
  return store => {
    client.on('receiveChatMessage', message => {
      store.dispatch('chat/addMessage', message);
      store.dispatch('obs/sendMessage', {
        name: 'SetSceneItemProperties',
        settings: {
          'scene-name': 'Pop-Ups',
          item: 'Louie-Resubscribe',
          visible: true
        }
      });
    });
  };
}
