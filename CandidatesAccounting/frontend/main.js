import React from 'react';
import ReactDOM from 'react-dom';
import {createStore} from 'redux';
import {Provider} from 'react-redux';
import createMuiTheme from 'material-ui/styles/theme';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import createPalette from 'material-ui/styles/palette';
import purple from 'material-ui/colors/purple';
import { BrowserRouter, Router, HashRouter, Route} from 'react-router-dom';
import reducer from './reducer'
import {CreateCandidate, Interviewee, Student, Trainee} from './candidates';
import AppView from './appview';

const store = createStore(reducer);

store.dispatch({
  type: "SET_STATE",
  state: {
    candidates: [
      new Interviewee(1, 'Олег', '07.01.1995', 'Oleg@mail.ru', 'C#', '04.09.2017', '8'),
      new Student(2, 'Ольга', '11.04.1997', 'solnishko14@rambler.com', 'ПМИ', 'frontend'),
      new Student(3, 'Андрей', '12.07.1997', 'andrey@gmail.com', 'КБ', 'backend'),
      new Trainee(4, 'Оксана', '02.02.1992', 'Oksana@confirmit.com', '5 этаж', 'Евгений Иванов'),
      new Trainee(5, 'Игорь', '22.07.1994', 'Igor@confirmit.com', '8 этаж', 'Евгения Иванова')
    ],
    candidateEditInfo: CreateCandidate('Candidate', {})
  }
});

const theme = createMuiTheme({
  palette: createPalette({
    primary: purple
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