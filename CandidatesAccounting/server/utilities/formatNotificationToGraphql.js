const formatNotificationToGraphql = (notification) => {
  notification.id = notification._id.toString()
  delete notification._id
  notification.source.id = notification.source._id.toString()
  delete notification.source._id
  return notification
}

export default formatNotificationToGraphql