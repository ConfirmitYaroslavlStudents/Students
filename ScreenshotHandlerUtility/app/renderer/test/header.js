import React from 'react'
import PropTypes from 'prop-types'
import styled from 'styled-components'

const TestHeader = (props) => {
  const { test } = props

  return (
    <Wrapper>
      <Header>{test.testName} №{test.number} {test.screenshotName ? ` (${test.screenshotName})` : ''} ({test.browserName})</Header>
      <SubHeader>{test.fixtureName}</SubHeader>
    </Wrapper>
  )
}

TestHeader.propTypes = {
  test: PropTypes.object.isRequired
}

export default TestHeader

const Wrapper = styled.div`
  display: inline-block;
  text-align: center;
`

const Header = styled.p`
  font-weight: bold;
  font-size: 120%;
  margin-bottom: 4px;
`

const SubHeader = styled.p`
  font-weight: bold;
  font-size: 120%;
  opacity: 0.8;
`