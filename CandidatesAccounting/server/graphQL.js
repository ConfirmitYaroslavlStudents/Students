import {buildSchema} from 'graphql';
import {getAllCandidates, getAllTags, addCandidate, updateCandidate, deleteCandidate, addComment, deleteComment} from './database';

export const schema = buildSchema(`    
  input CandidateInput {
    id: String!,
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
    _id: ID!,
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
    addCandidate(candidate: CandidateInput!): String
    updateCandidate(candidate: CandidateInput!): String
    deleteCandidate(candidateID: ID!): Boolean
    addComment(candidateID: ID!, comment: CommentInput!): Boolean
    deleteComment(candidateID: ID!, comment: CommentInput!): Boolean
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
    return addCandidate(candidate);
  },
  updateCandidate: ({candidate}) => {
    return updateCandidate(candidate)
      .then((candidateIDAfterUpdate) => {
        return candidateIDAfterUpdate;
      });
  },
  deleteCandidate: ({candidateID}) => {
    return deleteCandidate(candidateID)
      .then(() => {
        return true;
      });
  },
  addComment: ({candidateID, comment}) => {
    return addComment(candidateID, comment)
      .then(() => {
        return true;
      });
  },
  deleteComment: ({candidateID, comment}) => {
    return deleteComment(candidateID, comment);
  }
};