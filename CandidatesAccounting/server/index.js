import express from 'express'
import webpack from 'webpack'
import path from 'path'
import bodyParser from 'body-parser'
import fileUpload from 'express-fileupload'
import cookieParser from 'cookie-parser'
import webpackDevMiddleware from 'webpack-dev-middleware'
import webpackHotMiddleware from 'webpack-hot-middleware'
import graphqlHTTP from 'express-graphql'
import authorizationConfig from './authorization.config'
import serverConfig from './server.config'
import { schema, root } from './graphQL'
import { Account, connect } from './mongoose'
import expressSession  from 'express-session'
import passport from 'passport'
import passportLocal from 'passport-local'
import {
  getAvatar,
  addAvatar,
  getResume,
  addResume,
  getAttachment,
  addAttachment } from './mongoose'
import template from './template'

const port = serverConfig.port

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

// todo два процесса

if (developmentMode) {
  const config = require('../webpack/development.config')
  const compiler = webpack(require('../webpack/development.config'))
  app.use(webpackDevMiddleware(compiler, {
    publicPath: config.output.publicPath,
    hot: true,
    stats: {
      colors: true
    }
  }))
  app.use(webpackHotMiddleware(compiler))
}

app.use(expressSession({ secret: authorizationConfig.sessionSecret, resave: false, saveUninitialized: false }))
app.use(bodyParser.json())
app.use(fileUpload())
app.use(cookieParser())
app.use(passport.initialize())
app.use(passport.session())

passport.use(new passportLocal.Strategy(Account.authenticate()))
passport.serializeUser(Account.serializeUser())
passport.deserializeUser(Account.deserializeUser())

app.use('/graphql', (req, res, next) => {
  if (req.isAuthenticated()) {
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
  const username = req.body.username
  const allowedLogins = authorizationConfig.allowedLogins
  if (allowedLogins && allowedLogins.length > 0 && !allowedLogins.includes(username)) {
    return res.status(401).end()
  }
  return Account.findOne({ username }, (findError, user) => {
    if (findError) {
      console.log(findError)
      return res.status(401).end()
    }
    if (!user) {
      Account.register(new Account({ username }), req.body.password, (registerError) => {
        if (registerError) {
          console.log(registerError)
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

app.get('/:candidateStatus/:candidateId/avatar', (req, res) => {
  if (!req.isAuthenticated()) {
    return res.status(401).end()
  }
  return getAvatar(req.params.candidateId).then((result, error) => {
    if (error) {
      return res.status(500).end()
    }
    if (result.avatarFile) {
      res.attachment('avatar.jpg')
      res.send(result.avatarFile)
    } else {
      res.end()
    }
  })
})

app.post('/:candidateStatus/:candidateId/avatar', (req, res) => {
  if (!req.isAuthenticated()) {
    return res.status(401).end()
  }
  if (!req.headers['content-length'] || Number(req.headers['content-length']) > 16000000) {
    return res.status(500).end()
  }
  let file = req.files[Object.keys(req.files)[0]]
  return addAvatar(req.params.candidateId, file.data).then((result, error) => {
    if (error) {
      return res.status(500).end()
    }
    res.end()
  })
})

app.get('/interviewees/:intervieweeId/resume', (req, res) => {
  if (!req.isAuthenticated()) {
    return res.status(401).end()
  }
  return getResume(req.params.intervieweeId).then((result, error) => {
    if (error) {
      return res.status(500).end()
    }
    res.attachment(result.resumeName)
    res.send(result.resumeFile)
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

  return addResume(req.params.intervieweeId, file).then((result, error) => {
    if (error) {
      return res.status(500).end()
    }
    res.end()
  })
})

app.get('/:candidateStatus(interviewees|students|trainees)/:candidateId/comments/:commentId/attachment', (req, res) => {
  if (!req.isAuthenticated()) {
    return res.status(401).end()
  }
  return getAttachment(req.params.candidateId, req.params.commentId,).then((result, error) => {
    if (error) {
      return res.status(500).end()
    }
    res.attachment(result.attachmentName)
    res.send(result.attachmentData)
  })
})

app.post('/:candidateStatus(interviewees|students|trainees)/:candidateId/comments/:commentId/attachment', (req, res) => {
  if (!req.isAuthenticated()) {
    return res.status(401).end()
  }
  if (!req.headers['content-length'] || Number(req.headers['content-length']) > 16000000) {
    return res.status(500).end()
  }
  let file = req.files[Object.keys(req.files)[0]];
  return addAttachment(req.params.candidateId, req.params.commentId, file).then((result, error) => {
    if (error) {
      return res.status(500).end()
    }
    res.end()
  })
})

app.use(express.static(path.join(__dirname, '..', 'public')))

app.get('/*', (req, res) => {
  if (req.isAuthenticated()) {
    res.send(template({
      assetsRoot: path.join('/', 'assets'),
      username: getUsername(req),
    }))
  } else {
    res.send(template({
      assetsRoot: path.join('/', 'assets'),
      username: '',
    }))
  }
})

connect()

app.listen(app.get('port'), () => {
  console.log('Express server is listening on port', app.get('port'))
  if (developmentMode) {
    console.log('Waiting for webpack...')
  }
})