import {buildSchema} from 'graphql';
import {getAllCandidates, getAllTags, addCandidate, updateCandidate, deleteCandidate, addComment, deleteComment} from './database';

export const schema = buildSchema(`    
  input CandidateInput {
    id: ID!,
    name: String!,
    status: String!,
    birthDate: String!,
    email: String!,
    comments: [CommentInput]!,
    tags: [String]!,
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
    addCandidate(candidate: CandidateInput!): Boolean
    updateCandidate(id: ID!, candidate: CandidateInput!): Boolean
    deleteCandidate(id: ID!): Boolean
    addComment(candidateID: ID!, comment: CommentInput!): Boolean
    deleteComment(candidateID: ID!, commentNumber: Int!): Boolean
  }
`);

export const root = {
  candidates: () => {
    return getAllCandidates();
  },
  tags: () => {
    return getAllTags();
  },
  addCandidate: ({candidate}) => {
    try {
      addCandidate(candidate);
      console.log('Added new candidate:', candidate);
      return true;
    }
    catch(error) {
      console.log(error);
      return false;
    }
  },
  updateCandidate: ({id, candidate}) => {
    try {
      updateCandidate(id, candidate);
      console.log('Updated candidate: id:', id, 'new state:', candidate);
      return true;
    }
    catch(error) {
      console.log(error);
      return false;
    }
  },
  deleteCandidate: ({id}) => {
    try {
      deleteCandidate(id);
      console.log('Deleted candidate', 'id:', id);
      return true;
      }
    catch(error) {
      console.log(error);
      return false;
    }
  },
  addComment: ({candidateID, comment}) => {
    try {
      addComment(candidateID, comment);
      console.log('Added new comment:', 'id:', candidateID, 'comment:', comment);
      return true;
    }
    catch(error) {
      console.log(error);
      return false;
    }
  },
  deleteComment: ({candidateID, commentNumber}) => {
    try {
      deleteComment(candidateID, commentNumber);
      console.log('Deleted comment:', 'candidateID:', candidateID, 'comment number:', commentNumber);
      return true;
    }
    catch(error) {
      console.log(error);
      return false;
    }
  }
};