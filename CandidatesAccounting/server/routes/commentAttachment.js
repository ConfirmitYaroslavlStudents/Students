import express from 'express'
import { getAttachment, addAttachment, maxAllowedAttachmentLength } from '../mongoose'

const router = express.Router()

router.route('/:candidateStatus(interviewees|students|trainees)/:candidateId/comments/:commentId/attachment')
.all((req, res, next) => {
  if (req.isAuthenticated()) {
    next()
  } else {
    return res.status(401).end()
  }
})
.get((req, res) => {
  return getAttachment(req.params.candidateId, req.params.commentId,).then((result, error) => {
    if (error) {
      return res.status(500).end()
    }
    res.attachment(result.attachmentName)
    res.send(result.attachmentData)
  })
})
.post((req, res) => {
  if (!req.headers['content-length'] || Number(req.headers['content-length']) > maxAllowedAttachmentLength) {
    return res.status(400).end()
  }
  return addAttachment(req.params.candidateId, req.params.commentId, req.files.attachment).then((result, error) => {
    if (error) {
      return res.status(500).end()
    }
    res.end()
  })
})

export default router