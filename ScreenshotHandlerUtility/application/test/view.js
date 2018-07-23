import React, { Component } from 'react'
import PropTypes from 'prop-types'
import styled from 'styled-components'
import Screenshot from './screenshot'

class TestView extends Component {
  render() {
    const { test } = this.props

    return (
      <Wrapper>
        <ScreenshotWrapper>
          <ScreenshotHeader>Base{test.markedToUpdate ? '' : ' (selected)'}</ScreenshotHeader>
          <Screenshot source={test.baseScreenshotURL} borderColor={test.markedToUpdate ? '#BDBDBD' : '#9CCC65'} />
        </ScreenshotWrapper>
        <ScreenshotWrapper>
          <ScreenshotHeader>Difference ({test.misMatchPercentage}%)</ScreenshotHeader>
          <Screenshot source={test.diffScreenshotURL} borderColor='#ef5350'/>
        </ScreenshotWrapper>
        <ScreenshotWrapper>
          <ScreenshotHeader>Current{test.markedToUpdate ? ' (selected)' : ''}</ScreenshotHeader>
          <Screenshot source={test.newScreenshotURL} borderColor={test.markedToUpdate ? '#9CCC65' : '#BDBDBD'} />
        </ScreenshotWrapper>
      </Wrapper>
    )
  }
}

TestView.propTypes = {
  test: PropTypes.object.isRequired
}

export default TestView

const Wrapper = styled.div`
  display: flex;
  padding: 12px;
`

const ScreenshotWrapper = styled.div`
  display: inline-block;
  width: 33.3333%;
  text-align: center;
`

const ScreenshotHeader = styled.p`
  font-weight: bold;
`