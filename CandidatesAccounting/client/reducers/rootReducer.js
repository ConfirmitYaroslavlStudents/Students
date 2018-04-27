import { combineReducers } from 'redux'
import application, {SELECTORS as APPLICATION} from './applicationReducer'
import authorization, {SELECTORS as AUTHORIZATION} from './authorizationReducer'
import candidates, {SELECTORS as CANDIDATES} from './candidateReducer'
import comments, {SELECTORS as COMMENTS} from './commentReducer'
import notifications, {SELECTORS as NOTIFICATIONS} from './notificationReducer'
import tags, {SELECTORS as TAGS} from './tagReducer'

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

function bindSelectors(selector, innerSelector) { // TODO: correct bindSelectors
  return innerSelector
}
