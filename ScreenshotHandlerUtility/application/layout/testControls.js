import React from 'react'
import PropTypes from 'prop-types'
import styled from 'styled-components'
import { IconButton } from 'confirmit-react-components'
import { CheckIcon, CloseIcon } from 'confirmit-icons-material'
import { Tooltip } from 'confirmit-react-components'

const TestControls = (props) => {
  const { test, onUpdate, onCancel } = props

  const handleUpdate = () => {
    onUpdate(test)
  }

  const handleCancel = () => {
    onCancel(test)
  }

  const cancelTooltipText =
    test.markedAsError ?
      'Marked as error'
      :
      'Mark as error'

  const updateTooltipText =
    test.markedToUpdate ?
      'Marked to update'
      :
      'Mark to update'

  return (
    <React.Fragment>
      <Tooltip content={cancelTooltipText} defaultPlacement='top'>
        <ButtonWrapper>
          <IconButton onClick={handleCancel} className={test.markedAsError ? 'outlined-error-button' : null}>
            <CloseIcon className='error-icon' />
          </IconButton>
        </ButtonWrapper>
      </Tooltip>
      <Tooltip content={updateTooltipText} defaultPlacement='top'>
        <ButtonWrapper>
          <IconButton onClick={handleUpdate} className={test.markedToUpdate ? 'outlined-primary-light-button' : null}>
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