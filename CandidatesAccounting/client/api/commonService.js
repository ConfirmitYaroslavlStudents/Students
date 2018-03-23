import sendGraphQLQuery from './graphqlClient';
import createCandidate from '../utilities/createCandidate';

export function getInitialState(username, take, skip, status, sort, sortDir, searchRequest) {
  return sendGraphQLQuery(
    `query($username: String!, $first: Int!, $offset: Int!, $status: String, $sort: String, $sortDir: String, $searchRequest: String) {
      tags
      notifications(username: $username) {
    		id
        recent
        source {
          id
          name
          status
        }
        content {
          date
          author
          text
          attachment
        }
      }
      candidatesPaginated(first: $first, offset: $offset, status: $status, sort: $sort, sortDir: $sortDir, searchRequest: $searchRequest) {
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
        total
      }  
    }`,
    {
      username: username,
      first: take,
      offset: skip,
      status: status,
      sort: sort,
      sortDir: sortDir === 'desc' ? 'asc' : 'desc',
      searchRequest: searchRequest
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
      tags: data.tags,
      notifications: data.notifications,
      candidates: candidates,
      total: data.candidatesPaginated.total
    };
  });
}