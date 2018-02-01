function setInitialState() {
  return {
    type: 'SET_INITIAL_STATE'
  }
}

function login(email, password) {
  return {
    type: 'LOGIN',
    email,
    password
  }
}

function loginSuccess(email, password) {
  return {
    type: 'LOGIN_SUCCESS',
    email,
    password
  }
}

function logout() {
  return {
    type: 'LOGOUT'
  }
}

function logoutSuccess() {
  return {
    type: 'LOGOUT_SUCCESS'
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

function deleteCandidate(candidateID) {
  return {
    type: 'DELETE_CANDIDATE',
    candidateID
  }
}

function deleteCandidateSuccess(candidateID) {
  return {
    type: 'DELETE_CANDIDATE_SUCCESS',
    candidateID
  }
}

function updateCandidate(candidate) {
  return {
    type: 'UPDATE_CANDIDATE',
    candidate
  }
}

function updateCandidateSuccess(candidate) {
  return {
    type: 'UPDATE_CANDIDATE_SUCCESS',
    candidate
  }
}

function addComment(candidateID, comment) {
  return {
    type: 'ADD_COMMENT',
    candidateID,
    comment
  }
}

function addCommentSuccess(candidateID, comment) {
  return {
    type: 'ADD_COMMENT_SUCCESS',
    candidateID,
    comment
  }
}

function deleteComment(candidateID, comment) {
  return {
    type: 'DELETE_COMMENT',
    candidateID,
    comment
  }
}

function deleteCommentSuccess(candidateID, comment) {
  return {
    type: 'DELETE_COMMENT_SUCCESS',
    candidateID,
    comment
  }
}

function setErrorMessage(message) {
  return {
    type: 'SET_ERROR_MESSAGE',
    message
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

function setPageTitle(title) {
  return {
    type: 'SET_PAGE_TITLE',
    title
  }
}

module.exports = {setInitialState, login, loginSuccess, logout, logoutSuccess, addCandidate, deleteCandidate, updateCandidate, addComment,
                  deleteComment, setErrorMessage, addCandidateSuccess, deleteCandidateSuccess, updateCandidateSuccess,
                  addCommentSuccess, deleteCommentSuccess, setSearchRequest, search, setPageTitle};