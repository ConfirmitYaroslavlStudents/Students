import React from 'react'
import PropTypes from 'prop-types'
import UploadIcon from '@material-ui/icons/CloudUpload'
import FileUploader from '../../../components/fileUploader'
import ImageAvatar from '../../../components/decorators/imageAvatar'
import LetterAvatar from '../../../components/decorators/letterAvatar'
import { SmallerIconStyle, SmallButtonStyle } from '../../../components/styleObjects'
import styled from 'styled-components'

const AvatarUploader = (props) => {
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

export default AvatarUploader

const UploaderWrapper = styled.div`
  display: inline-flex;
`

const NoAvatarWrapper = styled.div`
  display: inline-flex;
  color: #777;
`