export default function searchCandidates(candidates, searchRequest) {
  const searchWords = searchRequest.split(' ').filter(word => word.length > 0)

  const foundCandidates = []

  candidates.forEach((candidate) => {
    let candidateIsValid = true
    for (let i = 0; i < searchWords.length; i++) {
      if (!searchByWord(candidate, searchWords[i])) {
        candidateIsValid = false
        break
      }
    }
    if (candidateIsValid) {
      foundCandidates.push(candidate)
    }
  })

  return foundCandidates
}

function searchByWord(candidate, searchWord) {
  let lowerCasedSearchWord = searchWord.toLowerCase()
  return (
    candidate.id === searchWord
    || candidate.name.toLowerCase().includes(lowerCasedSearchWord)
    || candidate.nickname && candidate.nickname.toLowerCase().includes(lowerCasedSearchWord)
    || candidate.status.toLowerCase().includes(lowerCasedSearchWord)
    || candidate.email.toLowerCase().includes(lowerCasedSearchWord)
    || (candidate.groupName && candidate.groupName.toLowerCase().includes(lowerCasedSearchWord))
    || (candidate.mentor && candidate.mentor.toLowerCase().includes(lowerCasedSearchWord))
    || candidate.tags.includes(searchWord)
  )
}