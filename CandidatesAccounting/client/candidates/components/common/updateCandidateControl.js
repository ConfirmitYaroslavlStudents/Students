import React from 'react'
import PropTypes from 'prop-types'
import UpdateCandidateDialog from './updateCandidateDialog'
import Spinner from '../../../commonComponents/UIComponentDecorators/spinner'
import styled from 'styled-components'

export default function UpdateCandidateControl(props) {
  if (props.candidate.id === props.candidateIdOnUpdating) {
    return (
      <SpinnerWrapper>
        <Spinner size={26}/>
      </SpinnerWrapper>
    )
  }

  return (
    <UpdateCandidateDialog
      candidate={props.candidate}
      disabled={props.disabled}
    />
  )
}

UpdateCandidateControl.propTypes = {
  candidate: PropTypes.object.isRequired,
  candidateIdOnUpdating: PropTypes.string,
  disabled: PropTypes.bool
}

const SpinnerWrapper = styled.div`
  display: inline-flex;
  align-items: center;
  padding-left: 7px;
  box-sizing: border-box;
  width: 40px;
  height: 40px;
`