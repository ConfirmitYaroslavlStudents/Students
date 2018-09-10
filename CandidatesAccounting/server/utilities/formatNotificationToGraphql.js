const formatNotificationToGraphql = (notification) => {
  notification.id = notification._id
  delete notification._id
  notification.source.id = notification.source._id
  delete notification.source._id
  return notification
}

export default formatNotificationToGraphql