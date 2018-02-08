import sendGraphQLQuery from './graphqlClient';

export function addCandidate(candidate) {
  return sendGraphQLQuery(
    `mutation addCandidate($candidate: CandidateInput!) {
      addCandidate(
        candidate: $candidate
      )
    }`,
    {candidate: candidate}
  )
  .then((data) => {
    if (!data.addCandidate) {
      throw 'Server error';
    } else {
      return data.addCandidate;
    }
  });
}

export function updateCandidate(candidate) {
  return sendGraphQLQuery(
    `mutation updateCandidate($candidate: CandidateInput!) {
      updateCandidate(
        candidate: $candidate
      )
    }`,
    {candidate: candidate}
  )
    .then((data) => {
      if (!data.updateCandidate) {
        throw 'Server error';
      }
    });
}

export function deleteCandidate(candidateID) {
  return sendGraphQLQuery(
    `mutation deleteCandidate($candidateID: ID!) {
      deleteCandidate(
        candidateID: $candidateID
      )
    }`,
    {candidateID: candidateID}
  )
  .then((data) => {
    if (!data.deleteCandidate) {
      throw 'Server error';
    }
  });
}