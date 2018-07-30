import React, { Component } from 'react'
import PropTypes from 'prop-types'
import styled, { css } from 'styled-components'
import { connect } from 'react-redux'
import * as actions from '../actions'
import NoFallenTestsPage from './noFallenTestsPage'
import TestHeader from '../test/header'
import SideMenu from '../sideMenu/menu'
import TestView from '../test/view'
import TestSelect from './testSelect'
import TestControls from './testControls'
import CommitConfirmDialog from './commitConfirmDialog'

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

  handleUpdate = (test) => {
    if (!test.markedToUpdate) {
      this.props.markToUpdate({test})
    }
    this.handleNextClick()
  }

  handleCancel = (test) => {
    if (!test.markedAsError) {
      this.props.markAsError({test})
    }
    this.handleNextClick()
  }

  render() {
    const { fallenTests, screenshotTotalAmount, currentTestIndex, markedToUpdateAmount, commit } = this.props

    const fallenTestArray = Object.keys(fallenTests).map(testIndex => fallenTests[testIndex])

    this.fallenTestAmount = fallenTestArray.length

    if (this.fallenTestAmount === 0) {
      return <NoFallenTestsPage />
    }

    const testsToUpdate = []
    fallenTestArray.forEach(test => {
      if (test.markedToUpdate) {
        testsToUpdate.push(test)
      }
    })

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
          <Column justify='flex-start'>
            <span>Fallen screenshots: {this.fallenTestAmount} of {screenshotTotalAmount}</span>
          </Column>
          <Column justify='center'>
            <TestSelect
              currentTestIndex={currentTestIndex}
              fallenTestAmount={this.fallenTestAmount}
              onBackClick={this.handleBackClick}
              onNextClick={this.handleNextClick}
            />
            <TestControls
              test={currentTest}
              onUpdate={this.handleUpdate}
              onCancel={this.handleCancel}
            />
          </Column>
          <Column justify='flex-end'>
            <CommitConfirmDialog
              commit={commit}
              markedToUpdateAmount={markedToUpdateAmount}
              testsToUpdate={testsToUpdate}
            />
          </Column>
        </Footer>
      </React.Fragment>
    )
  }
}


AppView.propTypes = {
  fallenTests: PropTypes.object.isRequired,
  screenshotTotalAmount: PropTypes.number.isRequired,
  currentTestIndex: PropTypes.number.isRequired,
  markToUpdate: PropTypes.func.isRequired,
  markAsError: PropTypes.func.isRequired,
  setCurrentTestIndex: PropTypes.func.isRequired,
  markedToUpdateAmount: PropTypes.number.isRequired,
  commit: PropTypes.func.isRequired
}

export default connect(state => ({
    fallenTests: state.fallenTests,
    screenshotTotalAmount: state.screenshotTotalAmount,
    currentTestIndex: state.currentTestIndex,
    markedToUpdateAmount: state.markedToUpdateAmount
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
  left: 7px;
  top: 7px;
`

const Footer = styled.div`
  display: flex;
  align-items: center;
  background-color: #7CB342;
  color: #ffffff;
  width: 100%;
  position: absolute;
  left: 0;
  bottom: 0;
  box-sizing: border-box;
  font-weight: bold; 
  padding: 12px 24px;
`

const Column = styled.div`
  display: inline-flex;
  width: 33.3333%;
  align-items: center;
  
  ${props => props.justify && css`    
    justify-content: ${props.justify};
	`}	
`