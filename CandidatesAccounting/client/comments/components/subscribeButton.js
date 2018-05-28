import React from 'react'
import PropTypes from 'prop-types'
import IconButton from '../../commonComponents/UIComponentDecorators/iconButton'
import NotificationsIcon from '@material-ui/icons/Notifications'
import NotificationsOffIcon from '@material-ui/icons/NotificationsOff'
import NotificationsActiveIcon from '@material-ui/icons/NotificationsActive'

export default function SubscribeButton(props){
  const buttonIcon =
    props.disabled ?
      <NotificationsIcon/>
      :
      props.active ?
        <NotificationsActiveIcon/>
        :
        <NotificationsOffIcon/>

  const handleClick = () => {
    if (props.active) {
      props.unsubscribe()
    } else {
      props.subscribe()
    }
  }

  return (
    <IconButton
      icon={buttonIcon}
      disabled={props.disabled}
      onClick={handleClick}
    />
  )
}

SubscribeButton.propTypes = {
  active: PropTypes.bool.isRequired,
  subscribe: PropTypes.func.isRequired,
  unsubscribe: PropTypes.func.isRequired,
  disabled: PropTypes.bool,
}