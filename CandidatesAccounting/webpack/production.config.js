const path = require('path');
const webpack = require('webpack');
const CopyWebpackPlugin = require('copy-webpack-plugin');

const __root = path.join(__dirname, '..');

module.exports = {
  mode: 'production',

  entry: {
    main: [
      'babel-polyfill',
      path.join(__root, 'client', 'main.js')
    ],
  },

  output: {
    filename: path.join('assets', '[name].js'),
    path: path.join(__root, 'dist', 'public'),
    publicPath: '/'
  },

  plugins: [
    new webpack.IgnorePlugin(/^\.\/locale$/, /moment$/),
    new CopyWebpackPlugin([
      {from: path.join(__root, 'public', 'assets', 'favicon.ico'), to: path.join(__root, 'dist', 'public', 'assets', 'favicon.ico')},
      {from: path.join(__root, 'public', 'assets', 'manifest.json'), to: path.join(__root, 'dist', 'public', 'assets', 'manifest.json')},
      {from: path.join(__root, 'index.js'), to: path.join(__root, 'dist', 'index.js')},
      {from: path.join(__root, 'package.json'), to: path.join(__root, 'dist', 'package.json')},
      {from: path.join(__root, 'web.config'), to: path.join(__root, 'dist', 'web.config')},
      {from: path.join(__root, 'server', 'production.server.config.js'), to: path.join(__root, 'dist', 'server', 'production.server.config.js')}
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
        include: path.join(__root, 'client'),
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