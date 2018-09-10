import mongoose from 'mongoose'
import { AccountSchema } from '../schemas'
import passportLocalMongoose from 'passport-local-mongoose'

AccountSchema.plugin(passportLocalMongoose)
export const Account = mongoose.model('Account', AccountSchema, 'accounts')

export const getNotifications = (username) => {
  return Account.findOne({ username }).exec()
  .then(account => account.notifications)
}

export const addNotification = (source, recipient, notification) => {
  Account.findOneAndUpdate({ username: recipient }, {$push: {notifications: {recent: true, source: source, content: notification }}}).exec()
}

export const noticeNotification = (username, notificationId) => {
  return Account.updateOne({ username, 'notifications._id': notificationId}, {$set: {'notifications.$.recent': false }}).exec()
}

export const deleteNotification = (username, notificationId) => {
  return Account.updateOne({ username }, { $pull: { notifications: { _id: notificationId }}}).exec()
}