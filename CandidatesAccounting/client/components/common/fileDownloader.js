import React from 'react'
import PropTypes from 'prop-types'
import IconButton from '../common/UIComponentDecorators/iconButton'

export default function fileDownloader(props) {
  const downloadFile = () => {
    window.open(props.downloadLink, '_self')
  }

  return (
    <div onClick={downloadFile}>
      <IconButton
        icon={props.icon}
        style={props.buttonStyle}
        disabled={props.disabled}
        onClick={downloadFile}/>
      {props.children}
    </div>
  )
}

fileDownloader.propTypes = {
  downloadLink: PropTypes.string.isRequired,
  icon: PropTypes.object.isRequired,
  buttoStyle: PropTypes.object,
  disabled: PropTypes.bool,
}