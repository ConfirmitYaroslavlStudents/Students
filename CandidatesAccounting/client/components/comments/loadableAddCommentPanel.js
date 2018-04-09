import React from 'react'
import Loadable from 'react-loadable'
import { LoadingAddCommentPanelWrapper } from '../common/styledComponents'
import Spinner from '../common/UIComponentDecorators/spinner'

const LoadableAddCommentPanel = Loadable({
  loader: () => import('./addCommentPanel'),
  loading: () => <LoadingAddCommentPanelWrapper> <Spinner size='medium'/> </LoadingAddCommentPanelWrapper>,
})

export default LoadableAddCommentPanel