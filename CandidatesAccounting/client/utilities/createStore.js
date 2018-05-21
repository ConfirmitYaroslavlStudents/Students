import React from 'react'
import { createStore, applyMiddleware, compose } from 'redux'
import createSagaMiddleware from 'redux-saga'
import rootSaga from '../rootSaga'
import { init } from '../applicationActions'

export default (reducer, username, history) => {
  const composeMiddlewares = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose
  const sagaMiddleware = createSagaMiddleware()
  let middlewares = applyMiddleware(sagaMiddleware)

  if (module.hot) {
    middlewares = composeMiddlewares(applyMiddleware(sagaMiddleware))
  }

  const store = createStore(reducer, middlewares)
  let sagaRun = sagaMiddleware.run(function* () {
    yield rootSaga({ history })
  })

  store.dispatch(init({ username }))

  if (module.hot) {
    module.hot.accept('../rootReducer', () => {
      const nextReducer = require('../rootReducer').default
      store.replaceReducer(nextReducer)
    })
    module.hot.accept('../rootSaga', () => {
      const newRootSaga = require('../rootSaga').default
      sagaRun.cancel()
      sagaRun.done.then(() => {
        sagaRun = sagaMiddleware.run(function* replaceSaga() {
          yield newRootSaga({ history })
        })
      })
    })
  }
  return store
}