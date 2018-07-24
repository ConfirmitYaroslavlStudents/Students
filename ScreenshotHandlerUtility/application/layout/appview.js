import React, { Component } from 'react'
import PropTypes from 'prop-types'
import styled from 'styled-components'
import { connect } from 'react-redux'
import * as actions from '../actions'
import NoFallenTestsPage from './noFallenTestsPage'
import TestHeader from '../test/header'
import SideMenu from '../sideMenu/menu'
import TestView from '../test/view'
import TestSelect from './testSelect'
import TestControls from './testControls'
import AppControls from './appControls'

class AppView extends Component {
  constructor(props) {
    super(props)
    this.fallenTestAmount = 0
  }

  handleBackClick = () => {
    const currentTestIndex = this.props.currentTestIndex
    if (currentTestIndex > 0) {
      this.props.setCurrentTestIndex({ index: currentTestIndex - 1 })
    }
  }

  handleNextClick = () => {
    const currentTestIndex = this.props.currentTestIndex
    if (currentTestIndex < this.fallenTestAmount - 1) {
      this.props.setCurrentTestIndex({ index: currentTestIndex + 1 })
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
    const { fallenTests, testTotalAmount, currentTestIndex } = this.props

    const fallenTestArray = Object.keys(fallenTests).map(testIndex => fallenTests[testIndex])

    this.fallenTestAmount = fallenTestArray.length

    if (this.fallenTestAmount === 0) {
      return (
        <NoFallenTestsPage totalTestAmount={testTotalAmount} />
      )
    }

    const currentTest = fallenTestArray[currentTestIndex]

    return (
      <React.Fragment>
        <Header>
          <SideMenuButtonWrapper>
            <SideMenu />
          </SideMenuButtonWrapper>
          <TestHeader test={currentTest} />
        </Header>
        <TestView test={currentTest} />
        <Footer>
          <LeftColumn>
            <span>Fallen tests: {this.fallenTestAmount} of {testTotalAmount}</span>
          </LeftColumn>
          <CenterColumn>
            <TestSelect
              currentTestIndex={currentTestIndex}
              fallenTestAmount={this.fallenTestAmount}
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
  testTotalAmount: PropTypes.number.isRequired,
  currentTestIndex: PropTypes.number.isRequired,
  markToUpdate: PropTypes.func.isRequired,
  unmarkToUpdate: PropTypes.func.isRequired,
  setCurrentTestIndex: PropTypes.func.isRequired
}

export default connect(state => ({
    fallenTests: state.fallenTests,
    testTotalAmount: state.testTotalAmount,
    currentTestIndex: state.currentTestIndex
  }
), actions)(AppView)

const Header = styled.div`
  position: relative;
  background-color: #F3F3F3;
  padding: 8px;
  text-align: center;
`

const SideMenuButtonWrapper = styled.div`
  display: inline-block;
  position: absolute;
  left: 4px;
  top: 10px;
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