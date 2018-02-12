import Immutable from 'immutable';

export default function reducer(state = Immutable.Map(), action) {
  switch (action.type) {
    case 'SET_INITIAL_STATE':
      return state.merge(action.state);

    case 'LOGIN_SUCCESS':
      state = state.set('userName', action.userName);
      return state = state.set('authorizationStatus', 'authorized');

    case 'LOGOUT_SUCCESS':
      state = state.set('userName', '');
      return state = state.set('authorizationStatus', 'not-authorized');

    case 'ADD_CANDIDATE_SUCCESS':
      return state.update('candidates', (candidates) => candidates.push(Immutable.fromJS(action.candidate)));

    case 'DELETE_CANDIDATE_SUCCESS':
      return state.update('candidates', (candidates) => candidates.filterNot((candidate) => candidate.get('id') === action.candidateID));

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

    case 'SET_ERROR_MESSAGE':
      return state = state.set('errorMessage', action.message);

    case 'SET_SEARCH_REQUEST':
      return state = state.set('searchRequest', action.searchRequest);

    case 'SET_PAGE_TITLE':
      return state = state.set('pageTitle', action.title);

    case 'SET_STATUS':
      return state = state.set('status', action.status);

    default:
      return state;
  }
}