import React from 'react'
import PropTypes from 'prop-types'
import Loadable from 'react-loadable'
import styled from 'styled-components'
import Spinner from '../../../commonComponents/UIComponentDecorators/spinner'

const LoadableCandidateInfoForm = Loadable({
  loader: () => import('./updateCandidateForm'),
  loading: () =>
    <LoadingCandidateInfoWrapper>
      <SpinnerWrapper>
        <Spinner size={50}/>
      </SpinnerWrapper>
    </LoadingCandidateInfoWrapper>,
})

LoadableCandidateInfoForm.propTypes = {
  candidate: PropTypes.object.isRequired,
  tags: PropTypes.array.isRequired
}

export default LoadableCandidateInfoForm

const LoadingCandidateInfoWrapper = styled.div`
  height: 517px;
  width: 547px;
  display: flex;
  backgroundColor: #fff;
`

const SpinnerWrapper = styled.div`
  display: inline-flex;
  boxSizing: border-box;
  align-items: center;
  height: 50px;
  width: 50px;
  margin: auto;
`