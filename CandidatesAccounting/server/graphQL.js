import { buildSchema } from 'graphql'
import searchCandidates from './utilities/searchCandidates'
import sortCandidates from './utilities/sortCandidates'
import {
  getCandidates,
  getCandidateById,
  getAllTags,
  getNotifications,
  addCandidate,
  updateCandidate,
  deleteCandidate,
  addComment,
  deleteComment,
  subscribe,
  unsubscribe,
  noticeNotification,
  deleteNotification
} from './mongoose'

export const schema = buildSchema(`    
  input CandidateInput {
    id: String!,
    name: String!,
    status: String!,
    phoneNumber: String!,
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
  type CandidateWithComments {
    id: ID!,
    name: String!,
    status: String!,
    phoneNumber: String!,
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
  type Candidate {
    id: ID!,
    name: String!,
    status: String!,
    phoneNumber: String!,
    email: String!,
    commentAmount: Int!,
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
    candidate(id: String!): CandidateWithComments,
    candidatesPaginated(first: Int!, offset: Int!, status: String, sort: String, sortDir: String, searchRequest: String): PaginateResult,
    tags: [String],
    notifications(username: String!): [Notification]
  }
  type Mutation {
    addCandidate(candidate: CandidateInput!): String
    updateCandidate(candidate: CandidateInput!): Boolean
    deleteCandidate(candidateId: ID!): Boolean
    addComment(candidateId: ID!, comment: CommentInput!): String
    deleteComment(candidateId: ID!, commentId: ID!): Boolean
    subscribe(candidateId: ID!, email: String!): Boolean
    unsubscribe(candidateId: ID!, email: String!): Boolean
    noticeNotification(username: String!, notificationId: ID!): Boolean
    deleteNotification(username: String!, notificationId: ID!): Boolean
  }
`)

export const root = {
  candidate: ({id}) => {
    return getCandidateById(id)
      .then(candidate => formatCandidateWithComments(candidate))
  },

  candidatesPaginated: ({first, offset, status, sort, sortDir, searchRequest}) => {
    return getCandidates(status !== '' ? status : 'Candidate')
      .then(candidates => {
        let totalAmount = candidates.length
        if (searchRequest && searchRequest.trim() !== '') {
          candidates = searchCandidates(candidates, searchRequest)
          totalAmount = candidates.length
        }
        if (sort && sort !== '' && sortDir && sortDir !== '') {
          candidates = sortCandidates(candidates, sort, sortDir)
        }
        let paginatedCandidates = []
        for (let i = Number(offset); i < Number(offset) + Number(first) && i < candidates.length; i++) {
          paginatedCandidates.push(formatCandidate(candidates[i]))
        }
        return {
          candidates: paginatedCandidates,
          total: totalAmount
        }
      })
  },

  tags: () => {
    return getAllTags()
  },

  notifications: ({username}) => {
    if (!username || username.trim() === '') {
      return []
    }
    return getNotifications(username)
      .then(notifications => notifications.map(notification => formatNotification(notification)))
  },

  addCandidate: ({candidate}) => {
    return addCandidate(candidate)
  },

  updateCandidate: ({candidate}) => {
    return updateCandidate(candidate.id, candidate)
      .then(result => !!result)
  },

  deleteCandidate: ({candidateId}) => {
    return deleteCandidate(candidateId)
  },

  addComment: ({candidateId, comment}) => {
    return addComment(candidateId, comment)
  },

  deleteComment: ({candidateId, commentId}) => {
    return deleteComment(candidateId, commentId)
      .then(result => !!result)
  },

  subscribe: ({candidateId, email}) => {
    return subscribe(candidateId, email)
      .then(result => !!result)
  },

  unsubscribe: ({candidateId, email}) => {
    return unsubscribe(candidateId, email)
      .then(result => !!result)
  },

  noticeNotification: ({username, notificationId}) => {
    return noticeNotification(username, notificationId)
      .then(result => !!result)
  },

  deleteNotification: ({username, notificationId}) => {
    return deleteNotification(username, notificationId)
      .then(result => !!result)
  }
}

function formatCandidate(candidate) {
  candidate.id = candidate._id
  delete candidate._id
  candidate.commentAmount = candidate.comments.length
  delete candidate.comments
  return candidate
}

function formatCandidateWithComments(candidate) {
  candidate.id = candidate._id
  delete candidate._id
  candidate.comments.forEach(comment => {
    comment.id = comment._id
    delete comment._id
  })
  return candidate
}

function formatNotification(notification) {
  notification.id = notification._id
  delete notification._id
  notification.source.id = notification.source._id
  delete notification.source._id
  return notification
}