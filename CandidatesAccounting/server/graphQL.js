import {buildSchema} from 'graphql';
import {getAllCandidates, getAllTags, getNotifications, addCandidate, updateCandidate, deleteCandidate, addComment, deleteComment, subscribe, unsubscribe, noticeNotification} from './mongoose';

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
  type Notification {
    id: ID!,
    recent: Boolean!,
    source: Candidate!,
    content: Comment!
  }
  type Query {
    candidates: [Candidate],
    tags: [String],
    notifications(username: String!): [Notification]
  }
  type Mutation {
    addCandidate(candidate: CandidateInput!): String
    updateCandidate(candidate: CandidateInput!): Boolean
    deleteCandidate(candidateID: ID!): Boolean
    addComment(candidateID: ID!, comment: CommentInput!): Boolean
    deleteComment(candidateID: ID!, comment: CommentInput!): Boolean
    subscribe(candidateID: ID!, email: String!): Boolean
    unsubscribe(candidateID: ID!, email: String!): Boolean
    noticeNotification(username: String!, notificationID: ID!): Boolean
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
  notifications: ({username}) => {
    if (!username || username.trim() === '') {
      return [];
    }
    return getNotifications(username)
      .then((result) => {
        let notifications = [];
        result.forEach((notification) => {
          notification.id = notification._id;
          delete notification._id;
          notification.source.id = notification.source._id;
          delete notification.source._id;
          notifications.push(notification);
        });
        return notifications;
      });
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
        return !!result;
      });
  },
  noticeNotification: ({username, notificationID}) => {
    return noticeNotification(username, notificationID)
      .then((result) => {
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