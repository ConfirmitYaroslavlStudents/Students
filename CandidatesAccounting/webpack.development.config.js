const path = require('path');
const webpack = require('webpack');
const MomentLocalesPlugin = require('moment-locales-webpack-plugin');

module.exports = {
  mode: 'development',
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
    new webpack.NoEmitOnErrorsPlugin(),
    new MomentLocalesPlugin({
      localesToKeep: ['es-us', 'ru'],
    }),
  ],

  optimization: {
    minimize: false,
    splitChunks: {
      chunks: "async",
      minSize: 10000,
      minChunks: 1,
      cacheGroups: {
        vendors: {
          test: /[\\/]node_modules\\[*lodash*|react\-dom\-factories]/,
          name: "vendors",
          chunks: "all"
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
        loader: ['style-loader', 'css-loader']
      },
      {
        test: /\.(woff|woff2|eot|ttf|svg)$/,
        loader: 'file-loader',
        options: {
          name: 'fonts/[name].[ext]'
        }
      }
    ]
  },

  devServer: {
    contentBase: path.join(__dirname, '/public'),
    hot: true,
    port: 3000,
  }
};