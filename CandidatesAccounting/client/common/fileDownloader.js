import React from 'react'
import PropTypes from 'prop-types'
import IconButton from './UIComponentDecorators/iconButton'

export default function fileDownloader(props) {
  const { downloadLink, disabled, icon, buttonStyle, withoutButton, children } = props

  const handleDownloadFile = () => {
    if (!disabled) {
      window.open(downloadLink, '_self')
    }
  }

  const button =
    withoutButton ?
      ''
      :
      <IconButton
        icon={icon}
        style={buttonStyle}
        disabled={disabled}
        onClick={handleDownloadFile}
      />

  return (
    <div className='inline-flex' onClick={handleDownloadFile}>
      {button}
      {children}
    </div>
  )
}

fileDownloader.propTypes = {
  downloadLink: PropTypes.string.isRequired,
  icon: PropTypes.object,
  buttoStyle: PropTypes.object,
  disabled: PropTypes.bool,
  withoutButton: PropTypes.bool
}