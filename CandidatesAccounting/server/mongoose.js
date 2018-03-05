import mongoose from 'mongoose';
import passportLocalMongoose from 'passport-local-mongoose';
import mongoosePaginate from 'mongoose-paginate';
import {AccountSchema, CandidateSchema, IntervieweeSchema, StudentSchema, TraineeSchema, TagSchema} from './schemas';
import streamifer from 'streamifier';

let gridFs = null;

mongoose.Promise = Promise;

export function connect() {
  return mongoose.connect('mongodb://localhost:27017/CandidateAccounting', {
    useMongoClient: true
  }).then((connection) => {
    gridFs = require('mongoose-gridfs')({
      collection: 'attachments',
      model: 'Attachment',
      mongooseConnection: connection
    });
  });
}

export function disconnect(error) {
  mongoose.disconnect();
  if(error) {
    return console.log(error);
  }
}

AccountSchema.plugin(passportLocalMongoose);
CandidateSchema.plugin(mongoosePaginate);
IntervieweeSchema.plugin(mongoosePaginate);
StudentSchema.plugin(mongoosePaginate);
TraineeSchema.plugin(mongoosePaginate);
export const Account = mongoose.model('Account', AccountSchema, 'accounts');
const Candidate = mongoose.model('Candidate', CandidateSchema, 'candidates');
const Interviewee = mongoose.model('Interviewee', IntervieweeSchema, 'candidates');
const Student = mongoose.model('Student', StudentSchema, 'candidates');
const Trainee = mongoose.model('Trainee', TraineeSchema, 'candidates');
const Tag = mongoose.model('Tag', TagSchema, 'tags');

function identifyModel(status) {
  switch (status) {
    case 'Interviewee':
      return Interviewee;
    case 'Student':
      return Student;
    case 'Trainee':
      return Trainee;
    default:
      return Candidate;
  }
}

export function getCandidates(status, sort, sortDir) {
  if (sort !== '' && sortDir !== '') {
    let sortSettings = {};
    sortSettings[sort] = sortDir;
    return identifyModel(status).find(status === 'Candidate' ? {} : {status: status}).sort(sortSettings).exec();
  } else {
    return identifyModel(status).find(status === 'Candidate' ? {} : {status: status}).exec();
  }
}

export function getCandidatesPaginated(offset, limit, status, sort, sortDir) {
  if (sort !== '' && sortDir !== '') {
    let sortSettings = {};
    sortSettings[sort] = sortDir;
    return identifyModel(status).paginate(status === 'Candidate' ? {} : {status: status}, {offset: offset, limit: limit, sort: sortSettings});
  } else {
    return identifyModel(status).paginate(status === 'Candidate' ? {} : {status: status}, {offset: offset, limit: limit});
  }
}

export function getCandidateByID(id) {
  return Candidate.findById(mongoose.Types.ObjectId(id)).exec();
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
  return identifyModel(newCandidate.status).create(newCandidate)
    .then((result) => {
    console.log(result._id);
      updateTags(result.tags);
      return result._id;
    });
}

export function updateCandidate(candidate) {
  return identifyModel(candidate.status).replaceOne({_id: candidate.id}, candidate)
    .then(() => {
      updateTags(candidate.tags);
      return candidate;
    });
}

export function deleteCandidate(candidateID) {
  return Candidate.findByIdAndRemove(candidateID).exec();
}

export function addComment(candidateID, comment) {
  const id = mongoose.Types.ObjectId();
  comment._id = id;
  return Candidate.findByIdAndUpdate(candidateID, {$push: {comments: comment}}).exec()
    .then((result) => {
      result.subscribers.forEach((subscriber) => {
        if (subscriber !== comment.author) {
          addNotification(result, subscriber, comment);
        }
      });
      return id;
    });
}

export function deleteComment(candidateID, commentID) {
  return Candidate.findByIdAndUpdate(candidateID, {$pull: {comments: {_id: commentID}}}).exec();
}

export function subscribe(candidateID, email) {
  return Candidate.findByIdAndUpdate(candidateID, {$push: {subscribers: email}}).exec();
}

export function unsubscribe(candidateID, email) {
  return Candidate.findByIdAndUpdate(candidateID, {$pull: {subscribers: email}}).exec();
}

export function noticeNotification(username, notificationID) {
  return Account.updateOne({username: username, 'notifications._id': notificationID}, {$set: {'notifications.$.recent': false}}).exec();
}

export function deleteNotification(username, notificationID) {
  return Account.updateOne({username: username}, {$pull: {notifications: {_id: notificationID}}}).exec();
}

export function getResume(intervieweeID) {
  return getCandidateByID(intervieweeID)
    .then((interviewee) => {
      return {
        resumeName: interviewee.resume,
        resumeData: interviewee.resumeFile
      };
    });
}

export function addResume(intervieweeID, resumeName, resumeData) {
  return getCandidateByID(intervieweeID)
    .then((interviewee) => {
      interviewee.resume = resumeName;
      interviewee.resumeFile = resumeData;
      return updateCandidate(interviewee);
    });
}

export function getAttachment(attachmentID) {
  return new Promise((resolve, reject) => {
    gridFs.model.readById(attachmentID, function(error, content){
      if (error) {
        reject(error);
      }
      resolve(content);
    });
  });
}

export function addAttachment(fileName, fileData) {
  return new Promise((resolve, reject) => {
    gridFs.model.write({
        filename: fileName,
        contentType: 'multipart/form-data'
      },
      streamifer.createReadStream(fileData),
      function(error, createdFile){
        if (error) {
          reject(error);
        }
        resolve(createdFile);
      });
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
  Tag.create(tags);
}

function addNotification(source, recipient, notification) {
  Account.findOneAndUpdate({username: recipient}, {$push: {notifications: {recent: true, source: source, content: notification}}}).exec();
}