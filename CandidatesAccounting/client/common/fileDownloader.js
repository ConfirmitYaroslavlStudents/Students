import React from 'react'
import PropTypes from 'prop-types'
import IconButton from './UIComponentDecorators/iconButton'

export default function fileDownloader(props) {
  const { downloadLink, disabled, icon, buttonStyle, children } = props

  const handleDownloadFile = () => {
    if (!disabled) {
      window.open(downloadLink, '_self')
    }
  }

  return (
    <div onClick={handleDownloadFile}>
      <IconButton
        icon={icon}
        style={buttonStyle}
        disabled={disabled}
        onClick={handleDownloadFile}
      />
      {children}
    </div>
  )
}

fileDownloader.propTypes = {
  downloadLink: PropTypes.string.isRequired,
  icon: PropTypes.object.isRequired,
  buttoStyle: PropTypes.object,
  disabled: PropTypes.bool,
}