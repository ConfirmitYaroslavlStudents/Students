import Immutable from 'immutable';

export default function reducer(state = Immutable.Map(), action) {
  switch (action.type) {
    case 'SET_STATE':
      return state.merge(action.state);

    case 'SET_APPLICATION_STATUS':
      return state = state.set('applicationStatus', action.status);

    case 'SET_ERROR_MESSAGE':
      return state = state.set('errorMessage', action.message);

    case 'UPDATE_CANDIDATE_SUCCESS':
      return state = state.update('candidates', (candidates) => candidates.map((candidate) => {
        if (candidate.get('id') === action.candidate.id) {
          return Immutable.fromJS(action.candidate);
        } else {
          return candidate;
        }
      }));

    case 'ADD_COMMENT_SUCCESS':
    return state = state.update('candidates', (candidates) => candidates.map((candidate) => {
      if (candidate.get('id') === action.candidateID) {
        return candidate.update('comments', (comments) => comments.push(Immutable.fromJS(action.comment)));
      } else {
        return candidate;
      }
    }));

    case 'DELETE_COMMENT_SUCCESS':
      return state = state.update('candidates', (candidates) => candidates.map((candidate) => {
        if (candidate.get('id') === action.candidateID) {
          return candidate.update('comments', (comments) => comments.filterNot((comment) =>
            comment.get('id') === action.commentID));
        } else {
          return candidate;
        }
      }));

    case 'SUBSCRIBE_SUCCESS':
      return state = state.update('candidates', (candidates) => candidates.map((candidate) => {
        if (candidate.get('id') === action.candidateID) {
          return candidate.update('subscribers', (subscribers) => subscribers.push(Immutable.fromJS(action.email)));
        } else {
          return candidate;
        }
      }));

    case 'UNSUBSCRIBE_SUCCESS':
      return state = state.update('candidates', (candidates) => candidates.map((candidate) => {
        if (candidate.get('id') === action.candidateID) {
          return candidate.update('subscribers', (subscribers) => subscribers.filterNot((email) => email === action.email));
        } else {
          return candidate;
        }
      }));

    case 'NOTICE_NOTIFICATION_SUCCESS':
      return state = state.update('notifications', (notifications) => notifications.map((notification) => {
        if (notification.get('id') === action.notificationID) {
          return notification.update('recent', (recent) => false);
        } else {
          return notification;
        }
      }));

    case 'DELETE_NOTIFICATION_SUCCESS':
      return state.update('notifications', (notifications) => notifications.filterNot((notification) => notification.get('id') === action.notificationID));

    case 'UPLOAD_RESUME_SUCCESS':
      return state = state.update('candidates', (candidates) => candidates.map((candidate) => {
        if (candidate.get('id') === action.intervieweeID) {
          return candidate.update('resume', (resume) => Immutable.fromJS(action.resume));
        } else {
          return candidate;
        }
      }));

    default:
      return state;
  }
}