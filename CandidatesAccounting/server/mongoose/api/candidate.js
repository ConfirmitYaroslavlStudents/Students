import mongoose from 'mongoose'
import { CandidateSchema } from '../schemas'
import { updateTags } from './tag'
import { addNotification } from './account'

const Candidate = mongoose.model('Candidate', CandidateSchema, 'candidates')

export const getCandidates = (candidateStatus) => {
  const searchSettings = candidateStatus === 'Candidate' ? {} : { status: candidateStatus }
  return Candidate.find(searchSettings).exec()
}

export const getCandidateById = (candidateId) => {
  return Candidate.findById(mongoose.Types.ObjectId(candidateId)).exec()
}

export const addCandidate = (newCandidate) => {
  return Candidate.create(newCandidate)
  .then(candidate => {
    updateTags(candidate.tags)
    return candidate._id.toString()
  })
}

export const updateCandidate = (candidateId, candidateNewState) => {
  let comments = candidateNewState.comments
  if (!comments) {
    comments = []
  }
  delete candidateNewState.comments
  delete candidateNewState.id
  return Candidate.updateOne({_id: candidateId}, {
    '$set': {
      ...candidateNewState
    },
    '$push': { comments }
  })
  .then(candidate => {
    if (candidateNewState.tags && candidateNewState.tags.length > 0) {
      updateTags(candidateNewState.tags)
    }
    return candidate
  })
}

export const deleteCandidate = (candidateId) => {
  return Candidate.findByIdAndRemove(candidateId).exec()
}

export const addComment = (candidateId, comment) => {
  const id = mongoose.Types.ObjectId()
  comment._id = id
  return Candidate.findByIdAndUpdate(candidateId, {$push: {comments: comment}}).exec()
  .then(candidate => {
    candidate.subscribers.forEach(subscriber => {
      if (subscriber !== comment.author) {
        addNotification(candidate, subscriber, comment)
      }
    })
    return id.toString()
  })
}

export const updateComment = (candidateId, commentId, comment) => {
  return Candidate.updateOne({_id: candidateId, 'comments._id': commentId}, {$set: {'comments.$': comment}}).exec()
  .then(() => {
    return candidateId
  })
}

export const deleteComment = (candidateId, commentId) => {
  return Candidate.updateOne({ _id: candidateId }, {$pull: {comments: {_id: commentId}}}).exec()
}

export const subscribe = (candidateId, email) => {
  return Candidate.updateOne({ _id: candidateId }, {$push: {subscribers: email}}).exec()
}

export const unsubscribe = (candidateId, email) => {
  return Candidate.updateOne({ _id: candidateId }, {$pull: {subscribers: email}}).exec()
}