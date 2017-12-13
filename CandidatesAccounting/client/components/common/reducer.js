import {Map} from 'immutable';

export default function reducer(state = Map(), action) {
  let candidate;

  switch (action.type) {
    case 'SET_INITIAL_STATE':
      return state.merge(action.state);

    case 'ADD_CANDIDATE_SUCCESS':
      return state.update('candidates', (candidates) => candidates.push(action.candidate));

    case 'DELETE_CANDIDATE_SUCCESS':
      return state.update('candidates', (candidates) => candidates.filterNot((candidate) => candidate.id === action.candidateID));

    case 'UPDATE_CANDIDATE_SUCCESS':
      return state = state.update('candidates', (candidates) => candidates.splice(candidates.indexOf(candidates.find(c =>
        c.id === action.candidate.id)),
        1,
        action.candidate));

    case 'ADD_COMMENT_SUCCESS':
      candidate = state.get('candidates').find(c => c.id === action.candidateID);
      candidate.comments.push(action.comment);
      return state = state.update('candidates', (candidates) => candidates.splice(candidates.indexOf(candidates.find(c =>
        c.id === candidate.id)),
        1,
        candidate));

    case 'DELETE_COMMENT_SUCCESS':
      candidate = state.get('candidates').find(c => c.id === action.candidateID);
      candidate.comments.splice(action.commentNumber, 1);
      return state = state.update('candidates', (candidates) => candidates.splice(candidates.indexOf(candidates.find(c =>
        c.id === candidate.id)),
        1,
        candidate));

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