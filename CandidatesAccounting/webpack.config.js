var path = require('path');

module.exports = {
  context: path.join(__dirname, 'client'),

  watch: true,

  entry: {
    main: ['babel-polyfill', './app/main.js'],
  },
  output: {
    path: path.join(__dirname, 'public', 'assets', 'javascript'),
    filename: '[name].js'
  },

  module: {
    loaders: [
      {
      test: /\.js$/,
      include: path.join(__dirname, 'client'),
      loader: ['babel-loader']
      },
      {
        test: /\.css$/,
        include: path.join(__dirname, 'client'),
        loader: ['style-loader', 'css-loader']
      },
    ]
  },

  devServer: {
    contentBase: path.join(__dirname, '/public'),
    port: 3000,
    historyApiFallback: true
  }
};