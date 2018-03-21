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

function setErrorMessage(message) {
  return {
    type: 'SET_ERROR_MESSAGE',
    message
  }
}

function loadCandidates(stateChanges, browserHistory) {
  return {
    type: 'LOAD_CANDIDATES',
    stateChanges,
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

function uploadResumeSuccess(intervieweeID, resume) {
  return {
    type: 'UPLOAD_RESUME_SUCCESS',
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

function addCandidate(candidate, browserHistory) {
  return {
    type: 'ADD_CANDIDATE',
    candidate,
    browserHistory
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

function updateCandidateSuccess(candidate) {
  return {
    type: 'UPDATE_CANDIDATE_SUCCESS',
    candidate
  }
}

function addComment(candidateID, comment, commentAttachment) {
  return {
    type: 'ADD_COMMENT',
    candidateID,
    comment,
    commentAttachment
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
  uploadResumeSuccess, updateCandidateSuccess, setErrorMessage, deleteNotification, deleteNotificationSuccess, loadCandidates, getCandidate,
  addCommentSuccess, deleteCommentSuccess, uploadResume,
  setApplicationStatus,
};