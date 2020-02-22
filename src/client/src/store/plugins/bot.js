import { HubConnectionBuilder } from '@aspnet/signalr';

function defaultClient() {
  return new HubConnectionBuilder()
    .configureLogging(process.env.VUE_APP_SIGNALR_LOG_LEVEL)
    .withUrl(process.env.VUE_APP_SIGNALR_HUB_URL)
    .build();
}

export default function createWebSocketPlugin(client = defaultClient()) {
  return store => {
    client.on('stateChanged', (oldState, newState) => {
      if (oldState !== newState && newState !== 'Connected')
        store.dispatch('app/connectionClosed', 'bot');
      else store.dispatch('app/connectionOpened', 'bot');
    });

    client.on('receiveChatMessage', message => {
      store.dispatch('chat/addMessage', message);
      // store.dispatch('obs/sendMessage', {
      //   name: 'SetSceneItemProperties',
      //   settings: {
      //     'scene-name': 'Pop-Ups',
      //     item: 'Louie-Resubscribe',
      //     visible: true
      //   }
      // });
    });

    store.subscribe((mutation, state) => {
      if (!state.app.bot && mutation.type === 'chat/CONNECT')
        client
          .start()
          .then(() => {
            store.dispatch('app/connectionOpened', 'bot');
          })
          .catch(err => {
            store.dispatch('app/connectionError', err);
          });
    });
  };
}
