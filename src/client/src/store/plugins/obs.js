import OBSWebSocket from 'obs-websocket-js';

function defaultClient() {
  return new OBSWebSocket();
}

export default function createWebSocketPlugin(client = defaultClient()) {
  return store => {
    client.on('ConnectionOpened', () => {
      store.dispatch('obs/connectionOpened');
    });
    client.on('ConnectionClosed', () => {
      store.dispatch('obs/connectionClosed');
    });

    client.on('error', err => {
      store.dispatch('obs/connectionError', err);
    });
    store.subscribe((mutation, state) => {
      if (state.obs.connected && mutation.type === 'obs/SEND_MESSAGE')
        client.send(mutation.payload.name, mutation.payload.settings);
    });

    client.connect();
  };
}
