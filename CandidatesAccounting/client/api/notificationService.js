import sendGraphQLQuery from './graphqlClient'

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
    let notifications = {}
    data.notifications.forEach((notification) => {
      notifications[notification.id] = notification
    })
    return notifications
  })
}

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
      throw 'Server error'
    }
  })
}

export function deleteNotification(username, notificationID) {
  return sendGraphQLQuery(
    `mutation deleteNotification($username: String!, $notificationID: ID!) {
      deleteNotification(
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
    if (!data.deleteNotification) {
      throw 'Server error'
    }
  })
}