import mongoose from 'mongoose'

const Schema = mongoose.Schema

export const AccountSchema = new Schema({
  username: {
    type: String,
    unique: true,
    required: true
  },
  password: String,
  notifications: [{
    recent: Boolean,
    source: {},
    content: {}
  }],
})

export const CandidateSchema = new Schema({
    status: String,
    name: {
      type: String,
      required: true
    },
    nickname: String,
    email: String,
    phoneNumber: String,
    hasAvatar: {
      type: Boolean,
      default: false
    },
    avatar: {
      type: Buffer,
      default: new Buffer('')
    },
    comments: [{
      author: {
        type: String,
        required: true
      },
      date: {
        type: String,
        required: true
      },
      text: {
        type: String,
        required: true
      },
      attachment: String,
      attachmentFile: {
        type: Buffer,
        default: new Buffer('')
      }
    }],
    tags: [String],
    subscribers: [String],
    interviewDate: String,
    resume: String,
    resumeFile: {
      type: Buffer,
      default: new Buffer('')
    },
    groupName: String,
    startingDate: String,
    endingDate: String,
    mentor: String
  })

export const TagSchema = new Schema({
    title: {
      type: String,
      required: true
    }
  })