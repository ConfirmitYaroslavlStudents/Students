const express = require('express');
const http = require('http');
const ntlm = require('express-ntlm');
const path = require('path');
const bodyParser  = require('body-parser');
const favicon = require ('serve-favicon');
const {Interviewee, Student, Trainee, Comment} = require('./frontend/candidatesClasses/');

let candidates = [
  new Interviewee(1, 'Олег', '27.10.1995', 'Oleg@mail.ru',
    [new Comment('AnnaR', '15:45 17.05.2017', 'Текст комментария №1')], ['backend', 'javascript', 'nodeJS'],
    '12:00 27.10.2017', 'resume.pdf'),
  new Student(2, 'Ольга', '11.04.1997', 'solnishko14@rambler.com',
    [new Comment('AnnaR', '15:45 17.05.2017', 'Текст комментария №2')], ['backend', 'C#', 'ASP.NET'],
    'КБ-3', '04.08.2017', '30.09.2017'),
  new Student(3, 'Андрей', '12.07.1997', 'andrey@gmail.com',
    [new Comment('AnnaR', '15:45 17.05.2017', 'Текст комментария №3')], ['frontend', 'C#', 'react', 'javascript'],
    'ПМИ-3', '04.08.2017', '30.09.2017'),
  new Trainee(4, 'Оксана', '07.09.1995', 'Oksana@confirmit.com',
    [new Comment('AnnaR', '15:45 17.05.2017', 'Текст комментария №4')], ['frontend', 'javascript', 'react', 'hub'],
    'Евгений Иванов'),
  new Trainee(5, 'Владимир','07.09.1995', 'Vladimir@confirmit.com',
    [new Comment('AnnaR', '15:45 17.05.2017', 'Текст комментария №5')], ['backend', 'C#', 'ASP.NET', 'es'],
    'Евгения Иванова')
];

let tags = [
  'backend', 'C#', 'javascript', 'frontend', 'react', 'ASP.NET', 'nodeJS', 'hub', 'es'
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
}));*/

//app.use(ntlm());

http.createServer(app).listen(app.get('port'), function() {
  console.log('Express server is listening on port', app.get('port'));
});

app.get('/candidates', (req, res) => {
  console.log('Send all candidates');
  //console.log(req.ntlm);
  res.send(candidates);
});

app.get('/tags', (req, res) => {
  console.log('Send all tags');
  res.send(tags);
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
  updateTags(candidate.tags);
  console.log('Add new candidate:', candidate);
  res.end();
});

app.put('/candidates/:id', (req, res) => {
  for (let i = 0; i < candidates.length; i++) {
    if (candidates[i].id === parseInt(req.params.id)) {
      console.log('Edit candidate');
      console.log('Previous state:', candidates[i]);
      let candidate = req.body.data;
      candidates[i] = candidate;
      updateTags(candidate.tags);
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

function updateTags(newTags) {
  for(let i = 0; i < newTags.length; i++) {
    if (!tags.includes(newTags[i])) {
      tags.push(newTags[i]);
    }
  }
}