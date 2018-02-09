function setInitialState(state) {
  return {
    type: 'SET_INITIAL_STATE',
    state
  }
}

function login(email, password) {
  return {
    type: 'LOGIN',
    email,
    password
  }
}

function loginSuccess(userName) {
  return {
    type: 'LOGIN_SUCCESS',
    userName
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

function deleteComment(candidateID, commentID) {
  return {
    type: 'DELETE_COMMENT',
    candidateID,
    commentID
  }
}

function deleteCommentSuccess(candidateID, commentID) {
  return {
    type: 'DELETE_COMMENT_SUCCESS',
    candidateID,
    commentID
  }
}

function subscribe(candidateID, email) {
  return {
    type: 'SUBSCRIBE',
    candidateID,
    email
  }
}

function subscribeSuccess(candidateID, email) {
  return {
    type: 'SUBSCRIBE_SUCCESS',
    candidateID,
    email
  }
}

function unsubscribe(candidateID, email) {
  return {
    type: 'UNSUBSCRIBE',
    candidateID,
    email
  }
}

function unsubscribeSuccess(candidateID, email) {
  return {
    type: 'UNSUBSCRIBE_SUCCESS',
    candidateID,
    email
  }
}

function noticeNotification(username, notificationID) {
  return {
    type: 'NOTICE_NOTIFICATION',
    username,
    notificationID
  }
}

function noticeNotificationSuccess(username, notificationID) {
  return {
    type: 'NOTICE_NOTIFICATION_SUCCESS',
    username,
    notificationID
  }
}

function deleteNotification(username, notificationID) {
  return {
    type: 'DELETE_NOTIFICATION',
    username,
    notificationID
  }
}

function deleteNotificationSuccess(username, notificationID) {
  return {
    type: 'DELETE_NOTIFICATION_SUCCESS',
    username,
    notificationID
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

module.exports = {
  setInitialState, login, loginSuccess, logout, logoutSuccess, addCandidate, deleteCandidate, updateCandidate, addComment,
  deleteComment, subscribe, subscribeSuccess, unsubscribe, unsubscribeSuccess, noticeNotification, noticeNotificationSuccess,
  setErrorMessage, deleteNotification, deleteNotificationSuccess,
  addCandidateSuccess, deleteCandidateSuccess, updateCandidateSuccess, addCommentSuccess, deleteCommentSuccess, setSearchRequest, search, setPageTitle};