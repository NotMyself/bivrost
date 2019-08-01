import { HubConnectionBuilder } from '@aspnet/signalr';

class Hub {
  constructor() {
    this.client = new HubConnectionBuilder()
      .configureLogging(process.env.VUE_APP_SIGNALR_LOG_LEVEL)
      .withUrl(process.env.VUE_APP_SIGNALR_HUB_URL)
      .build();
  }

  start() {
    this.client.start();
  }
}

export default new Hub();
