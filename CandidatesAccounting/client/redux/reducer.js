export default function reducer(state, action) {
  let candidates = [];
  let notifications = [];

  switch (action.type) {
    case 'SET_STATE':
      return {
        ...state,
        ...action.state
      };

    case 'SET_APPLICATION_STATUS':
      return {
        ...state,
        applicationStatus: action.status
      };

    case 'SET_ERROR_MESSAGE':
      return {
        ...state,
        errorMessage: action.message
      };

    case 'UPDATE_CANDIDATE_SUCCESS':
      for (let i = 0; i < state.candidates.length; i++) {
        if (state.candidates[i].id === action.candidate.id) {
          state.candidates[i] = action.candidate;
          break;
        }
      }
      return state;

    case 'ADD_COMMENT_SUCCESS':
      for (let i = 0; i < state.candidates.length; i++) {
        if (state.candidates[i].id === action.candidateID) {
          state.candidates[i].comments.push(action.comment);
          break;
        }
      }
      return {
        ...state,
      };

    case 'DELETE_COMMENT_SUCCESS':
      candidates = state.candidates.slice();
      for (let i = 0; i < candidates.length; i++) {
        if (candidates[i].id === action.candidateID) {
          for (let j = 0; j < candidates[i].comments.length; j++) {
            if (candidates[i].comments[j] === action.commentID) {
              candidates[i].comments.splice(j, 1);
              i = candidates.length;
              break;
            }
          }
        }
      }
      return {
        ...state,
        candidates: candidates
      };

    case 'SUBSCRIBE_SUCCESS':
      candidates = state.candidates.slice();
      for (let i = 0; i < candidates.length; i++) {
        if (candidates[i].id === action.candidateID) {
          candidates[i].subscribers.push(action.email);
          break;
        }
      }
      return {
        ...state,
        candidates: candidates
      };

    case 'UNSUBSCRIBE_SUCCESS':
      candidates = state.candidates.slice();
      for (let i = 0; i < candidates.length; i++) {
        if (candidates[i].id === action.candidateID) {
          for (let j = 0; j < candidates[i].subscribers.length; j++) {
            if (candidates[i].subscribers[j] === action.email) {
              candidates[i].subscribers.splice(j, 1);
              i = candidates.length;
              break;
            }
          }
        }
      }
      return {
        ...state,
        candidates: candidates
      };

    case 'NOTICE_NOTIFICATION_SUCCESS':
      notifications = state.notifications.slice();
      for (let i = 0; i < notifications.length; i++) {
        if (notifications[i].id === action.notificationID) {
          notifications[i].recent = false;
          break;
        }
      }
      return {
        ...state,
        notifications: notifications
      };

    case 'DELETE_NOTIFICATION_SUCCESS':
      notifications = state.notifications.slice();
      for (let i = 0; i < notifications.length; i++) {
        if (notifications[i].id === action.notificationID) {
          notifications.splice(i, 1);
          break;
        }
      }
      return {
        ...state,
        notifications: notifications
      };

    case 'UPLOAD_RESUME_SUCCESS':
      candidates = state.candidates.slice();
      for (let i = 0; i < candidates.length; i++) {
        if (candidates[i].id === action.intervieweeID) {
          candidates[i].resume = action.resume;
          break;
        }
      }
      return {
        ...state,
        candidates: candidates
      };

    default:
      return state;
  }
}