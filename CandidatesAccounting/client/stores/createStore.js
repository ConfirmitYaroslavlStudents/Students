import React from 'react'
import { createStore, applyMiddleware, compose } from 'redux'
import createSagaMiddleware from 'redux-saga'
import sagaCreator from '../sagas/sagas'
import { init } from '../actions/applicationActions'

export default (reducer, username, history) => {
  const composeMiddlewares = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose
  const sagaMiddleware = createSagaMiddleware()
  let middlewares = applyMiddleware(sagaMiddleware)

  if (module.hot) {
    middlewares = composeMiddlewares(applyMiddleware(sagaMiddleware))
  }

  const store = createStore(reducer, middlewares)
  let sagaRun = sagaMiddleware.run(function* () {
    yield sagaCreator({ history })
  })

  store.dispatch(init({ username }))

  if (module.hot) {
    module.hot.accept('../reducers/rootReducer', () => {
      const nextReducer = require('../reducers/rootReducer').default
      store.replaceReducer(nextReducer)
    })
    module.hot.accept('../sagas/sagas', () => {
      const newRootSaga = require('../sagas/sagas').default
      sagaRun.cancel()
      sagaRun.done.then(() => {
        sagaRun = sagaMiddleware.run(function* replaceSaga() {
          yield newRootSaga()
        })
      })
    })
  }
  return store
}