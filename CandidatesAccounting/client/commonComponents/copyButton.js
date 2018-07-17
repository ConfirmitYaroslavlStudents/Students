import React from 'react'
import PropTypes from 'prop-types'
import IconButton from './UIComponentDecorators/iconButton'
import CopyIcon from '@material-ui/icons/ContentCopy'
import { SmallButtonStyle, SmallestIconStyle} from './styleObjects'

const EmailWrapper = (props) => {
  const copyToBuffer = () => {
    const fakeInput = document.createElement('input')
    document.body.appendChild(fakeInput)
    fakeInput.setAttribute('value', props.text)
    fakeInput.select()
    document.execCommand('copy')
    document.body.removeChild(fakeInput)
  }

  return (
    <IconButton
      icon={<CopyIcon style={SmallestIconStyle} />}
      onClick={copyToBuffer}
      style={{...SmallButtonStyle, marginLeft: 2}}
    />
  )
}

EmailWrapper.propTypes = {
  text: PropTypes.string.isRequired
}

export default EmailWrapper