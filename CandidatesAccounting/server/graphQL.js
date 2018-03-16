import {buildSchema} from 'graphql';
import {getCandidates, getCandidatesPaginated, getCandidateByID, getAllTags, getNotifications, addCandidate, updateCandidate, deleteCandidate, addComment,
  deleteComment, subscribe, unsubscribe, noticeNotification, deleteNotification} from './mongoose';

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
    id: String!,
    author: String!,
    date: String!,
    text: String!,
    attachment: String
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
    id: ID!,
    author: String!,
    date: String!,
    text: String!,
    attachment: String
  }
  type Notification {
    id: ID!,
    recent: Boolean!,
    source: Candidate!,
    content: Comment!
  }
  type PaginateResult {
    candidates: [Candidate]!,
    total: Int!
  }
  type Query {
    candidates: [Candidate],
    candidate(id: String!): Candidate,
    candidatesPaginated(first: Int!, offset: Int!, status: String, sort: String, sortDir: String, searchRequest: String): PaginateResult,
    tags: [String],
    notifications(username: String!): [Notification]
  }
  type Mutation {
    addCandidate(candidate: CandidateInput!): String
    updateCandidate(candidate: CandidateInput!): Boolean
    deleteCandidate(candidateID: ID!): Boolean
    addComment(candidateID: ID!, comment: CommentInput!): String
    deleteComment(candidateID: ID!, commentID: ID!): Boolean
    subscribe(candidateID: ID!, email: String!): Boolean
    unsubscribe(candidateID: ID!, email: String!): Boolean
    noticeNotification(username: String!, notificationID: ID!): Boolean
    deleteNotification(username: String!, notificationID: ID!): Boolean
  }
`);

export const root = {
  candidates: () => {
    return getCandidates('Candidate')
      .then((result) => {
        return formatCandidates(result);
      });
  },
  candidate: ({id}) => {
    return getCandidateByID(id)
      .then((candidate) => {
        candidate.id = candidate._id;
        delete candidate._id;
        return candidate;
      });
  },
  candidatesPaginated: ({first, offset, status, sort, sortDir, searchRequest}) => {
    if (searchRequest && searchRequest.trim() !== '') {
      return getCandidates(status && status !== '' ? status : 'Candidate', sort, sortDir)
        .then((result) => {
          let validCandidates = search(result, searchRequest);
          let paginatedCandidates = [];
          for (let i = Number(offset); i < Number(offset) + Number(first) && i < validCandidates.length; i++) {
            paginatedCandidates.push(validCandidates[i]);
          }
          return {
            candidates: formatCandidates(paginatedCandidates),
            total: validCandidates.length
          };
        });
    } else {
      return getCandidatesPaginated(offset, first, status && status !== '' ? status : 'Candidate', sort, sortDir)
        .then((result) => {
          return {
            candidates: formatCandidates(result.docs),
            total: result.total
          }
        });
    }
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
    return updateCandidate(candidate.id, candidate)
      .then((result) => {
        return !!result;
      });
  },
  deleteCandidate: ({candidateID}) => {
    return deleteCandidate(candidateID);
  },
  addComment: ({candidateID, comment}) => {
    return addComment(candidateID, comment);
  },
  deleteComment: ({candidateID, commentID}) => {
    return deleteComment(candidateID, commentID)
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
  noticeNotification: ({username, notificationID}) => {
    return noticeNotification(username, notificationID)
      .then((result) => {
        return !!result;
      });
  },
  deleteNotification: ({username, notificationID}) => {
    return deleteNotification(username, notificationID)
      .then((result) => {
        return !!result;
      });
  }
};

function search(candidates, searchRequest) {
  let requestLowerCase = searchRequest.toLowerCase();
  let searchWords = requestLowerCase.split(' ');
  let foundCandidates = [];

  let searchMode = 'and'; //TODO: ask about search mode

  if (searchMode === 'or') {
    searchWords.forEach((searchWord) => {
      if (searchWord !== '') {
        let validCandidates = searchByWord(candidates, searchWord);
        validCandidates.forEach((candidate) => {
          if (!foundCandidates.includes(candidate)) {
            foundCandidates.push(candidate);
          }
        });
      }
    });
  } else { // 'and' mode
    candidates.forEach((candidate) => {
      let candidateIsValid = true;
      for (let i = 0; i < searchWords.length; i++) {
        if (searchWords[i] !== '') {
          if (searchByWord([candidate], searchWords[i]).length === 0) {
            candidateIsValid = false;
            break;
          }
        }
      }
      if (candidateIsValid) {
        foundCandidates.push(candidate);
      }
    });
  }
  return foundCandidates;
}

function searchByWord(candidates, searchWord) {
  let validCandidates = [];
  candidates.forEach((candidate) => {
    if (
      candidate.id === searchWord
      || candidate.name.toLowerCase().includes(searchWord)
      || candidate.status.toLowerCase().includes(searchWord)
      || candidate.email.toLowerCase().includes(searchWord)
      || (candidate.groupName && candidate.groupName.toLowerCase().includes(searchWord))
      || (candidate.mentor && candidate.mentor.toLowerCase().includes(searchWord))
      || candidate.tags.includes(searchWord)
    ) {
      validCandidates.push(candidate);
    }
  });
  return validCandidates;
}

function formatCandidates(candidates) {
  let validCandidates = [];
  candidates.forEach((candidate) => {
    candidate.id = candidate._id;
    delete candidate._id;
    candidate.comments.forEach((comment) => {
      comment.id = comment._id;
      delete comment._id;
    });
    validCandidates.push(candidate);
  });
  return validCandidates;
}