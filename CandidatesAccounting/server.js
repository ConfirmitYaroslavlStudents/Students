const express = require('express');
const http = require('http');
const path = require('path');
const bodyParser  = require('body-parser');
const favicon = require ('serve-favicon');
const {Interviewee, Student, Trainee, Comment} = require('./frontend/candidatesClasses/');

let candidates = [
  new Interviewee(1, 'Олег', '07.01.1995', 'Oleg@mail.ru',
    [new Comment('Анна', '10:15 17 May 2017', 'Текст комментария №1')], '04.09.2017', '8'),
  new Student(2, 'Ольга', '11.04.1997', 'solnishko14@rambler.com',
    [new Comment('Анна', '10:15 17 May 2017', 'Текст комментария №2')], 'frontend'),
  new Student(3, 'Андрей', '12.07.1997', 'andrey@gmail.com',
    [new Comment('Анна', '10:15 17 May 2017', 'Текст комментария №3')], 'backend'),
  new Trainee(4, 'Оксана', '02.02.1992', 'Oksana@confirmit.com',
    [new Comment('Анна', '10:15 17 May 2017', 'Текст комментария №4')], 'Евгений Иванов'),
  new Trainee(5, 'Владимир', '02.02.1992', 'Vladimir@confirmit.com',
    [new Comment('Анна', '10:15 17 May 2017', 'Текст комментария №5')], 'Евгения Иванова')
];

const app = express();
app.set('port', 3000);
app.set('view endine', 'ejs');

app.use(favicon(path.join(__dirname, 'public', 'assets', 'img', 'favicon.ico')));
app.use(express.static(path.join(__dirname, 'public')));
app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());

http.createServer(app).listen(app.get('port'), function() {
  console.log('Express server listening on port', app.get('port'));
});

app.get('/candidates', (req, res) => {
  console.log('SET_INITIAL_STATE');
  console.log(candidates);
  res.send(candidates);
});

app.post('/candidates', (req, res) => {
  candidates.push(req.body.candidate);
  console.log('ADD_NEW_CANDIDATE', req.body.candidate);
  console.log(candidates);
});

app.get('/*', (req, res) => {
  res.sendFile(path.join(__dirname, 'public', 'index.html'));
});
