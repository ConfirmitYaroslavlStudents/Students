const path = require('path');
const webpack = require('webpack');
const CopyWebpackPlugin = require('copy-webpack-plugin');

module.exports = {
  mode: 'production',

  entry: {
    main: [
      'babel-polyfill',
      path.join(__dirname, 'client', 'main.js')
    ],
  },

  output: {
    path: path.join(__dirname, 'dist', 'public'),
    publicPath: '/',
    filename: path.join('assets', '[name].js'),
  },

  plugins: [
    new webpack.IgnorePlugin(/^\.\/locale$/, /moment$/),
    new CopyWebpackPlugin([
      {from: path.join(__dirname, 'favicon.ico'), to: path.join(__dirname, 'dist', 'public', 'favicon.ico')},
      {from: path.join(__dirname, 'manifest.json'), to: path.join(__dirname, 'dist', 'public', 'manifest.json')},
      {from: path.join(__dirname, 'index.js'), to: path.join(__dirname, 'dist', 'index.js')},
      {from: path.join(__dirname, 'package.json'), to: path.join(__dirname, 'dist', 'package.json')},
      {from: path.join(__dirname, 'web.config'), to: path.join(__dirname, 'dist', 'web.config')},
      {from: path.join(__dirname, 'server', 'authorization.config.js'), to: path.join(__dirname, 'dist', 'server', 'authorization.config.js')},
      {from: path.join(__dirname, 'server', 'server.config.js'), to: path.join(__dirname, 'dist', 'server', 'server.config.js')}
    ])
  ],

  optimization: {
    minimize: true,
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
        include: path.join(__dirname, 'client'),
        loader: ['babel-loader']
      },
      {
        test: /\.css$/,
        use: [
          {
            loader: 'style-loader'
          },
          {
            loader: 'css-loader',
            options: {
              minimize: true
            }
          }]
      },
      {
        test: /\.(woff|woff2)(\?v=\d+\.\d+\.\d+)?$/,
        loader: 'file-loader',
        options: {
          name: 'fonts/[name].[hash:5].[ext]'
        }
      }
    ]
  }
};