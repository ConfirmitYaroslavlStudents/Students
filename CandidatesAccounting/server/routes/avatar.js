import express from 'express'
import authenticationCheckMiddleware from '../middlewares/authenticationCheck'
import { maxAllowedAttachmentLength } from '../mongoose'
import { getAvatar, addAvatar } from '../mongoose/api/avatar'

const router = express.Router()

router.route('/:candidateStatus/:candidateId/avatar')
.all(authenticationCheckMiddleware)
.get((req, res) => {
  return getAvatar(req.params.candidateId).then((result, error) => {
    if (error) {
      return res.status(500).end()
    }
    if (result.avatarFile) {
      res.send(result.avatarFile)
    } else {
      res.end()
    }
  })
})
.post((req, res) => {
  if (!req.headers['content-length'] || Number(req.headers['content-length']) > maxAllowedAttachmentLength) {
    return res.status(400).end()
  }
  return addAvatar(req.params.candidateId, req.files.avatar).then((result, error) => {
    if (error) {
      return res.status(500).end()
    }
    res.end()
  })
})

export default router