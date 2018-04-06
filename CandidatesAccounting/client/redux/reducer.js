export default function reducer(state, action) {
  switch (action.type) {
    case 'SET_STATE':
      return {
        ...state,
        ...action.state
      }

    case 'SET_APPLICATION_STATUS':
      return {
        ...state,
        applicationStatus: action.status
      }

    case 'SET_ERROR_MESSAGE':
      return {
        ...state,
        errorMessage: action.message
      }

    case 'UPDATE_CANDIDATE_SUCCESS':
      return {
        ...state,
        candidates: {
          ...state.candidates,
          [action.candidate.id] : {
            ...state.candidates[action.candidate.id],
            ...action.candidate
          }
        }
      }

    case 'ADD_COMMENT_SUCCESS':
      return {
        ...state,
        candidates: {
          ...state.candidates,
          [action.candidateID] : {
            ...state.candidates[action.candidateID],
            comments: {
              ...state.candidates[action.candidateID].comments,
              [action.comment.id]: action.comment,
            }
          }
        }
      }

    case 'DELETE_COMMENT_SUCCESS':
      let comments = { ...state.candidates[action.candidateID].comments }
      delete comments[action.commentID]
      return {
        ...state,
        candidates: {
          ...state.candidates,
          [action.candidateID]: {
            ...state.candidates[action.candidateID],
            comments: comments
          }
        }
      }

    case 'SUBSCRIBE_SUCCESS':
      return {
        ...state,
        candidates: {
          ...state.candidates,
          [action.candidateID]: {
            ...state.candidates[action.candidateID],
            subscribers: {
              ...state.candidates[action.candidateID].subscribers,
              [action.email]: action.email
            }
          }
        }
      }

    case 'UNSUBSCRIBE_SUCCESS':
      let subscribers = { ...state.candidates[action.candidateID].subscribers }
      delete subscribers[action.email]
      return {
        ...state,
        candidates: {
          ...state.candidates,
          [action.candidateID]: {
            ...state.candidates[action.candidateID],
            subscribers: subscribers
          }
        }
      }

    case 'NOTICE_NOTIFICATION_SUCCESS':
      return {
        ...state,
        notifications: {
          ...state.notifications,
          [action.notificationID]: {
            ...state.notifications[action.notificationID],
            recent: false
          }
        }
      }

    case 'DELETE_NOTIFICATION_SUCCESS':
      let notifications = { ...state.notifications };
      delete notifications[action.notificationID];
      return {
        ...state,
        notifications: notifications
      }

    case 'UPLOAD_RESUME_SUCCESS':
      return {
        ...state,
        candidates: {
          ...state.candidates,
          [action.intervieweeID]: {
            ...state.candidates[action.intervieweeID],
            resume: action.resume
          }
        }
      }

    default:
      return state
  }
}