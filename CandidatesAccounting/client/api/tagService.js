import {sendGraphQLQuery} from './graphqlClient';

export function getTags() {
  return sendGraphQLQuery(
    `query {
      tags
    }`
  )
  .then((data) => {
    let tagsArray = [];
    for (let i = 0; i < data.tags.length; i++) {
      tagsArray.push(data.tags[i]);
    }
    return tagsArray;
  })
}