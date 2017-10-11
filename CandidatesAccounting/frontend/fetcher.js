export function getAllCandidates() {
  return fetch('/candidates')
    .then(function(response) {
      return response.json();
    });
}

module.exports = {getAllCandidates};