import React from 'react'
import { createStore as reduxCreateStore, applyMiddleware } from 'redux'
import { composeWithDevTools } from 'redux-devtools-extension';
import createSagaMiddleware from 'redux-saga'
import rootSaga from '../rootSaga'

let sagaMiddleware
let sagaRun

const createStore = (reducer, history) => {
  sagaMiddleware = createSagaMiddleware()

  const store = reduxCreateStore(reducer, composeWithDevTools(applyMiddleware(sagaMiddleware)))

  sagaRun = sagaMiddleware.run(function* () {
    yield rootSaga({ history })
  })

  return store
}

export { sagaMiddleware, sagaRun, createStore }