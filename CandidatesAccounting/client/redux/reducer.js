import Immutable from 'immutable';

export default function reducer(state = Immutable.Map(), action) {
  switch (action.type) {
    case 'SET_STATE':
      return state.merge(action.state);

    case 'SET_APPLICATION_STATUS':
      return state = state.set('applicationStatus', action.status);

    case 'SET_PAGE_TITLE':
      return state = state.set('pageTitle', action.title);

    case 'SET_ERROR_MESSAGE':
      return state = state.set('errorMessage', action.message);

    case 'SET_CANDIDATE_STATUS':
      return state.set('candidateStatus', action.status);

    case 'SET_OFFSET':
      return state.set('offset', action.offset);

    case 'SET_CANDIDATES_PER_PAGE':
      return state.set('candidatesPerPage', action.candidatesPerPage);

    case 'SET_SORTING_FIELD':
      return state.set('sortingField', action.field);

    case 'SET_SORTING_DIRECTION':
      return state.set('sortingDirection', action.direction);

    case 'SET_SEARCH_REQUEST':
      return state = state.set('searchRequest', action.searchRequest);

    case 'ADD_CANDIDATE_SUCCESS':
      return state.update('candidates', (candidates) => candidates.push(Immutable.fromJS(action.candidate)));

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

    default:
      return state;
  }
}