import {Map} from 'immutable';

export default function reducer(state = Map(), action) {
switch (action.type) {
  case 'SET_STATE':
    return state.merge(action.state);

  case 'ADD_CANDIDATE':
    let lastId = state.get('candidates').last().id;
    let newCandidate = action.candidate;
    newCandidate.id = lastId + 1;
    return state.update('candidates', (candidates) => candidates.push(newCandidate));

  case 'DELETE_CANDIDATE':
    return state.update('candidates', (candidates) => candidates.filterNot((candidate) => candidate.id === action.id));

  case 'EDIT_CANDIDATE':
    return state = state.update('candidates', (candidates) => candidates.splice(candidates.indexOf(candidates.find(c =>
      c.id === action.id)),
      1,
      action.candidateNewState));

  case 'SET_TEMP_CANDIDATE':
    let candidate = action.candidate;
    candidate.status = action.candidate.constructor.name;
    return state.update('tempCandidate', (candidateEditInfo) => candidate);

  case 'CHANGE_TEMP_CANDIDATE_INFO':
    let newCandidateEditInfo = state.get('tempCandidate');
    newCandidateEditInfo[action.key] = action.value;
    return state.update('tempCandidate', () => newCandidateEditInfo);

  case 'SET_TEMP_CANDIDATE_COMMENT':
    let tempCandidate = state.get('tempCandidate');
    if (action.index >= tempCandidate.comments.length) {
      tempCandidate.comments.push(action.comment);
    } else {
      tempCandidate.comments[action.index] = action.comment;
    }
    return state.update('tempCandidate', () => tempCandidate);
}
return state;
}