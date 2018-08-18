import React from 'react'
import PropTypes from 'prop-types'

const FileDownloader = (props) => {
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

FileDownloader.propTypes = {
  downloadLink: PropTypes.string.isRequired,
  disabled: PropTypes.bool
}

export default FileDownloader