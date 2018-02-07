import {buildSchema} from 'graphql';
import {getAllCandidates, getAllTags, addCandidate, updateCandidate, deleteCandidate, addComment, deleteComment, subscribe, unsubscribe} from './mongoose';

export const schema = buildSchema(`    
  input CandidateInput {
    id: String!,
    name: String!,
    status: String!,
    birthDate: String!,
    email: String!,
    comments: [CommentInput]!,
    tags: [String]!,
    subscribers: [String]!,
    interviewDate: String,
    resume: String,
    groupName: String,
    startingDate: String,
    endingDate: String,
    mentor: String,
  }
  input CommentInput {
    author: String!,
    date: String!,
    text: String!,
  }
  type Candidate {
    id: ID!,
    name: String!,
    status: String!,
    birthDate: String!,
    email: String!,
    comments: [Comment]!,
    tags: [String]!,
    subscribers: [String]!,
    interviewDate: String,
    resume: String,
    groupName: String,
    startingDate: String,
    endingDate: String,
    mentor: String,
  }
  type Comment {
    author: String!,
    date: String!,
    text: String!,
  }
  type Query {
    candidates: [Candidate],
    tags: [String]
  }
  type Mutation {
    addCandidate(candidate: CandidateInput!): String
    updateCandidate(candidate: CandidateInput!): Boolean
    deleteCandidate(candidateID: ID!): Boolean
    addComment(candidateID: ID!, comment: CommentInput!): Boolean
    deleteComment(candidateID: ID!, comment: CommentInput!): Boolean
    subscribe(candidateID: ID!, email: String!): Boolean
    unsubscribe(candidateID: ID!, email: String!): Boolean
  }
`);

export const root = {
  candidates: () => {
    return getAllCandidates()
      .then((result) => {
        let candidates = [];
        result.forEach((candidate) => {
          candidate.id = candidate._id;
          delete candidate._id;
          candidates.push(candidate);
        });
        return candidates;
      });
  },
  tags: () => {
    return getAllTags();
  },
  addCandidate: ({candidate}) => {
    return addCandidate(candidate);
  },
  updateCandidate: ({candidate}) => {
    return updateCandidate(candidate)
      .then((result) => {
        return !!result;
      });
  },
  deleteCandidate: ({candidateID}) => {
    return deleteCandidate(candidateID)
      .then((result) => {
        return !!result;
      });
  },
  addComment: ({candidateID, comment}) => {
    return addComment(candidateID, comment)
      .then((result) => {
        result.subscribers.forEach((subscriber) => {
          if (subscriber !== comment.author) {
            console.log('mailto:', subscriber, '\n', 'message:', '\n', comment);
          }
        });
        return !!result;
      });
  },
  deleteComment: ({candidateID, comment}) => {
    return deleteComment(candidateID, comment)
      .then((result) => {
        return !!result;
      });
  },
  subscribe: ({candidateID, email}) => {
    return subscribe(candidateID, email)
      .then((result) => {
        return !!result;
      });
  },
  unsubscribe: ({candidateID, email}) => {
    return unsubscribe(candidateID, email)
      .then((result) => {
        return !!result;
      });
  },
};