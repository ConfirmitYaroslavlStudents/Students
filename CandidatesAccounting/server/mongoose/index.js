import mongoose from 'mongoose'
import { AccountSchema } from './schemas'
import passportLocalMongoose from 'passport-local-mongoose'
import serverConfig from '../development.server.config.js'

mongoose.set('useFindAndModify', false)
mongoose.set('useCreateIndex', true)
mongoose.Promise = Promise

AccountSchema.plugin(passportLocalMongoose)

export const connect = () => {
  return mongoose.connect(serverConfig.databaseConnectionURL, { useNewUrlParser: true })
}

export const maxAllowedAttachmentLength = 16 * 1024 * 1024 // 16MB