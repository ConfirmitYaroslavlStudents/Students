import React, { Component } from 'react'
import PropTypes from 'prop-types'
import TestList from './testList'

class SideMenuTestListGroups extends Component {
  render() {
    const { tests, currentTestIndex, onTestItemClick } = this.props

    const testGroups = []

    tests.forEach(test => {
      let groupExists = false
      for (let i = 0; i < testGroups.length; i++) {
        if (testGroups[i].name === test.fixtureName) {
          testGroups[i].tests.push(test)
          if (!test.unread) {
            testGroups[i].unread = false
          }
          if (!test.markedToUpdate) {
            testGroups[i].markedToUpdate = false
          }
          if (!test.markedAsError) {
            testGroups[i].markedAsError = false
          }
          if (test.index === currentTestIndex) {
            testGroups[i].selected = true
          }
          groupExists = true
          break
        }
      }
      if (!groupExists) {
        testGroups.push({
          name: test.fixtureName,
          tests: [test],
          unread: test.unread,
          markedToUpdate: test.markedToUpdate,
          markedAsError: test.markedAsError,
          selected: test.index === currentTestIndex
        })
      }
    })

    return (
      testGroups.map((group, index) =>
        <TestList key={index} testGroup={group} currentTestIndex={currentTestIndex} onTestItemClick={onTestItemClick} />
      )
    )
  }
}

SideMenuTestListGroups.propTypes = {
  tests: PropTypes.array.isRequired,
  currentTestIndex: PropTypes.number.isRequired,
  onTestItemClick: PropTypes.func.isRequired
}

export default SideMenuTestListGroups