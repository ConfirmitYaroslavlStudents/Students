function addCandidate(candidate) {
  return {
    type: 'ADD_CANDIDATE',
    candidate
  }
}

function deleteCandidate(id) {
  return {
    type: 'DELETE_CANDIDATE',
    id
  }
}

function editCandidate(id, candidateNewState) {
  return {
    type: 'EDIT_CANDIDATE',
    id,
    candidateNewState
  }
}

function addComment(candidateId, comment) {
  return {
    type: 'ADD_COMMENT',
    candidateId,
    comment
  }
}

function deleteComment(candidateId, commentId) {
  return {
    type: 'DELETE_COMMENT',
    candidateId,
    commentId
  }
}

module.exports = {addCandidate, deleteCandidate, editCandidate, addComment, deleteComment};