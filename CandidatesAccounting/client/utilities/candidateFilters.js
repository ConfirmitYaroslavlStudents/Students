export function searchByStatus(candidates, status) {
  let result = [];
  candidates.forEach((candidate) => {
    if (candidate.status === status) {
      result.push(candidate);
    }
  });
  return result;
}

export function searchById(candidates, id) {
  for (let i = 0; i < candidates.length; i++) {
    if (candidates[i].id === id) {
      return candidates[i];
    }
  }
}

export function searchByTag(candidates, tag) {
  let result = [];
  candidates.forEach((candidate) => {
    if (candidate.tags.includes(tag)) {
      result.push(candidate);
    }
  });
  return result;
}

export function searchByRequest(candidates, searchRequest) {
  const request = searchRequest.toLowerCase();
  let result = [];
  candidates.forEach((candidate) => {
    if (candidate.name.toLowerCase().includes(request)) {
      result.push(candidate);
      return;
    }
    if (candidate.email.toLowerCase().includes(request)) {
      result.push(candidate);
      return;
    }
    if (candidate.mentor && candidate.mentor.toLowerCase().includes(request)) {
      result.push(candidate);
      return;
    }
    for (let i = 0; i < candidate.tags.length; i++) {
      if (candidate.tags[i].toLowerCase().includes(request)) {
        result.push(candidate);
        break;
      }
    }
  });
  return result;
}