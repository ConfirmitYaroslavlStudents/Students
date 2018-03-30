const path = require('path');
const MomentLocalesPlugin = require('moment-locales-webpack-plugin');

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

  plugins: [
    new MomentLocalesPlugin({
      localesToKeep: ['es-us', 'ru'],
    }),
  ],

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