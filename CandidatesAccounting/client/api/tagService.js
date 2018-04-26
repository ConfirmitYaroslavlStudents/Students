import sendGraphQLQuery from './graphqlClient'

export function getTags() {
  return sendGraphQLQuery(
    `query {
      tags
    }`
  )
  .then(({ tags }) => {
    if (!tags) {
      throw 'Connection error'
    }
    return tags
  })
}