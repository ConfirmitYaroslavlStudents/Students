import { all } from 'redux-saga/effects'
import applicationSaga from './applicationSaga'
import authorizationSaga from './authorization/saga'
import candidatesSaga from './candidates/saga'
import commentSaga from './comments/saga'
import notificationSaga from './notifications/saga'
import tagSaga from './tags/saga'

const creator = ({ history }) => {
  function* rootSaga() {
    yield all([
      applicationSaga({ history }),
      authorizationSaga(),
      candidatesSaga({ history }),
      commentSaga({ history }),
      notificationSaga(),
      tagSaga()
    ])
  }

  return rootSaga()
}

export default creator