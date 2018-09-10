import express from 'express'
import webpack from 'webpack'
import path from 'path'
import bodyParser from 'body-parser'
import fileUpload from 'express-fileupload'
import cookieParser from 'cookie-parser'
import webpackDevMiddleware from 'webpack-dev-middleware'
import webpackHotMiddleware from 'webpack-hot-middleware'
import serverConfig from './server.config'
import { connect } from './mongoose'
import { Account } from './mongoose/api/account'
import expressSession  from 'express-session'
import passport from 'passport'
import LocalStrategy from 'passport-local'
import authorizationRouter from './routes/authorization'
import avatarRouter from './routes/avatar'
import resumeRouter from './routes/resume'
import commentAttachmentRouter from './routes/commentAttachment'
import graphqlRouter from './routes/graphql'
import template from './template'

//todo: remove
const developmentMode = process.argv[3] === 'development'

console.log(developmentMode ? 'Starting with development mode...' : 'Starting with production mode...')

const app = express()

app.set('port', serverConfig.port)
app.set('view endine', 'ejs')

// todo два процесса

if (developmentMode) {
  const config = require('../webpack/development.config')
  const compiler = webpack(require('../webpack/development.config'))
  app.use(webpackDevMiddleware(compiler, {
    publicPath: config.output.publicPath,
    hot: true,
    stats: { colors: true }
  }))
  app.use(webpackHotMiddleware(compiler))
}

app.use(express.static(path.join(__dirname, '..', 'public')))
app.use(expressSession({ secret: serverConfig.authorization.sessionSecret, resave: false, saveUninitialized: false }))
app.use(bodyParser.json())
app.use(fileUpload())
app.use(cookieParser())
app.use(passport.initialize())
app.use(passport.session())

passport.use(new LocalStrategy(Account.authenticate()))
passport.serializeUser(Account.serializeUser())
passport.deserializeUser(Account.deserializeUser())

app.use(authorizationRouter)
app.use(avatarRouter)
app.use(resumeRouter)
app.use(commentAttachmentRouter)
app.use(graphqlRouter)

app.get('/*', (req, res) => {
  res.send(template({
    assetsRoot: path.join('/', 'assets'),
    username: req.isAuthenticated() ? req.user.username : ''
  }))
})

connect()

app.listen(app.get('port'), () => {
  console.log('Express server is listening on port', app.get('port'))
  if (developmentMode) {
    console.log('Waiting for webpack...')
  }
})