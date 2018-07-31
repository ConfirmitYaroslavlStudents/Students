import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../actions'
import TestsArePassedPage from './testsArePassedPage'
import Header from './header'
import TestHeader from '../test/header'
import TestView from '../test/view'
import Footer from './footer'
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
      this.props.markToUpdate({ test })
    }
    this.handleNextClick()
  }

  handleCancel = (test) => {
    if (!test.markedAsError) {
      this.props.markAsError({ test })
    }
    this.handleNextClick()
  }

  render() {
    const { fallenTests, testTotalAmount, currentTestIndex, commit } = this.props

    const fallenTestArray = Object.keys(fallenTests).map(testIndex => fallenTests[testIndex])

    this.fallenTestAmount = fallenTestArray.length

    if (this.fallenTestAmount === 0) {
      return <TestsArePassedPage />
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
        <Header currentTest={currentTest}>
          <TestHeader test={currentTest} />
        </Header>

        <TestView test={currentTest} />

        <Footer
          leftColumn={
            <span>Fallen screenshots: {this.fallenTestAmount} of {testTotalAmount}</span>
          }
          centerColumn={
            <React.Fragment>
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
            </React.Fragment>
          }
          rightColumn={
            <CommitConfirmDialog
              commit={commit}
              testsToUpdate={testsToUpdate}
            />
          }
        />
      </React.Fragment>
    )
  }
}

AppView.propTypes = {
  fallenTests: PropTypes.object.isRequired,
  testTotalAmount: PropTypes.number.isRequired,
  currentTestIndex: PropTypes.number.isRequired,
  markToUpdate: PropTypes.func.isRequired,
  markAsError: PropTypes.func.isRequired,
  setCurrentTestIndex: PropTypes.func.isRequired,
  commit: PropTypes.func.isRequired
}

export default connect(state => ({
    fallenTests: state.fallenTests,
    testTotalAmount: state.testTotalAmount,
    currentTestIndex: state.currentTestIndex
  }
), actions)(AppView)