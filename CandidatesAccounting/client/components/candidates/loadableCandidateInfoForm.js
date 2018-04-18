import React from 'react'
import Loadable from 'react-loadable'
import styled from 'styled-components'
import Spinner from '../common/UIComponentDecorators/spinner'

const LoadableCandidateInfoForm = Loadable({
  loader: () => import('./candidateInfoForm'),
  loading: () =>
    <LoadingCandidateInfoWrapper>
      <SpinnerWrapper>
        <Spinner size={50}/>
      </SpinnerWrapper>
    </LoadingCandidateInfoWrapper>,
})

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

export default LoadableCandidateInfoForm