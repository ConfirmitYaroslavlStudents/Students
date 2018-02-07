import {sendGraphQLMutation} from './graphqlClient';

export function addCandidate(candidate) {
  return sendGraphQLMutation(
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
  return sendGraphQLMutation(
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
  return sendGraphQLMutation(
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