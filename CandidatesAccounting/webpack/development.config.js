const path = require('path');
const webpack = require('webpack');
const serverConfig = require('../server/development.server.config');

const root = path.join(__dirname, '..');

module.exports = {
  mode: 'development',

  entry: {
    main: [
      '@babel/polyfill',
      path.join(root, 'client', 'main.js')
    ],
  },

  output: {
    filename: '[name].js',
    path: path.join(root, 'dist', 'public', 'assets'),
    publicPath: serverConfig.assetsRoot
  },

  module: {
    rules: [
      {
        test: /\.js$/,
        include: path.join(root, 'client'),
        loader: 'babel-loader'
      },
      {
        test: /\.css$/,
        loaders: [ 'style-loader', 'css-loader' ]
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

  optimization: {
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

  devServer: {
    contentBase: path.join(root, 'public', 'assets'),
    publicPath: '/',
    port: 3001,
    headers: { 'Access-Control-Allow-Origin': '*' },
    watchContentBase: true
  },

  plugins: [
    new webpack.IgnorePlugin(/^\.\/locale?!\\ru.js$/, /moment$/),
    new webpack.HotModuleReplacementPlugin()
  ]
};