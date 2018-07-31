const path = require('path');
const CopyWebpackPlugin = require('copy-webpack-plugin');

module.exports = {
  mode: 'production',

  entry: {
    main: [
      'babel-polyfill',
      path.join(__dirname, 'app', 'renderer', 'main.js')
    ],
  },

  output: {
    path: path.join(__dirname, 'build', 'renderer'),
    publicPath: '/',
    filename: '[name].js'
  },

  plugins: [
    new CopyWebpackPlugin([
      {from: path.join(__dirname, 'app', 'main', 'index.js'), to: path.join(__dirname, 'build', 'main', 'index.js')},
      {from: path.join(__dirname, 'app', 'renderer', 'index.html'), to: path.join(__dirname, 'build', 'renderer', 'index.html')}
    ])
  ],

  optimization: {
    minimize: true
  },

  module: {
    rules: [
      {
        test: /\.js$/,
        include: path.join(__dirname, 'app', 'renderer'),
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
  },

  target: 'electron-main'
};