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

export function deleteCandidate(candidateID, candidateStatus) {
  return sendGraphQLMutation(
    `mutation deleteCandidate($candidateID: ID!, $candidateStatus: String!) {
      deleteCandidate(
        candidateID: $candidateID,
        candidateStatus: $candidateStatus
      )
    }`,
    {
      candidateID: candidateID,
      candidateStatus: candidateStatus
    }
  )
  .then((data) => {
    if (!data.deleteCandidate) {
      throw 'Server error';
    }
  });
}

export function updateCandidate(candidateNewState) {
  return sendGraphQLMutation(
    `mutation updateCandidate($candidate: CandidateInput!) {
      updateCandidate(
        candidate: $candidate
      )
    }`,
    {
      candidate: candidateNewState
    }
  )
  .then((data) => {
    if (!data.updateCandidate) {
      throw 'Server error';
    }
  });
}