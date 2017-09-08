import React from 'react';
import ReactDOM from 'react-dom';
import {createStore} from 'redux';
import {Provider} from 'react-redux';
import createMuiTheme from 'material-ui/styles/theme';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import createPalette from 'material-ui/styles/palette';
import purple from 'material-ui/colors/purple';
import { BrowserRouter, Switch, Route} from 'react-router-dom';
import reducer from './reducer'
import {CreateCandidate, Interviewee, Student, Trainee, Comment} from './candidates';
import AppView from './appview';

const store = createStore(reducer);

store.dispatch({
  type: "SET_STATE",
  state: {
    candidates: [
      new Interviewee(1, 'Олег', '07.01.1995', 'Oleg@mail.ru', [new Comment(' ', ' ', 'C#')], '04.09.2017', '8'),
      new Student(2, 'Ольга', '11.04.1997', 'solnishko14@rambler.com', [new Comment(' ', ' ', 'ПМИ')], 'frontend'),
      new Student(3, 'Андрей', '12.07.1997', 'andrey@gmail.com',  [new Comment(' ', ' ', 'КБ')],'backend'),
      new Trainee(4, 'Оксана', '02.02.1992', 'Oksana@confirmit.com', [new Comment(' ', ' ', '5 этаж')], 'Евгений Иванов'),
      new Trainee(5, 'Оксана', '02.02.1992', 'Oksana@confirmit.com', [new Comment(' ', ' ', '5 этаж')], 'Евгений Иванов'),
      new Trainee(6, 'Оксана', '02.02.1992', 'Oksana@confirmit.com', [new Comment(' ', ' ', '5 этаж')], 'Евгений Иванов'),
      new Trainee(7, 'Оксана', '02.02.1992', 'Oksana@confirmit.com', [new Comment(' ', ' ', '5 этаж')], 'Евгений Иванов'),
      new Trainee(8, 'Оксана', '02.02.1992', 'Oksana@confirmit.com', [new Comment(' ', ' ', '5 этаж')], 'Евгений Иванов'),
      new Trainee(9, 'Оксана', '02.02.1992', 'Oksana@confirmit.com', [new Comment(' ', ' ', '5 этаж')], 'Евгений Иванов'),
      new Trainee(10, 'Оксана', '02.02.1992', 'Oksana@confirmit.com', [new Comment(' ', ' ', '5 этаж')], 'Евгений Иванов'),
      new Trainee(11, 'Оксана', '02.02.1992', 'Oksana@confirmit.com', [new Comment(' ', ' ', '5 этаж')], 'Евгений Иванов'),
      new Trainee(12, 'Оксана', '02.02.1992', 'Oksana@confirmit.com', [new Comment(' ', ' ', '5 этаж')], 'Евгений Иванов'),
      new Trainee(13, 'Оксана', '02.02.1992', 'Oksana@confirmit.com', [new Comment(' ', ' ', '5 этаж')], 'Евгений Иванов'),
      new Trainee(14, 'Оксана', '02.02.1992', 'Oksana@confirmit.com', [new Comment(' ', ' ', '5 этаж')], 'Евгений Иванов'),
      new Trainee(15, 'Владимир', '02.02.1992', 'Vladimir@confirmit.com', [new Comment(' ', ' ', '8 этаж')], 'Евгения Иванова')
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
        <Switch>
          <Route exact path="/" render={() => <AppView selectedTab={0} />} />
          <Route path="/interviewees" render={() => <AppView selectedTab={1} />} />
          <Route path="/students" render={() => <AppView selectedTab={2} />} />
          <Route path="/trainees" render={() => <AppView selectedTab={3} />} />
        </Switch>
      </BrowserRouter>
    </Provider>
  </MuiThemeProvider>,

  document.getElementById('root')
);