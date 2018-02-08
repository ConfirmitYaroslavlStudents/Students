import sendGraphQLQuery from './graphqlClient';

export function noticeNotification(username, notificationID) {
  return sendGraphQLQuery(
    `mutation noticeNotification($username: String!, $notificationID: ID!) {
      noticeNotification(
        username: $username,
        notificationID: $notificationID
      )
    }`,
    {
      username: username,
      notificationID: notificationID
    }
  )
    .then((data) => {
      if (!data.noticeNotification) {
        throw 'Server error';
      }
    });
}