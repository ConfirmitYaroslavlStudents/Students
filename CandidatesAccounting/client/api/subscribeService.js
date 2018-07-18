import sendGraphQLQuery from './graphqlClient'

export const subscribe = (candidateId, email) => {
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

export const unsubscribe = (candidateId, email) => {
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