import mongoose from 'mongoose'
import passportLocalMongoose from 'passport-local-mongoose'
import {
  AccountSchema,
  CandidateSchema,
  IntervieweeSchema,
  StudentSchema,
  TraineeSchema,
  TagSchema
} from './schemas'

const connectionURL = 'mongodb://localhost:27017/CandidateAccounting'

mongoose.Promise = Promise

export function connect() {
  return mongoose.connect(connectionURL)
}

AccountSchema.plugin(passportLocalMongoose)
export const Account = mongoose.model('Account', AccountSchema, 'accounts')
const Candidate = mongoose.model('Candidate', CandidateSchema, 'candidates')
const Interviewee = mongoose.model('Interviewee', IntervieweeSchema, 'candidates')
const Student = mongoose.model('Student', StudentSchema, 'candidates')
const Trainee = mongoose.model('Trainee', TraineeSchema, 'candidates')
const Tag = mongoose.model('Tag', TagSchema, 'tags')

function identifyModel(status) {
  switch (status) {
    case 'Interviewee':
      return Interviewee
    case 'Student':
      return Student
    case 'Trainee':
      return Trainee
    default:
      return Candidate
  }
}

export function getCandidates(status) {
  const searchSettings = status === 'Candidate' ? {} : { status }
  return identifyModel(status).find(searchSettings).exec()
}

export function getCandidateById(id) {
  return Candidate.findById(mongoose.Types.ObjectId(id)).exec()
}

export function getAllTags() {
  return Tag.find({}).exec()
    .then(tags => tags.map(tag => tag.title))
}

export function getNotifications(username) {
  return Account.findOne({ username }).exec()
    .then(account => account.notifications)
}

export function addCandidate(newCandidate) {
  return identifyModel(newCandidate.status).create(newCandidate)
    .then(candidate => {
      updateTags(candidate.tags)
      return candidate._id
    })
}

export function updateCandidate(id, candidateNewState) {
  return identifyModel(candidateNewState.status).updateOne({_id: id}, {
    '$set': {
      status: candidateNewState.status,
      name: candidateNewState.name,
      nickname: candidateNewState.nickname ? candidateNewState.nickname : undefined,
      email: candidateNewState.email,
      phoneNumber: candidateNewState.phoneNumber ? candidateNewState.phoneNumber : undefined,
      avatar: candidateNewState.avatar ? candidateNewState.avatar : undefined,
      tags: candidateNewState.tags,
      subscribers: candidateNewState.subscribers,
      interviewDate: candidateNewState.interviewDate ? candidateNewState.interviewDate : undefined,
      resume: candidateNewState.resume ? candidateNewState.resume : undefined,
      resumeFile: candidateNewState.resumeFile ? candidateNewState.resumeFile : undefined,
      groupName: candidateNewState.groupName ? candidateNewState.groupName : undefined,
      startingDate: candidateNewState.startingDate ? candidateNewState.startingDate : undefined,
      endingDate: candidateNewState.endingDate ? candidateNewState.endingDate : undefined,
      mentor: candidateNewState.mentor ? candidateNewState.mentor : undefined
    },
    '$push': {comments: candidateNewState.comments ? candidateNewState.comments : []}})
    .then(candidate => {
      updateTags(candidate.tags)
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

export function deleteComment(candidateId, commentId) {
  return Candidate.findByIdAndUpdate(candidateId, {$pull: {comments: {_id: commentId}}}).exec()
}

export function updateComment(candidateStatus, candidateId, commentId, comment) {
  return identifyModel(candidateStatus).updateOne({_id: candidateId, 'comments._id': commentId}, {$set: {'comments.$': comment}}).exec()
    .then(() => {
      return candidateId
    })
}

export function subscribe(candidateId, email) {
  return Candidate.findByIdAndUpdate(candidateId, {$push: {subscribers: email}}).exec()
}

export function unsubscribe(candidateId, email) {
  return Candidate.findByIdAndUpdate(candidateId, {$pull: {subscribers: email}}).exec()
}

export function noticeNotification(username, notificationId) {
  return Account.updateOne({username, 'notifications._id': notificationId}, {$set: {'notifications.$.recent': false}}).exec()
}

export function deleteNotification(username, notificationId) {
  return Account.updateOne({username}, {$pull: {notifications: {_id: notificationId}}}).exec()
}

export function getAvatar(candidateId) {
  return getCandidateById(candidateId)
  .then(candidate => {
    return {
      avatarFile: candidate.avatar
    }
  })
}

export function addAvatar(id, avatarFile) {
  return getCandidateById(id)
  .then(candidate => {
    candidate.avatarFile = avatarFile
    candidate.comments = []
    return updateCandidate(id, candidate)
  })
}

export function getResume(intervieweeId) {
  return getCandidateById(intervieweeId)
    .then(interviewee => {
      return {
        resumeName: interviewee.resume,
        resumeFile: interviewee.resumeFile
      }
    })
}

export function addResume(id, resumeName, resumeFile) {
  return getCandidateById(id)
    .then(interviewee => {
      interviewee.resume = resumeName
      interviewee.resumeFile = resumeFile
      interviewee.comments = []
      return updateCandidate(id, interviewee)
    })
}

export function getAttachment(candidateId, commentId) {
  return getCandidateById(candidateId)
    .then(candidate => {
      for (let i = 0; i < candidate.comments.length; i++) {
        if (candidate.comments[i]._id.toString() === commentId) {
          return {
            attachmentName: candidate.comments[i].attachment,
            attachmentData: candidate.comments[i].attachmentFile
          }
        }
      }
      return {
        attachmentName: '',
        attachmentData: null
      }
    })
}

export function addAttachment(candidateId, commentId, attachmentName, attachmentData) {
  return getCandidateById(candidateId)
    .then(candidate => {
      for (let i = 0; i < candidate.comments.length; i++) {
        if (candidate.comments[i]._id.toString() === commentId) {
          candidate.comments[i].attachment = attachmentName
          candidate.comments[i].attachmentFile = attachmentData
          return updateComment(candidate.status, candidateId, commentId, candidate.comments[i])
        }
      }
    })
}

function updateTags(probablyNewTags) {
  getAllTags()
    .then(tags => {
      const tagsToAdd = [];
      probablyNewTags.forEach(tag => {
        if (!tags.includes(tag)) {
          tagsToAdd.push({ title: tag })
        }
      })
      if (tagsToAdd.length > 0) {
        addTags(tagsToAdd)
      }
    })
}

function addTags(tags) {
  Tag.create(tags)
}

function addNotification(source, recipient, notification) {
  Account.findOneAndUpdate({username: recipient}, {$push: {notifications: {recent: true, source: source, content: notification}}}).exec()
}