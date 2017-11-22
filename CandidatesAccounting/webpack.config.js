var path = require('path');
var webpack = require('webpack');

module.exports = {
  watch: true,

  entry: {
    main: [
      'babel-polyfill',
      'webpack-hot-middleware/client?path=/__webpack_hmr&timeout=20000',
      path.join(__dirname, 'client', 'app', 'main.js')
    ],
  },
  output: {
    path: path.join(__dirname, 'public'),
    publicPath: '/',
    filename: path.join('assets', '[name].js'),
  },

  plugins: [
    new webpack.HotModuleReplacementPlugin(),
    new webpack.NoEmitOnErrorsPlugin()
  ],

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
    hot: true,
    port: 3000,
  }
};