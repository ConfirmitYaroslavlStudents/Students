import {sendGraphQLQuery, sendGraphQLMutation} from './graphqlClient';
import {createCandidate} from '../databaseDocumentClasses';

export function getAllCandidates() {
  return sendGraphQLQuery(
    `query {
      candidates {
        id
        name
        status
        birthDate
        email
        comments {
          author
          date
          text
        }
        tags
        interviewDate
        resume
        groupName
        startingDate
        endingDate
        mentor
      }
    }`
  )
  .then((data) => {
    let candidatesArray = [];
    data.candidates.forEach((candidate) => {
      candidatesArray.push(createCandidate(candidate.status, candidate));
    });
    return candidatesArray;
  });
}

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
    `mutation updateCandidate($id: ID!, $candidate: CandidateInput!) {
      updateCandidate(
        id: $id
        candidate: $candidate
      )
    }`,
    {
      id: id,
      candidate: candidateNewState
    }
  )
  .then((data) => {
    if (!data.updateCandidate) {
      throw 'Server error';
    }
  });
}