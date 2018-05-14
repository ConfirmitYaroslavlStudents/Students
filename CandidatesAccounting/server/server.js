import express from 'express'
import webpack from 'webpack'
import config from '../webpack.development.config'
import path from 'path'
import bodyParser from 'body-parser'
import fileUpload from 'express-fileupload'
import cookieParser from 'cookie-parser'
import favicon from 'serve-favicon'
import webpackDevMiddleware from 'webpack-dev-middleware'
import webpackHotMiddleware from 'webpack-hot-middleware'
import graphqlHTTP from 'express-graphql'
import { schema, root } from './graphQL'
import { Account, connect } from './mongoose'
import expressSession  from 'express-session'
import passport from 'passport'
import passportLocal from 'passport-local'
import { getResume, addResume, getAttachment, addAttachment } from './mongoose'
import template from './template'

const port = 3000

const developmentMode = process.argv[3] === 'development'

console.log(developmentMode ? 'Starting with development mode...' : 'Starting with production mode...')

const getUsername = (req) => {
  if (req.isAuthenticated()) {
    return req.user.username
  } else {
    return ''
  }
}

const app = express()

app.set('port', port)
app.set('view endine', 'ejs')

if (developmentMode) {
  const compiler = webpack(require('../webpack.development.config'))
  app.use(webpackDevMiddleware(compiler, {
    publicPath: config.output.publicPath,
    hot: true,
    stats: {
      colors: true
    }
  }))
  app.use(webpackHotMiddleware(compiler))
}

app.use(favicon(path.join(__dirname, '..', 'public', 'favicon.ico')))
app.use(expressSession({ secret: 'yDyTP3T3Dvc4206O8pm', resave: false, saveUninitialized: false }))
app.use(bodyParser.json())
app.use(fileUpload())
app.use(cookieParser())
app.use(passport.initialize())
app.use(passport.session())

passport.use(new passportLocal.Strategy(Account.authenticate()))
passport.serializeUser(Account.serializeUser())
passport.deserializeUser(Account.deserializeUser())

app.use('/graphql', (req, res, next) => {
  if (req.isAuthenticated() || !req.body || !req.body.query || !req.body.query.includes('mutation')) {
    next()
  } else {
    res.status(401).end()
  }
})

app.use('/graphql', graphqlHTTP({
  schema: schema,
  rootValue: root,
  graphiql: developmentMode,
}))

app.get('/login', (req, res) => {
  res.json({username: getUsername(req)})
})

app.post('/login', (req, res) => {
  return Account.findOne({username: req.body.username}, (findError, user) => {
    if (findError) {
      return res.status(401).end()
    }
    if (!user) {
      Account.register(new Account({username: req.body.username}), req.body.password, (registerError) => {
        if (registerError) {
          return res.status(401).end()
        }
        passport.authenticate('local')(req, res, () => {
          res.json({username: req.user.username})
        })
      })
    } else {
      passport.authenticate('local')(req, res, () => {
        res.json({username: req.user.username})
      })
    }
  })
})

app.get('/logout', (req, res) => {
  req.logout()
  req.session.destroy(() => {
    res.redirect('/')
  })
})

app.get('/interviewees/:intervieweeId/resume', (req, res) => {
  return getResume(req.params.intervieweeId).then((result, error) => {
    if (error) {
      return res.status(500).end()
    }
    res.attachment(result.resumeName)
    res.send(result.resumeData)
  })
})

app.post('/interviewees/:intervieweeId/resume', (req, res) => {
  if (!req.isAuthenticated()) {
    return res.status(401).end()
  }
  if (!req.headers['content-length'] || Number(req.headers['content-length']) > 16000000) {
    return res.status(500).end()
  }
  let file = req.files[Object.keys(req.files)[0]]
  return addResume(req.params.intervieweeId, file.name, file.data).then((result, error) => {
    if (error) {
      return res.status(500).end()
    }
    res.end()
  })
})

app.get('/:candidateStatus(interviewees|students|trainees)/:candidateId/commentsActions/:commentID/attachment', (req, res) => {
  return getAttachment(req.params.candidateId, req.params.commentId,).then((result, error) => {
    if (error) {
      return res.status(500).end()
    }
    res.attachment(result.attachmentName)
    res.send(result.attachmentData)
  })
})

app.post('/:candidateStatus(interviewees|students|trainees)/:candidateId/commentsActions/:commentID/attachment', (req, res) => {
  if (!req.isAuthenticated()) {
    return res.status(401).end()
  }
  if (!req.headers['content-length'] || Number(req.headers['content-length']) > 16000000) {
    return res.status(500).end()
  }
  let file = req.files[Object.keys(req.files)[0]];
  return addAttachment(req.params.candidateId, req.params.commentId, file.name, file.data).then((result, error) => {
    if (error) {
      return res.status(500).end()
    }
    res.end()
  })
})

app.use(express.static(path.join(__dirname, '..', 'public')))

app.get('/*', (req, res) => {
  res.send(template({
    assetsRoot: path.join('/', 'assets'),
    username: getUsername(req),
  }))
})

connect()

app.listen(app.get('port'), () => {
  console.log('Express server is listening on port', app.get('port'))
  if (developmentMode) {
    console.log('Waiting for webpack...')
  }
})