import {Map} from 'immutable';

export default function reducer(state = Map(), action) {
  switch (action.type) {
    case 'SET_INITIAL_STATE':
      return state.merge(action.state);

    case 'ADD_CANDIDATE_SUCCESS':
      return state.update('candidates', (candidates) => candidates.push(action.candidate));

    case 'DELETE_CANDIDATE_SUCCESS':
      return state.update('candidates', (candidates) => candidates.filterNot((candidate) => candidate.id === action.candidateID));

    case 'UPDATE_CANDIDATE_SUCCESS':
      return state = state.update('candidates', (candidates) => candidates.map((candidate) => {
        if (candidate.id === action.candidate.id) {
          return action.candidate;
        } else {
          return candidate;
        }
      }));

    case 'ADD_COMMENT_SUCCESS':
      return state = state.update('candidates', (candidates) => candidates.map((candidate) => {
        if (candidate.id === action.candidateID) {
          candidate.comments.push(action.comment);
          return candidate;
        } else {
          return candidate;
        }
      }));

    case 'DELETE_COMMENT_SUCCESS':
      return state = state.update('candidates', (candidates) => candidates.map((candidate) => {
        if (candidate.id === action.candidateID) {
          for (let i = 0; i < candidate.comments.length; i++) {
            if (candidate.comments[i].author === action.comment.author &&
              candidate.comments[i].date === action.comment.date &&
              candidate.comments[i].text === action.comment.text) {
              candidate.comments.splice(i, 1);
              break;
            }
          }
          return candidate;
        } else {
          return candidate;
        }
      }));

    case 'SET_ERROR_MESSAGE':
      return state = state.set('errorMessage', action.message);

    case 'SET_USERNAME':
      return state = state.set('userName', action.userName);

    case 'SET_SEARCH_REQUEST':
      return state = state.set('searchRequest', action.searchRequest);

    default:
      return state;
  }
}