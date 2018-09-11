const path = require('path');
const webpack = require('webpack');
const serverConfig = require('../server/development.server.config');

const root = path.join(__dirname, '..');

module.exports = {
  mode: 'development',

  entry: {
    main: [
      'babel-polyfill',
      path.join(root, 'client', 'main.js')
    ],
  },

  output: {
    filename: '[name].js',
    path: path.join(root, 'dist', 'public', 'assets'),
    publicPath: serverConfig.assetsRoot
  },

  plugins: [
    new webpack.IgnorePlugin(/^\.\/locale?!\\ru.js$/, /moment$/),
    new webpack.HotModuleReplacementPlugin()
  ],

  optimization: {
    minimize: false,
    noEmitOnErrors: true,
    splitChunks: {
      cacheGroups: {
        vendors: {
          test: /[\\/]node_modules\\[*lodash*|react\-dom\-factories]/,
          name: 'vendors',
          chunks: 'initial'
        }
      }
    }
  },

  module: {
    rules: [
      {
        test: /\.js$/,
        include: path.join(root, 'client'),
        loader: ['babel-loader']
      },
      {
        test: /\.css$/,
        loader: ['style-loader', 'css-loader']
      },
      {
        test: /\.(woff|woff2)(\?v=\d+\.\d+\.\d+)?$/,
        loader: 'file-loader',
        options: {
          name: 'fonts/[name].[ext]'
        }
      }
    ]
  },

  devServer: {
    contentBase: path.join(root, 'public', 'assets'),
    publicPath: '/',
    port: 3001,
    headers: { 'Access-Control-Allow-Origin': '*' },
    watchContentBase: true
  }
};