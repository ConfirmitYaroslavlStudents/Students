function setInitialState() {
  return {
    type: 'SET_INITIAL_STATE'
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

function setErrorMessage(message) {
  return {
    type: 'SET_ERROR_MESSAGE',
    message
  }
}

function setUserName(userName) {
  return {
    type: 'SET_USERNAME',
    userName
  }
}

function setSearchRequest(searchRequest, browserHistory, delay) {
  return {
    type: 'SET_SEARCH_REQUEST',
    searchRequest,
    browserHistory,
    delay
  }
}

function search(searchRequest, browserHistory) {
  return {
    type: 'SEARCH',
    searchRequest,
    browserHistory
  }
}

module.exports = {setInitialState, addCandidate, deleteCandidate, editCandidate, addComment, deleteComment, setErrorMessage,
                  addCandidateSuccess, deleteCandidateSuccess, editCandidateSuccess, addCommentSuccess, deleteCommentSuccess,
                  setUserName, setSearchRequest, search};