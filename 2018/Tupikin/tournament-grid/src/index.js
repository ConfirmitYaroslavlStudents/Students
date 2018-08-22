import App from './app';
import { Provider } from 'react-redux';
import React from 'react';
import { render } from 'react-dom';
import store from './store/store';
import 'semantic-ui-less/semantic.less';
import 'treantjs/Treant.css';
import 'styling/app.less';

render(
  <Provider store={store}>
    <App/>
  </Provider>,
  document.getElementById('root')
);
