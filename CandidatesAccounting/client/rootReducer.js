import { combineReducers } from 'redux'
import application, { SELECTORS as APPLICATION } from './applicationReducer'
import authorization, { SELECTORS as AUTHORIZATION } from './authorization/reducer'
import candidates, { SELECTORS as CANDIDATES } from './candidates/reducer'
import comments, { SELECTORS as COMMENTS } from './comments/reducer'
import notifications, { SELECTORS as NOTIFICATIONS } from './notifications/reducer'
import tags, { SELECTORS as TAGS } from './tags/reducer'
import bindSelectors from './utilities/bindSelectors'

export default combineReducers({
  application,
  authorization,
  candidates,
  comments,
  notifications,
  tags
})

export const SELECTORS = {
  APPLICATION: bindSelectors(state => state.application, APPLICATION),
  AUTHORIZATION: bindSelectors(state => state.authorization, AUTHORIZATION),
  CANDIDATES: bindSelectors(state => state.candidates, CANDIDATES),
  COMMENTS: bindSelectors(state => state.comments, COMMENTS),
  NOTIFICATIONS: bindSelectors(state => state.notifications, NOTIFICATIONS),
  TAGS: bindSelectors(state => state.tags, TAGS)
}
