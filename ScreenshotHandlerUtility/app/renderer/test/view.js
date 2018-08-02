import React, { Component } from 'react'
import PropTypes from 'prop-types'
import styled from 'styled-components'
import { connect } from 'react-redux'
import * as actions from '../actions'
import Screenshot from './screenshot'

class TestView extends Component {
  componentDidMount() {
    if (this.props.test.unread) {
      this.props.removeUnread({test: this.props.test})
    }
  }

  componentDidUpdate() {
    if (this.props.test.unread) {
      this.props.removeUnread({test: this.props.test})
    }
  }

  render() {
    const { test } = this.props

    return (
      <Wrapper>
        <ScreenshotWrapper>
          <ScreenshotHeader>Base{test.markedToUpdate ? '' : <Selected>(selected)</Selected>}</ScreenshotHeader>
          <Screenshot source={test.baseScreenshotURL} borderColor={test.markedToUpdate ? '#BDBDBD' : '#7CB342'} />
        </ScreenshotWrapper>
        <ScreenshotWrapper>
          <ScreenshotHeader>Difference ({test.misMatchPercentage}%)</ScreenshotHeader>
          <Screenshot source={test.diffScreenshotURL} borderColor='#ef5350'/>
        </ScreenshotWrapper>
        <ScreenshotWrapper>
          <ScreenshotHeader>Current{test.markedToUpdate ? <Selected>(selected)</Selected> : ''}</ScreenshotHeader>
          <Screenshot source={test.newScreenshotURL} borderColor={test.markedToUpdate ? '#7CB342' : '#BDBDBD'} />
        </ScreenshotWrapper>
      </Wrapper>
    )
  }
}

TestView.propTypes = {
  test: PropTypes.object.isRequired,
  removeUnread: PropTypes.func.isRequired
}

export default connect(() => ({}), actions)(TestView)

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

const Selected = styled.span`  
  opacity: 0.85;
  margin-left: 4px;
`