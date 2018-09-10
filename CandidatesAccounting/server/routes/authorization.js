import express from 'express'
import serverConfig from '../server.config'
import { Account } from '../mongoose/api/account'
import passport from 'passport'

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
      Account.register(new Account({ username: req.user.username }), req.body.password, (error) => {
        if (error) {
          return res.status(500).end()
        }
        passport.authenticate('local')(req, res, () => {
          res.json({ username: req.user.username })
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