import sendGraphQLQuery from './graphqlClient'

export const getTags = () => {
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