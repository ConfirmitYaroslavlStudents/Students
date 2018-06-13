import React from 'react'
import PropTypes from 'prop-types'
import IconButton from './UIComponentDecorators/iconButton'
import CopyIcon from '@material-ui/icons/ContentCopy'
import { SmallButtonStyle, SmallestIconStyle} from './styleObjects'

export default function EmailWrapper(props) {
  return (
    <IconButton icon={<CopyIcon style={SmallestIconStyle}/>} style={{...SmallButtonStyle, marginLeft: 2}} onClick={() => {
      const fakeInput = document.createElement('input')
      document.body.appendChild(fakeInput)
      fakeInput.setAttribute('value', props.text)
      fakeInput.select()
      document.execCommand('copy')
      document.body.removeChild(fakeInput)
    }}/>
  )
}

EmailWrapper.propTypes = {
  text: PropTypes.string.isRequired
}