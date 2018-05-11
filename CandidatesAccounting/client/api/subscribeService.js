import sendGraphQLQuery from './graphqlClient'

export function subscribe(candidateId, email) {
  return sendGraphQLQuery(
    `mutation subscribe($candidateId: ID!, $email: String!) {
      subscribe(
        candidateId: $candidateId,
        email: $email
      )
    }`,
    {
      candidateId,
      email
    }
  )
  .then(data => {
    if (!data.subscribe) {
      throw 'Server error'
    }
  })
}

export function unsubscribe(candidateId, email) {
  return sendGraphQLQuery(
    `mutation unsubscribe($candidateId: ID!, $email: String!) {
      unsubscribe(
        candidateId: $candidateId,
        email: $email
      )
    }`,
    {
      candidateId,
      email
    }
  )
  .then(data => {
    if (!data.unsubscribe) {
      throw 'Server error'
    }
  })
}