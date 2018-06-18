import React from 'react'
import PropTypes from 'prop-types'
import UploadIcon from '@material-ui/icons/FileUpload'
import FileUploader from '../../../commonComponents/fileUploader'
import ImageAvatar from '../../../commonComponents/UIComponentDecorators/imageAvatar'
import LetterAvatar from '../../../commonComponents/UIComponentDecorators/letterAvatar'
import { SmallerIconStyle, SmallButtonStyle } from '../../../commonComponents/styleObjects'
import styled from 'styled-components'

export default function AvatarUploader(props) {
  const { candidate, onAvatarUpload } = props

  const handleFileUpload = file => {
    onAvatarUpload(file)
  }

  const handleCancel = () => {
    onAvatarUpload(null)
  }

  const avatar =
    candidate.hasAvatar ?
      <ImageAvatar
        source={'/' + candidate.name.toLowerCase() + 's/' + candidate.id + '/avatar'}
        alternative={candidate.name ? candidate.name[0] : 'avatar'}
      />
      :
      candidate.name ?
        <LetterAvatar letters={candidate.name[0]} />
        :
        <NoAvatarWrapper> no avatar uploaded </NoAvatarWrapper>

  return (
    <UploaderWrapper>
      { avatar }
      <FileUploader
        onAccept={handleFileUpload}
        onCancel={handleCancel}
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