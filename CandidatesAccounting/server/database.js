import {Interviewee, Student, Trainee, Comment} from '../client/databaseDocumentClasses';
import mongodb from 'mongodb';

const url = 'mongodb://localhost:27017/CandidateAccounting';
const MongoClient = mongodb.MongoClient;

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
  return MongoClient.connect(url)
    .then((db) => {
      return Promise.all([
        db.collection('interviewees').find().toArray(),
        db.collection('students').find().toArray(),
        db.collection('trainees').find().toArray()
      ])
    })
    .then((results) => {
      let allCandidates = [];
      results.forEach((candidates) => {
          candidates.forEach((candidate) => {
            allCandidates.push(candidate);
          });
      });
      return allCandidates;
    });
}

export function getAllTags() {
  return MongoClient.connect(url)
    .then((db) => {
      return db.collection('tags').find().toArray();
    })
    .then((result) => {
      let tags = [];
      result.forEach((tag) => {
        tags.push(tag.name);
      });
      return tags;
    });
}

export function addCandidate(newCandidate) {
  return MongoClient.connect(url)
    .then((db) => {
      switch(newCandidate.status) {
        case 'Interviewee':
          return db.collection('interviewees').insertOne(newCandidate);
        case 'Student':
          return db.collection('students').insertOne(newCandidate);
        case 'Trainee':
          return db.collection('trainees').insertOne(newCandidate);
      }
    })
    .then((result) => {
      console.log(result.ops[0]._id);
      return result.ops[0]._id;
    });
}

export function updateCandidate(candidate) {
  return MongoClient.connect(url)
    .then((db) => {
      console.log('db', candidate);
      switch(candidate.status) {
        case 'Interviewee':
          return db.collection('interviewees').replaceOne({_id: candidate.id}, candidate);
        case 'Student':
          return db.collection('students').replaceOne({_id: candidate.id}, candidate);
        case 'Trainee':
          return db.collection('trainees').replaceOne({_id: candidate.id}, candidate);
      }
    })
    .then(() => {
      return true;
    });
}

export function deleteCandidate(id) {
  return MongoClient.connect(url)
    .then((db) => {
      console.log('db', id);
       return db.collection('interviewees').deleteOne({_id: id});
    })
    .then(() => {
      return true;
    });
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