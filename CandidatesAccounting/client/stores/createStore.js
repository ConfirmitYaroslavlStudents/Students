import '../css/commonStyles.css'
import '../css/selectizeStyles.css'
import 'typeface-roboto'
import 'react-quill/dist/quill.snow.css'
import React from 'react'
import { createStore, applyMiddleware, compose } from 'redux'
import createSagaMiddleware from 'redux-saga'
import rootSaga from '../sagas/sagas'
import { init } from '../actions/actions'

export default (reducer, initialState) => {
  const composeMiddlewares = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose
  const sagaMiddleware = createSagaMiddleware()
  let middlewares = applyMiddleware(sagaMiddleware)

  if (module.hot) {
    middlewares = composeMiddlewares(applyMiddleware(sagaMiddleware))
  }

  const store = createStore(reducer, middlewares)
  store.dispatch(init(initialState))

  let sagaRun = sagaMiddleware.run(function* () {
    yield rootSaga()
  })

  if (module.hot) {
    module.hot.accept('../reducers/reducer', () => {
      const nextReducer = require('../reducers/reducer').default
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