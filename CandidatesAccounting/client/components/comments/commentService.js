import {fetchPost, fetchDelete} from '../common/fetch';

export function addComment(candidateId, comment) {
  return fetchPost('/api/candidates/' + candidateId + '/comments', comment);
}

export function deleteComment(candidateId, commentId) {
  return fetchDelete('/api/candidates/' + candidateId + '/comments/' + commentId);
}