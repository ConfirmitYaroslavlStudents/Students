import React from 'react'
import Loadable from 'react-loadable'
import { LoadingCandidateInfoWrapper } from '../common/styledComponents'
import Spinner from '../common/UIComponentDecorators/spinner'

const LoadableCandidateInfoForm = Loadable({
  loader: () => import('./candidateInfoForm'),
  loading: () => <LoadingCandidateInfoWrapper> <Spinner size="medium"/> </LoadingCandidateInfoWrapper>,
})

export default LoadableCandidateInfoForm