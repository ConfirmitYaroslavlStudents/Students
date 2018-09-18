import express from 'express'
import { Account } from '../mongoose/api/account'
import passport from 'passport'
import minimist from 'minimist'

const argv = minimist(process.argv.slice(2))
const serverConfig =
  argv.mode === 'production' ?
    require('../production.server.config')
    :
    require('../development.server.config')

const router = express.Router()

router.route('/login')
.get((req, res) => {
  const username =
    req.isAuthenticated() ?
      req.user.username
      :
      ''
  res.json({ username })
})
.post((req, res) => {
  const allowedLogins = serverConfig.authorization.allowedLogins
  if (allowedLogins && allowedLogins.length > 0 && !allowedLogins.includes(req.body.username)) {
    return res.status(403).end()
  }

  return Account.findOne({ username: req.body.username }, (error, user) => {
    if (error) {
      return res.status(500).end()
    }
    if (user) {
      passport.authenticate('local')(req, res, () => {
        res.json({ username: req.user.username })
      })
    } else {
      Account.register(new Account({ username: req.body.username }), req.body.password, (error) => {
        if (error) {
          return res.status(500).end()
        }
        passport.authenticate('local')(req, res, () => {
          res.json({ username: req.body.username })
        })
      })
    }
  })
})

router.route('/logout')
.get((req, res) => {
  req.logout()
  req.session.destroy(() => {
    res.redirect('/')
  })
})

export default router