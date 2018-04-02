export default function reducer(state, action) {
  let candidates = {};
  let notifications = {};

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
      state.candidates[action.candidate.id] = action.candidate;
      return state;

    case 'ADD_COMMENT_SUCCESS':
      candidates = { ...state.candidates };
      candidates[action.candidateID].comments[action.comment.id] = action.comment;
      return {
        ...state,
        candidates : candidates
      };

    case 'DELETE_COMMENT_SUCCESS':
      candidates = { ...state.candidates };
      delete candidates[action.candidateID].comments[action.commentID];
      return {
        ...state,
        candidates: candidates
      };

    case 'SUBSCRIBE_SUCCESS':
      candidates = { ...state.candidates };
      candidates[action.candidateID].subscribers[action.email] = action.email;
      return {
        ...state,
        candidates: candidates
      };

    case 'UNSUBSCRIBE_SUCCESS':
      candidates = { ...state.candidates };
      delete candidates[action.candidateID].subscribers[action.email];
      return {
        ...state,
        candidates: candidates
      };

    case 'NOTICE_NOTIFICATION_SUCCESS':
      notifications = { ...state.notifications };
      notifications[action.notificationID].recent = false;
      return {
        ...state,
        notifications: notifications
      };

    case 'DELETE_NOTIFICATION_SUCCESS':
      notifications = { ...state.notifications };
      delete notifications[action.notificationID];
      return {
        ...state,
        notifications: notifications
      };

    case 'UPLOAD_RESUME_SUCCESS':
      candidates = { ...state.candidates };
      candidates[action.intervieweeID].resume = action.resume;
      return {
        ...state,
        candidates: candidates
      };

    default:
      return state;
  }
}