import React from 'react'
import PropTypes from 'prop-types'
import DeleteIcon from '@material-ui/icons/Cancel'
import { formatDateTime } from '../../utilities/customMoment'
import IconButton from '../../commonComponents/UIComponentDecorators/iconButton'
import { SmallerIconStyle, SmallButtonStyle } from '../../commonComponents/styleObjects'
import styled from 'styled-components'

const NotificationInfo = (props) => {
  const { notification, onDelete } = props

  return (
    <React.Fragment>
      <CandidateNameWrapper>{notification.source.name}</CandidateNameWrapper>
      <ControlsWrapper>
        <DateWrapper>
          {formatDateTime(notification.content.date)}
        </DateWrapper>
        <ButtonWrapper>
          <IconButton
            icon={<DeleteIcon style={SmallerIconStyle} />}
            style={SmallButtonStyle}
            onClick={onDelete}
          />
        </ButtonWrapper>
      </ControlsWrapper>
    </React.Fragment>
  )
}

NotificationInfo.propTypes = {
  notification: PropTypes.object.isRequired,
  onDelete: PropTypes.func.isRequired,
}

export default NotificationInfo

const ButtonWrapper = styled.div`
  display: inline-flex;
  z-index: 10;
  margin-left: 4px;
  margin-right: -6px;
  margin-top: -6px;
`

const CandidateNameWrapper = styled.div`
  display: inline-flex;
`

const ControlsWrapper = styled.div`
  display: inline-flex;
  float: right;
`

const DateWrapper = styled.div`
  color: #888;
  font-size: 96%;
`