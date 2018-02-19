import sendGraphQLQuery from './graphqlClient';
import createCandidate from '../utilities/createCandidate';

export function getCandidates(take, skip, status, sort, sortDir) {
  return sendGraphQLQuery(
    `query($first: Int!, $offset: Int!, $status: String, $sort: String, $sortDir: String) {
      candidatesPaginated(first: $first, offset: $offset, status: $status, sort: $sort, sortDir: $sortDir) {
        candidates {
          id
          name
          status
          birthDate
          email
          comments {
            id
            author
            date
            text
          }
          tags
          subscribers
          interviewDate
          resume
          groupName
          startingDate
          endingDate
          mentor
        }
        total
      }      
    }`,
    {
      first: take,
      offset: skip,
      status: status,
      sort: sort,
      sortDir: sortDir === 'desc' ? 'asc' : 'desc'
    }
  )
    .then((data) => {
      if (!data) {
        throw 'Connection error';
      }
      let candidates = [];
      data.candidatesPaginated.candidates.forEach((candidate) => {
        candidates.push(createCandidate(candidate.status, candidate));
      });
      return {
        candidates: candidates,
        total: data.candidatesPaginated.total
      };
    });
}

export function getCandidate(id) {
  return sendGraphQLQuery(
    `query($id: String!) {
      candidate(id: $id) {
        id
        name
        status
        birthDate
        email
        comments {
          id
          author
          date
          text
        }
        tags
        subscribers
        interviewDate
        resume
        groupName
        startingDate
        endingDate
        mentor
      }      
    }`,
    {
      id: id,
    }
  )
    .then((data) => {
      if (!data) {
        throw 'Connection error';
      }
      return createCandidate(data.candidate.status, data.candidate);
    });
}

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