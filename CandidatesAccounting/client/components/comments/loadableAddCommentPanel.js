import React from 'react'
import Loadable from 'react-loadable'
import Spinner from '../common/UIComponentDecorators/spinner'

const LoadableAddCommentPanel = Loadable({
  loader: () => import('./addCommentPanel'),
  loading: () => <div style={{height: 164, display: 'flex', backgroundColor: '#fff',
    'boxShadow': '0 0 15px rgba(0, 0, 0, 0.2)'}}> <Spinner size="medium"/> </div>,
})

export default LoadableAddCommentPanel