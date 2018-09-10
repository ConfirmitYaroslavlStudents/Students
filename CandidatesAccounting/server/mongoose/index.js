import mongoose from 'mongoose'
import { AccountSchema } from './schemas'
import passportLocalMongoose from 'passport-local-mongoose'
import serverConfig from '../server.config.js'

mongoose.Promise = Promise

AccountSchema.plugin(passportLocalMongoose)

export const connect = () => {
  return mongoose.connect(serverConfig.databaseConnectionURL, { useNewUrlParser: true })
}

export const maxAllowedAttachmentLength = 16 * 1024 * 1024 // 16MB