import React from 'react'
import PropTypes from 'prop-types'
import styled from 'styled-components'
import { IconButton } from 'confirmit-react-components'
import { CheckIcon, CloseIcon } from 'confirmit-icons-material'
import Tooltip from '../commonComponents/UIDecorators/tooltip'

const TestControls = (props) => {
  const { test, onUpdate, onCancel } = props

  const handleUpdate = () => {
    onUpdate(test)
  }

  const handleCancel = () => {
    onCancel(test)
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
      <Tooltip title={cancelTooltipText} placement='top'>
        <ButtonWrapper>
          <IconButton onClick={handleCancel} className={test.markedToUpdate ? '' : 'outlined-error-button'}>
            <CloseIcon className='error-icon' />
          </IconButton>
        </ButtonWrapper>
      </Tooltip>
      <Tooltip title={updateTooltipText} placement='top'>
        <ButtonWrapper>
          <IconButton onClick={handleUpdate} className={test.markedToUpdate ? 'outlined-primary-light-button' : ''}>
            <CheckIcon className='primary-light-icon' />
          </IconButton>
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
