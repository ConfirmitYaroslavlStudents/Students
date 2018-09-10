import searchCandidates from '../utilities/searchCandidates'
import sortCandidates from '../utilities/sortCandidates'
import { getTags } from '../mongoose/api/tag'
import { deleteNotification, getNotifications, noticeNotification } from '../mongoose/api/account'
import formatCandidateWithCommentsToGraphql from '../utilities/formatCandidateWithCommentsToGraphql'
import formatCandidateToGraphql from '../utilities/formatCandidateToGraphql'
import formatNotificationToGraphql from '../utilities/formatNotificationToGraphql'
import {
  addCandidate,
  addComment,
  deleteCandidate,
  deleteComment,
  getCandidateById,
  getCandidates,
  subscribe,
  unsubscribe,
  updateCandidate
} from '../mongoose/api/candidate'

const root = {
  candidate: ({ id }) => {
    return getCandidateById(id)
    .then(candidate => formatCandidateWithCommentsToGraphql(candidate))
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
        paginatedCandidates.push(formatCandidateToGraphql(candidates[i]))
      }
      return {
        candidates: paginatedCandidates,
        total: totalAmount
      }
    })
  },

  tags: () => {
    return getTags()
  },

  notifications: ({ username }) => {
    if (!username || username.trim() === '') {
      return []
    }
    return getNotifications(username)
    .then(notifications => notifications.map(notification => formatNotificationToGraphql(notification)))
  },

  addCandidate: ({ candidate }) => {
    return addCandidate(candidate)
  },

  updateCandidate: ({ candidate }) => {
    return updateCandidate(candidate.id, candidate)
    .then(result => !!result)
  },

  deleteCandidate: ({ candidateId }) => {
    return deleteCandidate(candidateId)
  },

  addComment: ({ candidateId, comment }) => {
    return addComment(candidateId, comment)
  },

  deleteComment: ({ candidateId, commentId }) => {
    return deleteComment(candidateId, commentId)
    .then(result => !!result)
  },

  subscribe: ({ candidateId, email }) => {
    return subscribe(candidateId, email)
    .then(result => !!result)
  },

  unsubscribe: ({ candidateId, email }) => {
    return unsubscribe(candidateId, email)
    .then(result => !!result)
  },

  noticeNotification: ({ username, notificationId }) => {
    return noticeNotification(username, notificationId)
    .then(result => !!result)
  },

  deleteNotification: ({ username, notificationId }) => {
    return deleteNotification(username, notificationId)
    .then(result => !!result)
  }
}

export default root