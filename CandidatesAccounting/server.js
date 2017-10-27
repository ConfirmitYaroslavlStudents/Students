const express = require('express');
const http = require('http');
const ntlm = require('express-ntlm');
const {formatDate, formatDateTime} = require('./frontend/moment');
const path = require('path');
const bodyParser  = require('body-parser');
const favicon = require ('serve-favicon');
const {Interviewee, Student, Trainee, Comment} = require('./frontend/candidatesClasses/');

let candidates = [
  new Interviewee(1, 'Олег', formatDate('27', '10', '1995'), 'Oleg@mail.ru',
    [new Comment('Анна', formatDateTime('15', '45', '17', '05', '2017'), 'Текст комментария №1')], [],
    formatDateTime('12', '00', '27', '10', '2017'), {}),
  new Student(2, 'Ольга', formatDate('11', '04', '1997'), 'solnishko14@rambler.com',
    [new Comment('Анна', formatDateTime('15', '45', '17', '05', '2017'), 'Текст комментария №2')], [],
    'КБ-3', formatDate('04', '08', '2017'), formatDate('30', '09', '2017')),
  new Student(3, 'Андрей', formatDate('12', '07', '1997'), 'andrey@gmail.com',
    [new Comment('Анна', formatDateTime('15', '45', '17', '05', '2017'), 'Текст комментария №3')], [],
    'ПМИ-3', formatDate('04', '08', '2017'), formatDate('30', '09', '2017')),
  new Trainee(4, 'Оксана', formatDate('07', '09', '1995'), 'Oksana@confirmit.com',
    [new Comment('Анна', formatDateTime('15', '45', '17', '05', '2017'), 'Текст комментария №4')], [],
    'Евгений Иванов'),
  new Trainee(5, 'Владимир', formatDate('07', '09', '1995'), 'Vladimir@confirmit.com',
    [new Comment('Анна', formatDateTime('15', '45', '17', '05', '2017'), 'Текст комментария №5')], [],
    'Евгения Иванова')
];

const app = express();
app.set('port', 3000);
app.set('view endine', 'ejs');

app.use(favicon(path.join(__dirname, 'public', 'assets', 'img', 'favicon.ico')));
app.use(express.static(path.join(__dirname, 'public')));
app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());
/*app.use(ntlm({
  debug: function() {
    var args = Array.prototype.slice.apply(arguments);
    console.log.apply(null, args);
  },
  domain: 'FIRM',
  domaincontroller: 'ldap://myad.example',
}));
*/
//app.use(ntlm());

http.createServer(app).listen(app.get('port'), function() {
  console.log('Express server listening on port', app.get('port'));
});

app.get('/candidates', (req, res) => {
  console.log('Send all candidates');
  //console.log(req.ntlm);
  res.send(candidates);
});

app.post('/candidates', (req, res) => {
  let lastId = 0;
  for (let i = 0; i < candidates.length; i++) {
    if (candidates[i].id > lastId) {
      lastId = candidates[i].id;
    }
  }
  let candidate = req.body.data;
  candidate.id = lastId + 1;
  candidates.push(candidate);
  console.log('Add new candidate:', candidate);
  res.end();
});

app.put('/candidates/:id', (req, res) => {
  for (let i = 0; i < candidates.length; i++) {
    if (candidates[i].id === parseInt(req.params.id)) {
      console.log('Edit candidate');
      console.log('Previous state:', candidates[i]);
      candidates[i] = req.body.data;
      console.log('New state:', candidates[i]);
      break;
    }
  }
  res.end();
});

app.delete('/candidates/:id', (req, res) => {
  for (let i = 0; i < candidates.length; i++) {
    if (candidates[i].id === parseInt(req.params.id)) {
      console.log('Delete candidate:', candidates[i]);
      candidates.splice(i, 1);
      break;
    }
  }
  res.end();
});

app.post('/candidates/:candidateId/comments', (req, res) => {
  for (let i = 0; i < candidates.length; i++) {
    if (candidates[i].id === parseInt(req.params.candidateId)) {
      candidates[i].comments.push(req.body.data);
      console.log('Add comment:', candidates[i].id, req.body.data);
      break;
    }
  }
  res.end();
});

app.delete('/candidates/:candidateId/comments/:commentId', (req, res) => {
  for (let i = 0; i < candidates.length; i++) {
    if (candidates[i].id === parseInt(req.params.candidateId)) {
      console.log('Delete comment:', candidates[i].id, candidates[i].comments[req.params.commentId]);
      candidates[i].comments.splice(req.params.commentId, 1);
      break;
    }
  }
  res.end();
});

app.get('/*', (req, res) => {
  res.sendFile(path.join(__dirname, 'public', 'index.html'));
});