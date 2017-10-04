import React from 'react';
import ReactDOM from 'react-dom';
import {createStore} from 'redux';
import {Provider} from 'react-redux';
import reducer from './reducer'
import { BrowserRouter, Route} from 'react-router-dom';
import createMuiTheme from 'material-ui/styles/createMuiTheme';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import createPalette from 'material-ui/styles/createPalette';
import {deepPurple} from 'material-ui/colors';
import AppView from './appview';
import {Interviewee, Student, Trainee, Comment} from './candidatesClasses';

const store = createStore(
  reducer,
  window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__()
);

store.dispatch({
  type: "SET_STATE",
  state: {
    candidates: [
      new Interviewee(1, 'Олег', '07.01.1995', 'Oleg@mail.ru',
        [new Comment('Анна', '10:15 17 May 2017', 'Текст комментария №1')], '04.09.2017', '8'),
      new Student(2, 'Ольга', '11.04.1997', 'solnishko14@rambler.com',
        [new Comment('Анна', '10:15 17 May 2017', 'Текст комментария №2')], 'frontend'),
      new Student(3, 'Андрей', '12.07.1997', 'andrey@gmail.com',
        [new Comment('Анна', '10:15 17 May 2017', 'Текст комментария №3')], 'backend'),
      new Trainee(4, 'Оксана', '02.02.1992', 'Oksana@confirmit.com',
        [new Comment('Анна', '10:15 17 May 2017', 'Текст комментария №4')], 'Евгений Иванов'),
      new Trainee(5, 'Владимир', '02.02.1992', 'Vladimir@confirmit.com',
        [new Comment('Анна', '10:15 17 May 2017', 'Текст комментария №5')], 'Евгения Иванова'),
    ]
  }
});

const theme = createMuiTheme({
  palette: createPalette({
    primary: deepPurple,
  })
});

ReactDOM.render(
  <MuiThemeProvider theme={theme}>
    <Provider store={store}>
      <BrowserRouter>
        <Route path="/" component={AppView} />
      </BrowserRouter>
    </Provider>
  </MuiThemeProvider>,

  document.getElementById('root')
);