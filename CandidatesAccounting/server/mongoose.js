import mongoose from 'mongoose';
import passportLocalMongoose from 'passport-local-mongoose';
import {AccountSchema, CandidateSchema, IntervieweeSchema, StudentSchema, TraineeSchema, TagSchema} from './schemas';

mongoose.Promise = Promise;

export function connect() {
  return mongoose.connect('mongodb://localhost:27017/CandidateAccounting', {
    useMongoClient: true
  });
}

export function disconnect(error) {
  mongoose.disconnect();
  if(error) {
    return console.log(error);
  }
}

connect();

AccountSchema.plugin(passportLocalMongoose);
export const Account = mongoose.model('Account', AccountSchema, 'accounts');
const Candidate = mongoose.model('Candidate', CandidateSchema, 'candidates');
const Interviewee = mongoose.model('Interviewee', IntervieweeSchema, 'candidates');
const Student = mongoose.model('Student', StudentSchema, 'candidates');
const Trainee = mongoose.model('Trainee', TraineeSchema, 'candidates');
const Tag = mongoose.model('Tag', TagSchema, 'tags');

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
  return Candidate.find({}).exec();
}

export function getAllTags() {
  return Tag.find({}).exec()
    .then((result) => {
      let tags = [];
      result.forEach((tag) => {
        tags.push(tag.title);
      });
      return tags;
    });
}

export function getNotifications(username) {
  return Account.findOne({username: username}).exec()
    .then((account) => {
      return account.notifications;
    });
}

export function addCandidate(newCandidate) {
  return identifyModel(newCandidate).create(newCandidate)
    .then((result) => {
    console.log(result._id);
      updateTags(result.tags);
      return result._id;
    });
}

export function updateCandidate(candidate) {
  return identifyModel(candidate).replaceOne({_id: candidate.id}, candidate)
    .then(() => {
      updateTags(candidate.tags);
      return candidate;
    });
}

export function deleteCandidate(candidateID) {
  return Candidate.findByIdAndRemove(candidateID).exec();
}

export function addComment(candidateID, comment) {
  return Candidate.findByIdAndUpdate(candidateID, {$push: {comments: comment}}).exec()
    .then((result) => {
      result.subscribers.forEach((subscriber) => {
        if (subscriber !== comment.author) {
          addNotification(result, subscriber, comment);
        }
      });
      return result;
    });
}

export function deleteComment(candidateID, comment) {
  return Candidate.findByIdAndUpdate(candidateID, {$pull: {comments: {author: comment.author, date: comment.date, text: comment.text}}}).exec();
}

export function subscribe(candidateID, email) {
  return Candidate.findByIdAndUpdate(candidateID, {$push: {subscribers: email}}).exec();
}

export function unsubscribe(candidateID, email) {
  return Candidate.findByIdAndUpdate(candidateID, {$pull: {subscribers: email}}).exec();
}

export function noticeNotification(username, notificationID) {
  return Account.updateOne({username: username, 'notifications._id': notificationID}, {'$set': {'notifications.$.recent': false}}).exec();
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
  Tag.create(tags);
}

function addNotification(source, recipient, notification) {
  Account.findOneAndUpdate({username: recipient}, {$push: {notifications: {recent: true, source: source, content: notification}}}).exec();
}