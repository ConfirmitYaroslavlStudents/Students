import { combineReducers } from 'redux'
import application from './applicationReducer'
import authorization from './authorizationReducer'
import candidates from './candidateReducer'
import comments from './commentReducer'
import notifications from './notificationReducer'
import tags from './tagReducer'

export default combineReducers({
  application,
  authorization,
  candidates,
  comments,
  notifications,
  tags
})