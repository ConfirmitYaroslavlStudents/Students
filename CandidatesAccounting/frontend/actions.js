function addCandidate(candidate) {
  return {
    type: 'ADD_CANDIDATE',
    candidate
  }
}

function deleteCandidate(id) {
  return {
    type: 'DELETE_CANDIDATE',
    id
  }
}

function editCandidate(id, candidateNewState) {
  return {
    type: 'EDIT_CANDIDATE',
    id,
    candidateNewState
  }
}

function setCandidateEditInfo(candidate) {
  return {
    type: 'SET_CANDIDATE_EDIT_INFO',
    candidate
  }
}

function changeCandidateEditInfo(key, value) {
  return {
    type: 'CHANGE_CANDIDATE_EDIT_INFO',
    key,
    value
  }
}

module.exports = {addCandidate, deleteCandidate, editCandidate, setCandidateEditInfo, changeCandidateEditInfo};