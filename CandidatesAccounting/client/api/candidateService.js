import sendGraphQLQuery from './graphqlClient'
import Candidate from '../utilities/candidate'
import convertArrayToDictionary from '../utilities/convertArrayToDictionary'

export const getCandidates = (take, skip, status, sort, sortDir, searchRequest) => {
  return sendGraphQLQuery(
    `query($first: Int!, $offset: Int!, $status: String, $sort: String, $sortDir: String, $searchRequest: String) {
      candidatesPaginated(first: $first, offset: $offset, status: $status, sort: $sort, sortDir: $sortDir, searchRequest: $searchRequest) {
        candidates {
          id
          name
          nickname
          status
          phoneNumber
          email
          commentAmount
          tags
          subscribers
          hasAvatar
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
      status,
      sort,
      sortDir,
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

export const getCandidate = (id) => {
  return sendGraphQLQuery(
    `query($id: String!) {
      candidate(id: $id) {
        id
        name
        nickname
        status
        phoneNumber
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
        hasAvatar
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

export const addCandidate = (candidate) => {
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

export const updateCandidate = (candidate) => {
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

export const deleteCandidate = (candidateId) => {
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

const convertToGraphQLType = (candidate) => {
  const convertedCadidate = {
    ...candidate,
    comments: Object.keys(candidate.comments).map(commentID => candidate.comments[commentID]),
    subscribers: Object.keys(candidate.subscribers).map(email => email)
  }
  delete convertedCadidate.commentAmount
  return convertedCadidate
}