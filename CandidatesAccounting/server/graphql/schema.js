import { buildSchema } from 'graphql'

const schema = buildSchema(`    
  input CandidateInput {
    id: String!,
    name: String!,    
    nickname: String,
    status: String,
    phoneNumber: String,
    email: String,
    comments: [CommentInput],
    tags: [String],
    subscribers: [String],
    hasAvatar: Boolean,
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
    nickname: String,
    status: String!,
    phoneNumber: String,
    email: String,
    comments: [Comment]!,
    tags: [String],
    subscribers: [String],
    hasAvatar: Boolean,
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
    nickname: String,
    status: String!,
    phoneNumber: String,
    email: String,
    commentAmount: Int!,
    tags: [String],
    subscribers: [String],
    hasAvatar: Boolean,
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

export default schema