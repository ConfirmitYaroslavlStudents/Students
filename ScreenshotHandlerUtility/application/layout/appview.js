import React, { Component } from 'react'
import PropTypes from 'prop-types'
import styled from 'styled-components'
import { connect } from 'react-redux'
import * as actions from '../actions'
import NoFallenTestsPage from './noFallenTestsPage'
import TestHeader from '../test/header'
import SideMenu from './sideMenu'
import TestView from '../test/view'
import TestSelect from './testSelect'
import TestControls from './testControls'
import AppControls from './appControls'

class AppView extends Component {
  constructor(props) {
    super(props)
    this.state = { currentTestIndex: 0 }
  }

  handleBackClick = () => {
    let currentTestIndex = this.state.currentTestIndex
    if (currentTestIndex > 0) {
      currentTestIndex--
      this.setState({ currentTestIndex })
    }
  }

  handleNextClick = () => {
    let currentTestIndex = this.state.currentTestIndex
    if (currentTestIndex < Object.keys(this.props.fallenTests).length - 1) {
      currentTestIndex++
      this.setState({ currentTestIndex })
    }
  }

  handleUpdate = (test) => () => {
    this.props.markToUpdate({ test })
    this.handleNextClick()
  }

  handleCancel = (test) => () => {
    this.props.unmarkToUpdate({ test })
  }

  render() {
    const { currentTestIndex } = this.state
    const { fallenTests, testTotalCount, markToUpdate, unmarkToUpdate } = this.props

    const fallenTestsArray = Object.keys(fallenTests).map(testIndex => fallenTests[testIndex])

    if (fallenTestsArray.length === 0) {
      return (
        <NoFallenTestsPage totalTestAmount={testTotalCount} />
      )
    }

    const currentTest = fallenTestsArray[currentTestIndex]

    return (
      <React.Fragment>
        <Header>
          <SideMenu />
          <TestHeader test={currentTest} />
        </Header>
        <TestView
          test={currentTest}
          markToUpdate={markToUpdate}
          unmarkToUpdate={unmarkToUpdate}
          onBackClick={this.handleBackClick}
          onNextClick={this.handleNextClick}
          isFirst={currentTestIndex === 0}
          isLast={currentTestIndex === fallenTestsArray.length - 1}
        />
        <Footer>
          <LeftColumn>
            <span>Fallen tests: {fallenTestsArray.length} of {testTotalCount}</span>
          </LeftColumn>
          <CenterColumn>
            <TestSelect
              currentTestIndex={currentTestIndex}
              testTotalAmount={fallenTestsArray.length}
              onBackClick={this.handleBackClick}
              onNextClick={this.handleNextClick}
            />
            <TestControls
              test={currentTest}
              onUpdate={this.handleUpdate(currentTest)}
              onCancel={this.handleCancel(currentTest)}
            />
          </CenterColumn>
          <RightColumn>
            <AppControls />
          </RightColumn>
        </Footer>
      </React.Fragment>
    )
  }
}


AppView.propTypes = {
  fallenTests: PropTypes.object.isRequired,
  testTotalCount: PropTypes.number.isRequired,
  markToUpdate: PropTypes.func.isRequired,
  unmarkToUpdate: PropTypes.func.isRequired
}

export default connect(state => ({
    fallenTests: state.fallenTests,
    testTotalCount: state.testTotalCount
  }
), actions)(AppView)

const Header = styled.div`
  position: relative;
  background-color: #F3F3F3;
  padding: 8px;
  text-align: center;
`

const Footer = styled.div`
  background-color: #29B6F6;
  color: #ffffff;
  width: 100%;
  position: absolute;
  left: 0;
  bottom: 0;
  box-sizing: border-box;
  padding: 12px 24px;
`

const LeftColumn = styled.div`
  display: inline-block;
  width: 33.3333%;
  text-align: left;
`

const CenterColumn = styled.div`
  display: inline-block;
  width: 33.3333%;
  text-align: center;
`

const RightColumn = styled.div`
  display: inline-block;
  width: 33.3333%;
  text-align: right;
`