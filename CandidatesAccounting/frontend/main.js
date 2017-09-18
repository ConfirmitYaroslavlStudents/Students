import React from 'react';
import ReactDOM from 'react-dom';
import {createStore} from 'redux';
import {Provider} from 'react-redux';
import reducer from './reducer'
import createMuiTheme from 'material-ui/styles/createMuiTheme';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import createPalette from 'material-ui/styles/createPalette';
import purple from 'material-ui/colors/purple';
import { BrowserRouter, Switch, Route} from 'react-router-dom';
import {CreateCandidate, Interviewee, Student, Trainee, Comment} from './candidates';
import AppView from './appview';

const store = createStore(reducer);

store.dispatch({
  type: "SET_STATE",
  state: {
    candidates: [
      new Interviewee(1, 'Олег', '07.01.1995', 'Oleg@mail.ru',
        [new Comment('Анна', '10:15 17.5.2017', 'Текст комментария №1')], '04.09.2017', '8'),
      new Student(2, 'Ольга', '11.04.1997', 'solnishko14@rambler.com',
        [new Comment('Анна', '10:15 17.05.2017', 'Текст комментария №2')], 'frontend'),
      new Student(3, 'Андрей', '12.07.1997', 'andrey@gmail.com',
        [new Comment('Анна', '10:15 17.5.2017', 'Текст комментария №3')], 'backend'),
      new Trainee(4, 'Оксана', '02.02.1992', 'Oksana@confirmit.com',
        [new Comment('Анна', '10:15 17.5.2017', 'Текст комментария №4')], 'Евгений Иванов'),
      new Trainee(5, 'Владимир', '02.02.1992', 'Vladimir@confirmit.com',
        [new Comment('Анна', '10:15 17.5.2017', 'Текст комментария №5')], 'Евгения Иванова')
    ],
    tempCandidate: CreateCandidate('Candidate', {})
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