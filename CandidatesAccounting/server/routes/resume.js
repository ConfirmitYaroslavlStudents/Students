import express from 'express'
import authenticationCheckMiddleware from '../middlewares/authenticationCheck'
import { maxAllowedAttachmentLength } from '../mongoose'
import { getResume, addResume } from '../mongoose/api/resume'

const router = express.Router()

router.route('/interviewees/:intervieweeId/resume')
.all(authenticationCheckMiddleware)
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