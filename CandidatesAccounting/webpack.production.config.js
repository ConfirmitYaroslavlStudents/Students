const path = require('path')
const webpack = require('webpack');

module.exports = {
  mode: 'production',

  entry: {
    main: [
      'babel-polyfill',
      path.join(__dirname, 'client', 'main.js')
    ],
  },

  output: {
    path: path.join(__dirname, 'public'),
    publicPath: '/',
    filename: path.join('assets', '[name].js'),
  },

  plugins: [
    new webpack.IgnorePlugin(/^\.\/locale$/, /moment$/)
  ],

  optimization: {
    minimize: true,
    splitChunks: {
      cacheGroups: {
        vendors: {
          test: /[\\/]node_modules\\[*lodash*|react\-dom\-factories]/,
          name: "vendors",
          chunks: "initial"
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