import React from 'react'
import PropTypes from 'prop-types'
import IconButton from '../common/UIComponentDecorators/iconButton'
import NotificationsIcon from 'material-ui-icons/Notifications'
import NotificationsOffIcon from 'material-ui-icons/NotificationsOff'
import NotificationsActiveIcon from 'material-ui-icons/NotificationsActive'

export default function SubscribeButton(props){
  const buttonIcon =
    props.disabled ?
      <NotificationsIcon/>
      :
      props.active ?
        <NotificationsActiveIcon/>
        :
        <NotificationsOffIcon/>

  const onClick = () => {
    if (props.active) {
      props.unsubscribe(props.candidate.id, props.username);
    } else {
      props.subscribe(props.candidate.id, props.username);
    }
  }

  return (
    <IconButton
      icon={buttonIcon}
      disabled={props.disabled}
      onClick={onClick}
    />
  )
}

SubscribeButton.propTypes = {
  active: PropTypes.bool.isRequired,
  candidate: PropTypes.object.isRequired,
  username: PropTypes.string.isRequired,
  subscribe: PropTypes.func.isRequired,
  unsubscribe: PropTypes.func.isRequired,
  disabled: PropTypes.bool,
}