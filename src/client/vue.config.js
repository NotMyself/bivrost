module.exports = {
  css: {
    sourceMap: true
  },
  devServer: {
    proxy: 'http://localhost:5000/'
  },
  transpileDependencies: ['vuetify']
};
