import {sendGraphQLMutation} from './graphqlClient';

export function subscribe(candidateID, email) {
  return sendGraphQLMutation(
    `mutation subscribe($candidateID: ID!, $email: String!) {
      subscribe(
        candidateID: $candidateID,
        email: $email
      )
    }`,
    {
      candidateID: candidateID,
      email: email
    }
  )
    .then((data) => {
      if (!data.subscribe) {
        throw 'Server error';
      }
    });
}

export function unsubscribe(candidateID, email) {
  return sendGraphQLMutation(
    `mutation unsubscribe($candidateID: ID!, $email: String!) {
      unsubscribe(
        candidateID: $candidateID,
        email: $email
      )
    }`,
    {
      candidateID: candidateID,
      email: email
    }
  )
    .then((data) => {
      if (!data.unsubscribe) {
        throw 'Server error';
      }
    });
}