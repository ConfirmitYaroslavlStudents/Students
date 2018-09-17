import express from 'express'
import path from 'path'
import minimist from 'minimist'
import bodyParser from 'body-parser'
import fileUpload from 'express-fileupload'
import cookieParser from 'cookie-parser'
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

const argv = minimist(process.argv.slice(2))
const productionMode = argv.mode === 'production'

const serverConfig =
  productionMode ?
    require('./production.server.config')
    :
    require('./development.server.config')

console.log(
  productionMode ?
    'CandidateAccounting server is starting with production mode...'
    :
    'CandidateAccounting server is starting with development mode...')

const app = express()

app.set('port', serverConfig.port)
app.set('view endine', 'ejs')

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
    assetsRoot: serverConfig.assetsRoot,
    username: req.isAuthenticated() ? req.user.username : ''
  }))
})

connect()

app.listen(app.get('port'), () => {
  console.log('CandidateAccounting server is listening on port', app.get('port'))
})