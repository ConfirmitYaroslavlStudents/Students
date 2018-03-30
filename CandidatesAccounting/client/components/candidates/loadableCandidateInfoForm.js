import React from 'react';
import Loadable from 'react-loadable';
import Spinner from '../common/UIComponentDecorators/spinner';

const LoadableCandidateInfoForm = Loadable({
  loader: () => import('./candidateInfoForm'),
  loading: () => <div style={{height: 517, width: 547, display: 'flex', backgroundColor: '#fff'}}> <Spinner size="medium"/> </div>,
});

export default LoadableCandidateInfoForm;