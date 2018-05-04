import React from 'react'
import PropTypes from 'prop-types'
import styled from 'styled-components'

export default function ErrorPage(props) {
  return (
    <ErrorPageWrapper>
      <ErrorCodeWrapper>{props.errorCode}</ErrorCodeWrapper>
      <ErrorMessageWrapper>{props.errorMessage}</ErrorMessageWrapper>
    </ErrorPageWrapper>
  )
}

ErrorPage.propTypes = {
  errorCode: PropTypes.number.isRequired,
  errorMessage: PropTypes.string.isRequired
}

const ErrorPageWrapper = styled.div`
  width: 100%;
  min-height: 100vmin;
  background: #EEE;
  position: absolute;
  top: 0;
  padding-top: 160px;
  box-sizing: border-box;
  text-align: center;
`

const ErrorCodeWrapper = styled.div`
  display: inline-block;
  font-size: 220%;
  color: #666;
  margin-right: 10px;
`

const ErrorMessageWrapper = styled.div`
  display: inline-block;
  font-size: 150%;
  color: #888;
`