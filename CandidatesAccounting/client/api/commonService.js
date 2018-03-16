import sendGraphQLQuery from './graphqlClient';

export function getInitialState(username) {
  return sendGraphQLQuery(
    `query($username: String!) {
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
    }`,
    {username: username}
  )
  .then((data) => {
    if (!data) {
      throw 'Connection error';
    }
    return {
      tags: data.tags,
      notifications: data.notifications
    };
  });
}