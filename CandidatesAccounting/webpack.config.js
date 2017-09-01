var path = require('path');

module.exports = {
  context: path.join(__dirname, 'frontend'),

  watch: true,

  entry: {
    main: './main'
  },
  output: {
    path:       path.join(__dirname, 'public'),
    filename:   '[name].js'
  },

  module: {
    loaders: [{
      test:    /\.js$/,
      include: path.join(__dirname, 'frontend'),
      loader: ["babel-loader"],
      query:{
        presets:["es2015", "react"]
      }
    }]
  },

  devServer: {
    contentBase: path.join(__dirname, '/public'),
    port: 3000,
    historyApiFallback: true
  }
};
