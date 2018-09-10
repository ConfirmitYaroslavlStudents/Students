import express from 'express'
import { getResume, addResume, maxAllowedAttachmentLength } from '../mongoose'

const router = express.Router()

router.route('/interviewees/:intervieweeId/resume')
.all((req, res, next) => {
  if (req.isAuthenticated()) {
    next()
  } else {
    return res.status(401).end()
  }
})
.get((req, res) => {
  return getResume(req.params.intervieweeId).then((result, error) => {
    if (error) {
      return res.status(500).end()
    }
    res.attachment(result.resumeName)
    res.send(result.resumeFile)
  })
})
.post((req, res) => {
  if (!req.headers['content-length'] || Number(req.headers['content-length']) > maxAllowedAttachmentLength) {
    return res.status(400).end()
  }
  return addResume(req.params.intervieweeId, req.files.resume).then((result, error) => {
    if (error) {
      return res.status(500).end()
    }
    res.end()
  })
})

export default router