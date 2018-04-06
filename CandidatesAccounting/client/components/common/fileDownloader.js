import React from 'react'
import PropTypes from 'prop-types'
import IconButton from '../common/UIComponentDecorators/iconButton'

export default function fileDownloader(props) {
  return (
    <IconButton
      icon={props.icon}
      style={props.buttonStyle ? props.buttonStyle : {}}
      disabled={props.disabled}
      onClick={() => {
        window.open(props.downloadLink, '_self')
      }}/>
  )
}

fileDownloader.propTypes = {
  downloadLink: PropTypes.string.isRequired,
  icon: PropTypes.object.isRequired,
  buttoStyle: PropTypes.object,
  disabled: PropTypes.bool,
}