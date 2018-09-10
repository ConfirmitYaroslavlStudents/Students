import express from 'express'
import authenticationCheckMiddleware from '../middlewares/authenticationCheck'
import { maxAllowedAttachmentLength } from '../mongoose'
import { getAttachment, addAttachment } from '../mongoose/api/commentAttachment'

const router = express.Router()

router.route('/:candidateStatus(interviewees|students|trainees)/:candidateId/comments/:commentId/attachment')
.all(authenticationCheckMiddleware)
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