import {fetchGet, fetchPost, fetchDelete, fetchPut} from '../components/common/fetch';
import {createCandidate} from '../databaseDocumentClasses';

export function getAllCandidates() {
  return fetchGet('/api/candidates')
    .then((candidates) => {
      let candidatesArray = [];
      for (let i = 0; i < candidates.length; i++) {
        candidatesArray.push(createCandidate(candidates[i].status, candidates[i]));
      }
      return candidatesArray;
    })
}

export function addCandidate(candidate) {
  return fetchPost('/api/candidates', candidate);
}

export function deleteCandidate(id) {
  return fetchDelete('/api/candidates/' + id);
}

export function editCandidate(id, candidateNewState) {
  return fetchPut('/api/candidates/' + id, candidateNewState);
}