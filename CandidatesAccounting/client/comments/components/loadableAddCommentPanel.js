import React from 'react'
import Loadable from 'react-loadable'
import Spinner from '../../common/UIComponentDecorators/spinner'
import styled from 'styled-components'

const LoadableAddCommentPanel = Loadable({
  loader: () => import('./addCommentPanel'),
  loading: () =>
    <LoadingAddCommentPanelWrapper>
      <SpinnerWrapper>
        <Spinner size={50}/>
      </SpinnerWrapper>
    </LoadingAddCommentPanelWrapper>,
})

export default LoadableAddCommentPanel

const LoadingAddCommentPanelWrapper = styled.div`
  height: 164px;
  display: flex;
  background-color: #fff;
  box-shadow: 0 0 15px rgba(0, 0, 0, 0.2);
`

const SpinnerWrapper = styled.div`
  display: inline-flex;
  boxSizing: border-box;
  align-items: center;
  height: 50px;
  width: 50px;
  margin: auto;
`