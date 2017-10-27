import {fetchGet, fetchPost, fetchDelete, fetchPut} from './fetcher';
import {createCandidate} from './candidatesClasses';

export function getAllCandidates() {
  return fetchGet('/candidates')
    .then((candidates) => {
      let candidatesArray = [];
      for (let i = 0; i < candidates.length; i++) {
        candidatesArray.push(createCandidate(candidates[i].status, candidates[i]));
      }
      return candidatesArray;
    })
}

export function addCandidate(candidate) {
  return fetchPost('/candidates', candidate);
}

export function deleteCandidate(id) {
  return fetchDelete('/candidates/' + id);
}

export function editCandidate(id, candidateNewState) {
  return fetchPut('/candidates/' + id, candidateNewState);
}

module.exports = {getAllCandidates, addCandidate, deleteCandidate, editCandidate};