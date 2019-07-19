module.exports = {
  configureWebpack: function(config) {
    let plugins = [];
    for (let i = 0; i < config.plugins.length; i++) {
      if(config.plugins[i].constructor.name !== 'HotModuleReplacementPlugin') {
        plugins.push(config.plugins[i]);
      }
    }

    config.plugins = plugins;
  },

  css: {
    sourceMap: true
  }
}
