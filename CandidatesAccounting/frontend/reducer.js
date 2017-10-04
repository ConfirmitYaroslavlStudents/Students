import {Map} from 'immutable';
import {CreateCandidate} from './candidatesClasses';

export default function reducer(state = Map(), action) {
  let candidate;

  switch (action.type) {
    case 'SET_STATE':
      return state.merge(action.state);

    case 'ADD_CANDIDATE':
      let lastId = state.get('candidates').last().id;
      let newCandidate = CreateCandidate(action.candidate.status ? action.candidate.status
                                                                 : action.candidate.constructor.name, action.candidate);
      newCandidate.id = lastId + 1;
      return state.update('candidates', (candidates) => candidates.push(newCandidate));

    case 'DELETE_CANDIDATE':
      return state.update('candidates', (candidates) => candidates.filterNot((candidate) => candidate.id === action.id));

    case 'EDIT_CANDIDATE':
      return state = state.update('candidates', (candidates) => candidates.splice(candidates.indexOf(candidates.find(c =>
        c.id === action.id)),
        1,
        CreateCandidate(action.candidateNewState.status ? action.candidateNewState.status
          : action.candidateNewState.constructor.name, action.candidateNewState)));

    case 'ADD_COMMENT':
      candidate = state.get('candidates').find(c => c.id === action.candidateId);
      candidate.comments.push(action.comment);
      return state = state.update('candidates', (candidates) => candidates.splice(candidates.indexOf(candidates.find(c =>
        c.id === candidate.id)),
        1,
        candidate));

    case 'DELETE_COMMENT':
      candidate = state.get('candidates').find(c => c.id === action.candidateId);
      candidate.comments.splice(action.commentId, 1);
      return state = state.update('candidates', (candidates) => candidates.splice(candidates.indexOf(candidates.find(c =>
        c.id === candidate.id)),
        1,
        candidate));
  }

  return state;
}