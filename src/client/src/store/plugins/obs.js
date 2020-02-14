import OBSWebSocket from 'obs-websocket-js';

const client = new OBSWebSocket();

export default function createWebSocketPlugin() {
  return store => {
    client.on('ConnectionOpened', data => {
      store.dispatch('obs/connectionOpened', data);
    });
    client.on('ConnectionClosed', data => {
      store.dispatch('obs/connectionClosed', data);
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
