import React from 'react'
import { createStore, applyMiddleware } from 'redux'
import createSagaMiddleware from 'redux-saga'
import saga from '../saga'
import { init } from '../actions'

export default (reducer, initialState) => {
  const sagaMiddleware = createSagaMiddleware()
  let middlewares = applyMiddleware(sagaMiddleware)
  const store = createStore(reducer, middlewares)

  sagaMiddleware.run(function* () {
    yield saga()
  })

  store.dispatch(init({ initialState }))

  return store
}