import {fetchPost, fetchDelete} from './fetcher';

export function addComment(candidateId, comment) {
  return fetchPost('/candidates/' + candidateId + '/comments', comment);
}

export function deleteComment(candidateId, commentId) {
  return fetchDelete('/candidates/' + candidateId + '/comments/' + commentId);
}

module.exports = {addComment, deleteComment};