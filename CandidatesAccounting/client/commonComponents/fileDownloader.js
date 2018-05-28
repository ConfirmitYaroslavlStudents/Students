import React from 'react'
import PropTypes from 'prop-types'

export default function fileDownloader(props) {
  const { downloadLink, disabled, children } = props

  const handleDownloadFile = () => {
    if (!disabled) {
      window.open(downloadLink, '_self')
    }
  }

  return (
    <div className='inline-flex' onClick={handleDownloadFile}>
      {children}
    </div>
  )
}

fileDownloader.propTypes = {
  downloadLink: PropTypes.string.isRequired,
  disabled: PropTypes.bool
}