import sendGraphQLQuery from './graphqlClient';
import createCandidate from '../utilities/createCandidate';

export function getInitialState(username) {
  return sendGraphQLQuery(
    `query($username: String!) {
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
        }
      }
    }`,
    {username: username}
  )
  .then((data) => {
    if (!data) {
      throw 'Connection error';
    }
    let candidates = [];
    data.candidates.forEach((candidate) => {
      candidates.push(createCandidate(candidate.status, candidate));
    });
    return {
      candidates: candidates,
      tags: data.tags,
      notifications: data.notifications
    };
  });
}