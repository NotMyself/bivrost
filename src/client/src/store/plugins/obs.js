import OBSWebSocket from 'obs-websocket-js';

const clientName = 'obs';

function defaultClient() {
  return new OBSWebSocket();
}

export default function createWebSocketPlugin(client = defaultClient()) {
  return store => {
    client.on('ConnectionOpened', () => {
      store.dispatch('app/connectionOpened', clientName);
    });
    client.on('ConnectionClosed', () => {
      store.dispatch('app/connectionClosed', clientName);
    });

    client.on('error', err => {
      store.dispatch('app/connectionError', err);
    });

    store.subscribe((mutation, state) => {
      if (!state.app.obsConnected && mutation.type === 'obs/CONNECT')
        client.connect().catch(err => {
          store.dispatch('app/connectionError', err);
        });

      if (state.obs.connected && mutation.type === 'obs/SEND_MESSAGE')
        client.send(mutation.payload.name, mutation.payload.settings);
    });
  };
}
