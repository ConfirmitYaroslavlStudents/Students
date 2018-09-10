import { getCandidateById, updateComment } from './candidate'

export const getAttachment = (candidateId, commentId) => {
  return getCandidateById(candidateId)
  .then(candidate => {
    for (let i = 0; i < candidate.comments.length; i++) {
      const comment = candidate.comments[i]
      if (comment._id.toString() === commentId) {
        return {
          attachmentName: comment.attachment,
          attachmentData: comment.attachmentFile
        }
      }
    }
    return {
      attachmentName: '',
      attachmentData: null
    }
  })
}

export const addAttachment = (candidateId, commentId, attachmentFile) => {
  return getCandidateById(candidateId)
  .then(candidate => {
    for (let i = 0; i < candidate.comments.length; i++) {
      let comment = candidate.comments[i]
      if (comment._id.toString() === commentId) {
        comment.attachment = attachmentFile.name
        comment.attachmentFile = attachmentFile.data
        return updateComment(candidateId, commentId, comment)
      }
    }
  })
}