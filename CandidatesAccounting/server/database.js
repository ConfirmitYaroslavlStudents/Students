import {Interviewee, Student, Trainee, Comment} from '../client/databaseDocumentClasses';

let database = {
  candidates: [
    new Interviewee(1, 'Олег', '27.10.1995', 'Oleg@mail.ru',
      [new Comment('AnnaR', '15:45 17.05.2017', 'Текст комментария №1'),
       new Comment('AnnaR', '17:40 17.05.2017', 'Текст комментария №6')], ['backend', 'javascript', 'nodeJS'],
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

export function getAllCandidates() {
  return database.candidates;
}

export function getAllTags() {
  return database.tags;
}

export function addCandidate(newCandidate) {
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

export function updateCandidate(id, candidateNewState) {
  for (let i = 0; i < database.candidates.length; i++) {
    if (parseInt(database.candidates[i].id) === parseInt(id)) {
      database.candidates[i] = candidateNewState;
      console.log(database.candidates);
      updateTags(database.candidates[i]);
      return;
    }
  }
  throw 'Update candidate error. Candidate not found.';
}

export function deleteCandidate(id) {
  for (let i = 0; i < database.candidates.length; i++) {
    if (parseInt(database.candidates[i].id) === parseInt(id)) {
      database.candidates.splice(i, 1);
      return;
    }
  }
  throw 'Delete candidate error. Candidate not found.';
}

export function addComment(candidateID, comment) {
  for (let i = 0; i < database.candidates.length; i++) {
    if (parseInt(database.candidates[i].id) === parseInt(candidateID)) {
      database.candidates[i].comments.push(comment);
      return;
    }
  }
  throw 'Add comment error. Candidate not found.';
}

export function deleteComment(candidateID, commentNumber) {
  for (let i = 0; i < database.candidates.length; i++) {
    if (parseInt(database.candidates[i].id) === parseInt(candidateID)) {
      database.candidates[i].comments.splice(parseInt(commentNumber), 1);
      return;
    }
  }
  throw 'Delete comment error. Candidate or comment not found.';
}

function updateTags(newTags) {
  for(let i = 0; i < newTags.length; i++) {
    if (!database.tags.includes(newTags[i])) {
      database.tags.push(newTags[i]);
    }
  }
}