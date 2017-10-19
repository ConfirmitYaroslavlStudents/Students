function setInitialState() {
  return {
    type: 'SET_INITIAL_STATE'
  }
}

function setInitialStateSuccess() {
  return {
    type: 'SET_INITIAL_STATE_SUCCESS'
  }
}

function addCandidate(candidate) {
  return {
    type: 'ADD_CANDIDATE',
    candidate
  }
}

function addCandidateSuccess(candidate) {
  return {
    type: 'ADD_CANDIDATE_SUCCESS',
    candidate
  }
}

function deleteCandidate(id) {
  return {
    type: 'DELETE_CANDIDATE',
    id
  }
}

function deleteCandidateSuccess(id) {
  return {
    type: 'DELETE_CANDIDATE_SUCCESS',
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

function editCandidateSuccess(id, candidateNewState) {
  return {
    type: 'EDIT_CANDIDATE_SUCCESS',
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

function addCommentSuccess(candidateId, comment) {
  return {
    type: 'ADD_COMMENT_SUCCESS',
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

function deleteCommentSuccess(candidateId, commentId) {
  return {
    type: 'DELETE_COMMENT_SUCCESS',
    candidateId,
    commentId
  }
}

function serviceFailure(message) {
  return {
    type: 'SERVICE_FAILURE',
    message
  }
}

module.exports = {setInitialState, addCandidate, deleteCandidate, editCandidate, addComment, deleteComment, serviceFailure,
                  setInitialStateSuccess, addCandidateSuccess, deleteCandidateSuccess, editCandidateSuccess,
                  addCommentSuccess, deleteCommentSuccess};