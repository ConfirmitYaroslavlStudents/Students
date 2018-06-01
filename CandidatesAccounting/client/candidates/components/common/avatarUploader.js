import React from 'react'
import PropTypes from 'prop-types'
import UploadIcon from '@material-ui/icons/FileUpload'
import FileUploader from '../../../commonComponents/fileUploader'
import LetterAvatar from '../../../commonComponents/UIComponentDecorators/letterAvatar'
import AvatarIcon from '@material-ui/icons/AccountCircle'
import { SmallerIconStyle, SmallButtonStyle } from '../../../commonComponents/styleObjects'
import styled from 'styled-components'

export default function AvatarUploader(props) {
  const { candidate, onAvatarUpload } = props

  const handleFileUpload = file => {
    onAvatarUpload(file)
  }

  const avatar =
    candidate.hasAvatar ?
      <AvatarIcon style={{height: 42, width: 42, color: '#3F51B5'}} />
      :
      candidate.name[0] ?
        <LetterAvatar letters={candidate.name[0]} />
        :
        <NoAvatarWrapper> upload avatar </NoAvatarWrapper>

  return (
    <UploaderWrapper>
      { avatar }
      <FileUploader
        uploadFile={handleFileUpload}
        fileTypes={['png', 'jpg', 'jpeg']}
        icon={<UploadIcon style={SmallerIconStyle}/>}
        buttonStyle={SmallButtonStyle}
      />
    </UploaderWrapper>
  )
}

AvatarUploader.propTypes = {
  candidate: PropTypes.object.isRequired,
  onAvatarUpload: PropTypes.func.isRequired
}

const UploaderWrapper = styled.div`
  display: inline-flex;
`

const NoAvatarWrapper = styled.div`
  display: inline-flex;
  color: #777;
`