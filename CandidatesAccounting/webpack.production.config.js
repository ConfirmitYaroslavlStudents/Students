var path = require('path');

module.exports = {
  mode: 'production',
  entry: {
    main: [
      'babel-polyfill',
      path.join(__dirname, 'client', 'app', 'main.js')
    ],
  },
  output: {
    path: path.join(__dirname, 'public'),
    publicPath: '/',
    filename: path.join('assets', '[name].js'),
  },

  module: {
    rules: [
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
  }
};