import mongoose from 'mongoose';
import {CandidateSchema, IntervieweeSchema, StudentSchema, TraineeSchema, TagSchema} from "./schemas";

mongoose.Promise = Promise;

const Candidate = mongoose.model('Candidate', CandidateSchema, 'candidates');
const Interviewee = mongoose.model('Interviewee', IntervieweeSchema, 'candidates');
const Student = mongoose.model('Student', StudentSchema, 'candidates');
const Trainee = mongoose.model('Trainee', TraineeSchema, 'candidates');
const Tag = mongoose.model('Tag', TagSchema, 'tags');

function connect() {
  return mongoose.connect('mongodb://localhost:27017/CandidateAccounting', {
    useMongoClient: true
  });
}

function disconnect(error) {
  mongoose.disconnect();
  if(error) {
    return console.log(error);
  }
}

function identifyModel(candidate) {
  switch (candidate.status) {
    case 'Interviewee':
      return Interviewee;
    case 'Student':
      return Student;
    case 'Trainee':
      return Trainee;
  }
}

export function getAllCandidates() {
  return connect()
    .then(() => {
      return Candidate.find({}).exec(disconnect)
    });
}

export function getAllTags() {
  return connect()
    .then(() => {
      return Tag.find({}).exec(disconnect)
    })
    .then((result) => {
      let tags = [];
      result.forEach((tag) => {
        tags.push(tag.title);
      });
      return tags;
    });
}

export function addCandidate(newCandidate) {
  return connect()
    .then(() => {
      return identifyModel(newCandidate).create(newCandidate, disconnect)
    })
    .then((result) => {
      updateTags(result.tags);
      return result._id;
    });
}

export function updateCandidate(candidate) {
  return connect()
    .then(() => {
      return identifyModel(candidate).replaceOne({_id: candidate.id}, candidate).exec(disconnect)
    })
    .then(() => {
      updateTags(candidate.tags);
      return candidate;
    });
}

export function deleteCandidate(candidateID) {
  return connect()
    .then(() => {
      return Candidate.findByIdAndRemove(candidateID).exec(disconnect);
    });
}

export function addComment(candidateID, comment) {
  return connect()
    .then(() => {
      return Candidate.findByIdAndUpdate(candidateID, {$push: {comments: comment}}).exec(disconnect);
    });
}

export function deleteComment(candidateID, comment) {
  return connect()
    .then(() => {
      return Candidate.findByIdAndUpdate(candidateID,
        {$pull: {comments: {author: comment.author, date: comment.date, text: comment.text}}}).exec(disconnect);
    });
}

function updateTags(probablyNewTags) {
  getAllTags()
    .then((tags) => {
      let tagsToAdd = [];
      probablyNewTags.forEach((tag) => {
        if (!tags.includes(tag)) {
          tagsToAdd.push({title: tag});
        }
      });
      if (tagsToAdd.length > 0) {
        addTags(tagsToAdd);
      }
    });
}

function addTags(tags) {
  connect()
    .then(() => {
      Tag.create(tags, disconnect);
    });
}