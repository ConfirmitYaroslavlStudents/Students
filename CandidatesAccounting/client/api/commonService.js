import {sendGraphQLQuery} from './graphqlClient';
import {createCandidate} from '../databaseDocumentClasses';

export function getInitialState() {
  return sendGraphQLQuery(
    `query {
      candidates {
        _id
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
      let validCandidate = candidate;
      validCandidate .id = candidate._id;
      candidates.push(createCandidate(validCandidate .status, validCandidate));
    });
    result.candidates = candidates;
    result.tags = data.tags;
    return result;
  });
}