import React from 'react'
import { createStore as reduxCreateStore, applyMiddleware } from 'redux'
import { composeWithDevTools } from 'redux-devtools-extension'
import createSagaMiddleware from 'redux-saga'

let sagaMiddleware

const createStore = (reducer) => {
  sagaMiddleware = createSagaMiddleware()
  const middlewares =
    module.hot ?
      composeWithDevTools(applyMiddleware(sagaMiddleware))
      :
      applyMiddleware(sagaMiddleware)
  return reduxCreateStore(reducer, middlewares)
}

export { createStore, sagaMiddleware }