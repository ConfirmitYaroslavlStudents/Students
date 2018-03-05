function setState(state) {
  return {
    type: 'SET_STATE',
    state
  }
}

function setApplicationStatus(status) {
  return {
    type: 'SET_APPLICATION_STATUS',
    status
  }
}

function setPageTitle(title) {
  return {
    type: 'SET_PAGE_TITLE',
    title
  }
}

function setSearchRequest(searchRequest) {
  return {
    type: 'SET_SEARCH_REQUEST',
    searchRequest
  }
}

function setErrorMessage(message) {
  return {
    type: 'SET_ERROR_MESSAGE',
    message
  }
}

function setCandidateStatus(status) {
  return {
    type: 'SET_CANDIDATE_STATUS',
    status
  }
}

function setOffset(offset) {
  return {
    type: 'SET_OFFSET',
    offset
  }
}

function setCandidatesPerPage(candidatesPerPage) {
  return {
    type: 'SET_CANDIDATES_PER_PAGE',
    candidatesPerPage
  }
}

function setSortingField(field) {
  return {
    type: 'SET_SORTING_FIELD',
    field
  }
}

function setSortingDirection(direction) {
  return {
    type: 'SET_SORTING_DIRECTION',
    direction
  }
}

function loadCandidates() {
  return {
    type: 'LOAD_CANDIDATES'
  }
}

function changeURL(browserHistory) {
  return {
    type: 'CHANGE_URL',
    browserHistory
  }
}

function getCandidate(id) {
  return {
    type: 'GET_CANDIDATE',
    id
  }
}

function uploadResume(intervieweeID, resume) {
  return {
    type: 'UPLOAD_RESUME',
    intervieweeID,
    resume
  }
}

function login(email, password) {
  return {
    type: 'LOGIN',
    email,
    password
  }
}

function logout() {
  return {
    type: 'LOGOUT'
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

function updateCandidate(candidate) {
  return {
    type: 'UPDATE_CANDIDATE',
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

module.exports = {
  setState, login, logout, addCandidate, deleteCandidate, updateCandidate, addComment,
  deleteComment, subscribe, subscribeSuccess, unsubscribe, unsubscribeSuccess, noticeNotification, noticeNotificationSuccess,
  setErrorMessage, deleteNotification, deleteNotificationSuccess, loadCandidates, changeURL, getCandidate, setSortingField, setSortingDirection,
  addCandidateSuccess, addCommentSuccess, deleteCommentSuccess, setSearchRequest, uploadResume,
  setPageTitle, setApplicationStatus, setCandidateStatus, setOffset, setCandidatesPerPage
};