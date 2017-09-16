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

function setTempCandidate(candidate) {
  return {
    type: 'SET_TEMP_CANDIDATE',
    candidate
  }
}

function changeTempCandidateInfo(key, value) {
  return {
    type: 'CHANGE_TEMP_CANDIDATE_INFO',
    key,
    value
  }
}

function setTempCandidateComment(index, comment) {
  return {
    type: 'SET_TEMP_CANDIDATE_COMMENT',
    index,
    comment
  }
}

module.exports = {addCandidate, deleteCandidate, editCandidate, setTempCandidate, changeTempCandidateInfo,
  setTempCandidateComment};