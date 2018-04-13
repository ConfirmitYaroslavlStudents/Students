import React from 'react'
import PropTypes from 'prop-types'
import { ErrorPageWrapper, ErrorCodeWrapper, ErrorMessageWrapper } from '../common/styledComponents'

export default function ErrorPage(props) {
  return (
    <ErrorPageWrapper>
      <ErrorCodeWrapper>{props.errorCode}</ErrorCodeWrapper>
      <ErrorMessageWrapper>{props.errorMessage}</ErrorMessageWrapper>
    </ErrorPageWrapper>
  )
}

ErrorPage.propTypes = {
  errorCode: PropTypes.number,
  errorMessage: PropTypes.string,
}