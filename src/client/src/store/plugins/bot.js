import { HubConnectionBuilder } from '@aspnet/signalr';

const client = new HubConnectionBuilder()
  .configureLogging(process.env.VUE_APP_SIGNALR_LOG_LEVEL)
  .withUrl(process.env.VUE_APP_SIGNALR_HUB_URL)
  .build();

export default function createWebSocketPlugin() {
  return store => {
    client.on('stateChanged', (oldState, newState) => {
      if (oldState !== newState && newState !== 'Connected')
        store.dispatch('chat/connectionClosed');
      else store.dispatch('chat/connectionOpened');
    });

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

    client
      .start()
      .then(() => {
        store.dispatch('chat/connectionOpened');
      })
      .catch(err => {
        store.dispatch('chat/connectionError', err);
      });
  };
}
