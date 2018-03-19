export default function searchCandidates(candidates, searchRequest) {
  let requestLowerCase = searchRequest.toLowerCase();
  let searchWords = requestLowerCase.split(' ');
  searchWords = searchWords.filter(word => word.length > 0);
  let foundCandidates = [];

  candidates.forEach((candidate) => {
    let candidateIsValid = true;
    for (let i = 0; i < searchWords.length; i++) {
      if (!searchByWord(candidate, searchWords[i])) {
        candidateIsValid = false;
        break;
      }
    }
    if (candidateIsValid) {
      foundCandidates.push(candidate);
    }
  });

  return foundCandidates;
}

function searchByWord(candidate, searchWord) {
  return (
    candidate.id === searchWord
    || candidate.name.toLowerCase().includes(searchWord)
    || candidate.status.toLowerCase().includes(searchWord)
    || candidate.email.toLowerCase().includes(searchWord)
    || (candidate.groupName && candidate.groupName.toLowerCase().includes(searchWord))
    || (candidate.mentor && candidate.mentor.toLowerCase().includes(searchWord))
    || candidate.tags.includes(searchWord)
  );
}