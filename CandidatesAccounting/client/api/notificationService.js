import sendGraphQLQuery from './graphqlClient'
import convertArrayToDictinary from '../utilities/convertArrayToDictionary'

export function getNotifications(username) {
  return sendGraphQLQuery(
    `query($username: String!) {
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
    { username }
  )
  .then((data) => {
    if (!data) {
      throw 'Connection error'
    }
    return convertArrayToDictinary(data.notifications)
  })
}

export function noticeNotification(username, notificationId) {
  return sendGraphQLQuery(
    `mutation noticeNotification($username: String!, $notificationId: ID!) {
      noticeNotification(
        username: $username,
        notificationId: $notificationId
      )
    }`,
    {
      username,
      notificationId
    }
  )
  .then(data => {
    if (!data.noticeNotification) {
      throw 'Server error'
    }
  })
}

export function deleteNotification(username, notificationId) {
  return sendGraphQLQuery(
    `mutation deleteNotification($username: String!, $notificationId: ID!) {
      deleteNotification(
        username: $username,
        notificationId: $notificationId
      )
    }`,
    {
      username,
      notificationId
    }
  )
  .then(data => {
    if (!data.deleteNotification) {
      throw 'Server error'
    }
  })
}