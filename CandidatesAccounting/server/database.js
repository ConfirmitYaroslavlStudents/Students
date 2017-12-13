import mongodb from 'mongodb';

const url = 'mongodb://localhost:27017/CandidateAccounting';
const MongoClient = mongodb.MongoClient;

export function getAllCandidates() {
  return MongoClient.connect(url)
    .then((db) => {
      return db.collection('candidates').find().toArray();
    })
    .then((candidates) => {
      return candidates;
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
      return db.collection("candidates").insertOne(newCandidate);
    })
    .then((result) => {
      return result.ops[0]._id;
    });
}

export function updateCandidate(candidate) {
  return MongoClient.connect(url)
    .then((db) => {
      return db.collection("candidates").replaceOne({_id: mongodb.ObjectId(candidate.id)}, candidate)
        .then(() => {
          return candidate.id;
        });
    });
}

export function deleteCandidate(candidateID) {
  return MongoClient.connect(url)
    .then((db) => {
      return db.collection("candidates").deleteOne({_id: mongodb.ObjectId(candidateID)});
    });
}

export function addComment(candidateID, comment) {
  return MongoClient.connect(url)
    .then((db) => {
      return db.collection("candidates")
        .updateOne({_id: mongodb.ObjectId(candidateID)}, {$push: {comments: comment}});
    });
}

export function deleteComment(candidateID, comment) {
  return MongoClient.connect(url)
    .then((db) => {
      return db.collection("candidates")
        .updateOne({_id: mongodb.ObjectId(candidateID)}, {$pull: {comments: {author: comment.author, date: comment.date, text: comment.text}}});
    });
}

function updateTags(newTags) {
}