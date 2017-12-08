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

export function deleteCandidate(id) {
  return sendGraphQLMutation(
    `mutation deleteCandidate($id: ID!) {
      deleteCandidate(
        id: $id
      )
    }`,
    {id: id}
  )
  .then((data) => {
    if (!data.deleteCandidate) {
      throw 'Server error';
    }
  });
}

export function editCandidate(id, candidateNewState) {
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