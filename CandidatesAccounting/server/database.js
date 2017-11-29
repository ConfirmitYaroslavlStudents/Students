import {Interviewee, Student, Trainee, Comment} from '../client/databaseDocumentClasses';

let database = {
  candidates: [
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
  ],
  tags: [
    'backend', 'C#', 'javascript', 'frontend', 'react', 'ASP.NET', 'nodeJS', 'hub', 'es'
  ]
};

function addCandidate(newCandidate) {
  let lastId = 0;
  database.candidates.forEach((candidate) => {
    if (candidate.id > lastId) {
      lastId = candidate.id;
    }
  });
  newCandidate.id = lastId + 1;
  database.candidates.push(newCandidate);
  updateTags(newCandidate.tags);
}

function updateCandidate(id, candidateNewState) {
  for (let i = 0; i < database.candidates.length; i++) {
    if (database.candidates[i].id === id) {
      database.candidates[i] = candidateNewState;
      updateTags(database.candidates[i]);
      break;
    }
  }
}

function deleteCandidate(id) {
  for (let i = 0; i < database.candidates.length; i++) {
    if (database.candidates[i].id === id) {
      database.candidates.splice(i, 1);
      break;
    }
  }
}

function addComment(candidateID, comment) {
  for (let i = 0; i < database.candidates.length; i++) {
    if (database.candidates[i].id === candidateID) {
      database.candidates[i].comments.push(comment);
      break;
    }
  }
}

function deleteComment(candidateID, commentID) {
  for (let i = 0; i < database.candidates.length; i++) {
    if (database.candidates[i].id === candidateID) {
      database.candidates[i].comments.splice(commentID, 1);
      break;
    }
  }
}

function updateTags(newTags) {
  for(let i = 0; i < newTags.length; i++) {
    if (!database.tags.includes(newTags[i])) {
      database.tags.push(newTags[i]);
    }
  }
}

module.exports = {database, addCandidate, updateCandidate, deleteCandidate, addComment, deleteComment};