export default function SortCandidates(candidates, sortingField, sortingDirection) {
  const sortResult = sortingDirection === 'desc' ? 1 : -1
  return candidates.sort((a, b) => {
    if (!a[sortingField] || a[sortingField] === null) {
      return sortResult
    }
    if (!b[sortingField] || b[sortingField] === null) {
      return -sortResult
    }
    switch (sortingField) {
      case 'learningStart':
        return sortResult * sortByDate(a[sortingField], b[sortingField])
      case 'learningEnd':
        return sortResult * sortByDate(a[sortingField], b[sortingField])
      case 'interviewDate':
        return sortResult * sortByDateTime(a[sortingField], b[sortingField])
      default:
        return sortResult * sortByAlphabet(a[sortingField], b[sortingField])
    }
  })
}

function sortByAlphabet(a, b) {
  if (a.toLowerCase() > b.toLowerCase()) {
    return 1
  } else {
    if (a.toLowerCase() < b.toLowerCase()) {
      return -1
    } else {
      return 0
    }
  }
}

function sortByDate(a, b) {
  const aDate = a.split('.')
  const aResult = Number(aDate[2])*10000 + Number(aDate[1])*100 + Number(aDate[0])

  const bDate = b.split('.')
  const bResult = Number(bDate[2])*10000 + Number(bDate[1])*100 + Number(bDate[0])

  if (aResult > bResult) {
    return 1
  } else {
    if (aResult < bResult) {
      return -1
    } else {
      return 0
    }
  }
}

function sortByDateTime(a, b) {
  const aSplited = a.split(' ')
  const aTime = aSplited[0].split(':')
  const aDate = aSplited[1].split('.')
  const aResult = Number(aDate[2])*100000000 + Number(aDate[1])*1000000 + Number(aDate[0])*10000 + Number(aTime[0])*100 + Number(aTime[1])

  const bSplited = b.split(' ')
  const bTime = bSplited[0].split(':')
  const bDate = bSplited[1].split('.')
  const bResult = Number(bDate[2])*100000000 + Number(bDate[1])*1000000 + Number(bDate[0])*10000 + Number(bTime[0])*100 + Number(bTime[1])

  if (aResult > bResult) {
    return 1
  } else {
    if (aResult < bResult) {
      return -1
    } else {
      return 0
    }
  }
}
