import React from 'react'
import PropTypes from 'prop-types'
import styled from 'styled-components'
import IconButton from '../commonComponents/UIDecorators/iconButton'
import DoneIcon from '@material-ui/icons/Done'
import CloseIcon from '@material-ui/icons/Close'
import Tooltip from '../commonComponents/UIDecorators/tooltip'

const TestControls = (props) => {
  const { test, onUpdate, onCancel } = props

  const handleUpdate = () => {
    if (!test.markedToUpdate) {
      onUpdate()
    }
  }

  const handleCancel = () => {
    if (test.markedToUpdate) {
      onCancel()
    }
  }

  const updateTooltipText =
    test.markedToUpdate ?
      'Current screenshot is selected'
      :
      'Select current screenshot'

  const cancelTooltipText =
    test.markedToUpdate ?
      'Select base screenshot'
      :
      'Base screenshot is selected'

  return (
    <React.Fragment>
      <Tooltip title={updateTooltipText} placement='top'>
        <ButtonWrapper>
          <IconButton icon={<DoneIcon color='secondary' />} onClick={handleUpdate} selected={test.markedToUpdate} />
        </ButtonWrapper>
      </Tooltip>
      <Tooltip title={cancelTooltipText} placement='top'>
        <ButtonWrapper>
          <IconButton icon={<CloseIcon color='secondary' />} onClick={handleCancel} selected={!test.markedToUpdate} />
        </ButtonWrapper>
      </Tooltip>
    </React.Fragment>
  )
}

TestControls.propTypes = {
  test: PropTypes.object.isRequired,
  onUpdate: PropTypes.func.isRequired,
  onCancel: PropTypes.func.isRequired
}

export default TestControls

const ButtonWrapper = styled.div`
  display: inline-block;
  margin: 0 4px;
`
