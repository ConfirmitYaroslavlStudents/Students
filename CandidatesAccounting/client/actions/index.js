import * as applicationActions from './applicationActions'
import * as authorizationActions from './authorizationActions'
import * as candidateActions from './candidateActions'
import * as commentActions from './commentActions'
import * as notificationActions from './notificationActions'
import * as tagActions from './tagActions'

const actions = {
  ...applicationActions,
  ...authorizationActions,
  ...candidateActions,
  ...commentActions,
  ...notificationActions,
  ...tagActions
}

export default actions