export function getAllCandidates() {
  return fetch('/candidates')
    .then(function(response) {
      if (response.status !== 200) {
        alert('Error: ' + response.status);
      } else {
        return response.json();
      }
    })
}

export function addCandidate(candidate) {
  fetch("/candidates",
    {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({candidate: candidate})
    })
    .then(function(response){ return response; })
    .then(function(response){
      if (response.status !== 200) {
        alert('Error: ' + response.status);
      }
    })
    .catch( alert );
}

export function deleteCandidate(id) {
  fetch("/candidates/" + id,
    {
      method: "DELETE"
    })
    .then(function(response){ return response; })
    .then(function(response){
      if (response.status !== 200) {
        alert('Error: ' + response.status);
      }
    })
    .catch( alert );
}

export function editCandidate(id, candidateNewState) {
  fetch("/candidates/" + id,
    {
      method: "PUT",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({candidate: candidateNewState})
    })
    .then(function(response){ return response; })
    .then(function(response){
      if (response.status !== 200) {
        alert('Error: ' + response.status);
      }
    })
    .catch( alert );
}

export function addComment(candidateId, comment) {
  fetch("/candidates/" + candidateId + '/comments',
    {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({comment: comment})
    })
    .then(function(response){ return response; })
    .then(function(response){
      if (response.status !== 200) {
        alert('Error: ' + response.status);
      }
    })
    .catch( alert );
}

export function deleteComment(candidateId, commentId) {
  fetch("/candidates/" + candidateId + '/comments/' + commentId,
    {
      method: "DELETE"
    })
    .then(function(response){ return response; })
    .then(function(response){
      if (response.status !== 200) {
        alert('Error: ' + response.status);
      }
    })
    .catch( alert );
}

module.exports = {getAllCandidates, addCandidate, deleteCandidate, editCandidate, addComment, deleteComment};