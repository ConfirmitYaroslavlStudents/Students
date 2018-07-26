const path = require('path');
const CopyWebpackPlugin = require('copy-webpack-plugin')

module.exports = {
  mode: 'development',

  entry: {
    main: [
      'babel-polyfill',
      path.join(__dirname, 'application', 'main.js')
    ],
  },

  output: {
    path: path.join(__dirname, 'dist'),
    publicPath: '/',
    filename: '[name].js'
  },

  plugins: [
    new CopyWebpackPlugin([
      {from: path.join(__dirname, 'application', 'index.html'), to: path.join(__dirname, 'dist', 'index.html')}
    ])
  ],

  optimization: {
    minimize: false
  },

  module: {
    rules: [
      {
        test: /\.js$/,
        include: path.join(__dirname, 'application'),
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
              minimize: false
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
  },

  target: 'electron-main'
};