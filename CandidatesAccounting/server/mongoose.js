import mongoose from 'mongoose'
import passportLocalMongoose from 'passport-local-mongoose'
import serverConfig from './server.config.json'
import {
  AccountSchema,
  CandidateSchema,
  TagSchema
} from './schemas'

const connectionURL = serverConfig.databaseConnectionURL

mongoose.Promise = Promise

export function connect() {
  return mongoose.connect(connectionURL)
}

AccountSchema.plugin(passportLocalMongoose)
export const Account = mongoose.model('Account', AccountSchema, 'accounts')

const Candidate = mongoose.model('Candidate', CandidateSchema, 'candidates')
const Tag = mongoose.model('Tag', TagSchema, 'tags')


// Get database data

export function getCandidates(candidateStatus) {
  const searchSettings = candidateStatus === 'Candidate' ? {} : { status: candidateStatus }
  return Candidate.find(searchSettings).exec()
}

export function getCandidateById(candidateId) {
  return Candidate.findById(mongoose.Types.ObjectId(candidateId)).exec()
}

export function getAllTags() {
  return Tag.find({}).exec()
    .then(tags => tags.map(tag => tag.title))
}


// Change database data

export function addCandidate(newCandidate) {
  return Candidate.create(newCandidate)
    .then(candidate => {
      updateTags(candidate.tags)
      return candidate._id
    })
}

export function updateCandidate(candidateId, candidateNewState) {
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
    updateTags(candidateNewState.tags)
    return candidate
  })
}

export function deleteCandidate(candidateId) {
  return Candidate.findByIdAndRemove(candidateId).exec()
}

export function addComment(candidateId, comment) {
  const id = mongoose.Types.ObjectId()
  comment._id = id
  return Candidate.findByIdAndUpdate(candidateId, {$push: {comments: comment}}).exec()
    .then(candidate => {
      candidate.subscribers.forEach(subscriber => {
        if (subscriber !== comment.author) {
          addNotification(candidate, subscriber, comment)
        }
      })
      return id
    })
}

export function updateComment(candidateId, commentId, comment) {
  return Candidate.updateOne({_id: candidateId, 'comments._id': commentId}, {$set: {'comments.$': comment}}).exec()
  .then(() => {
    return candidateId
  })
}

export function deleteComment(candidateId, commentId) {
  return Candidate.findByIdAndUpdate(candidateId, {$pull: {comments: {_id: commentId}}}).exec()
}


// Subscription methods

export function subscribe(candidateId, email) {
  return Candidate.findByIdAndUpdate(candidateId, {$push: {subscribers: email}}).exec()
}

export function unsubscribe(candidateId, email) {
  return Candidate.findByIdAndUpdate(candidateId, {$pull: {subscribers: email}}).exec()
}


// Get/change avatar data

export function getAvatar(candidateId) {
  return getCandidateById(candidateId)
  .then(candidate => {
    return {
      avatarFile: candidate.avatar
    }
  })
}

export function addAvatar(candidateId, avatarFile) {
  return updateCandidate(candidateId, { avatar: avatarFile, hasAvatar: true })
}


// Get/change resume data

export function getResume(candidateId) {
  return getCandidateById(candidateId)
    .then(interviewee => {
      return {
        resumeName: interviewee.resume,
        resumeFile: interviewee.resumeFile
      }
    })
}

export function addResume(candidateId, resumeFile) {
  return updateCandidate(candidateId, { resume: resumeFile.name, resumeFile: resumeFile.data })
}


// Get/change comment attachment data

export function getAttachment(candidateId, commentId) {
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

export function addAttachment(candidateId, commentId, attachmentFile) {
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


// Get/change notification data

export function getNotifications(username) {
  return Account.findOne({ username }).exec()
  .then(account => account.notifications)
}

function addNotification(source, recipient, notification) {
  Account.findOneAndUpdate({ username: recipient }, {$push: {notifications: {recent: true, source: source, content: notification }}}).exec()
}

export function noticeNotification(username, notificationId) {
  return Account.updateOne({ username, 'notifications._id': notificationId}, {$set: {'notifications.$.recent': false }}).exec()
}

export function deleteNotification(username, notificationId) {
  return Account.updateOne({ username }, { $pull: { notifications: { _id: notificationId }}}).exec()
}


// Update tag database

function updateTags(probablyNewTags) {
  getAllTags()
    .then(tags => {
      const tagsToAdd = []
      probablyNewTags.forEach(tag => {
        if (!tags.includes(tag)) {
          tagsToAdd.push({ title: tag })
        }
      })
      if (tagsToAdd.length > 0) {
        Tag.create(tags)
      }
    })
}