import {sendGraphQLQuery} from './graphqlClient';
import {createCandidate} from '../databaseDocumentClasses';

export function getInitialState() {
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
      tags
    }`
  )
  .then((data) => {
    let result = {};
    let candidates = [];
    data.candidates.forEach((candidate) => {
      candidates.push(createCandidate(candidate.status, candidate));
    });
    result.candidates = candidates;
    result.tags = data.tags;
    return result;
  });
}