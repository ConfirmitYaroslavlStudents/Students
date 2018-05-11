import sendGraphQLQuery from './graphqlClient'
import Candidate from '../utilities/candidate'
import convertArrayToDictionary from '../utilities/convertArrayToDictionary'

export function getCandidates(take, skip, status, sort, sortDir, searchRequest) {
  return sendGraphQLQuery(
    `query($first: Int!, $offset: Int!, $status: String, $sort: String, $sortDir: String, $searchRequest: String) {
      candidatesPaginated(first: $first, offset: $offset, status: $status, sort: $sort, sortDir: $sortDir, searchRequest: $searchRequest) {
        candidates {
          id
          name
          status
          birthDate
          email
          commentAmount
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
      sort,
      sortDir: sortDir === 'desc' ? 'asc' : 'desc',
      searchRequest
    }
  )
  .then(data => {
    if (!data) {
      throw 'Connection error'
    }
    let candidates = data.candidatesPaginated.candidates.map(candidate => new Candidate(candidate.status, candidate))
    candidates = convertArrayToDictionary(candidates)
    return {
      candidates,
      total: data.candidatesPaginated.total
    }
  })
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
          attachment
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
    { id }
  )
  .then(data => {
    if (!data) {
      throw 'Connection error'
    }
    return {
      candidate: new Candidate(data.candidate.status, data.candidate),
      comments: convertArrayToDictionary(data.candidate.comments)
    }
  })
}

export function addCandidate(candidate) {
  return sendGraphQLQuery(
    `mutation addCandidate($candidate: CandidateInput!) {
      addCandidate(
        candidate: $candidate
      )
    }`,
    { candidate: convertToGraphQLType(candidate) }
  )
  .then(data => {
    if (!data.addCandidate) {
      throw 'Server error'
    } else {
      return data.addCandidate
    }
  })
}

export function updateCandidate(candidate) {
  return sendGraphQLQuery(
    `mutation updateCandidate($candidate: CandidateInput!) {
      updateCandidate(
        candidate: $candidate
      )
    }`,
    { candidate: convertToGraphQLType(candidate) }
  )
  .then(data => {
    if (!data.updateCandidate) {
      throw 'Server error'
    }
  })
}

export function deleteCandidate(candidateId) {
  return sendGraphQLQuery(
    `mutation deleteCandidate($candidateId: ID!) {
      deleteCandidate(
        candidateId: $candidateId
      )
    }`,
    { candidateId }
  )
  .then((data) => {
    if (!data.deleteCandidate) {
      throw 'Server error'
    }
  })
}

function convertToGraphQLType(candidate) {
  delete candidate.commentAmount
  return {
    ...candidate,
    comments: Object.keys(candidate.comments).map(commentID => candidate.comments[commentID]),
    subscribers: Object.keys(candidate.subscribers).map(email => email)
  }
}