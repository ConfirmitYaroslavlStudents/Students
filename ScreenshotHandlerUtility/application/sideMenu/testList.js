import React, { Component } from 'react'
import PropTypes from 'prop-types'
import styled, { css } from 'styled-components'
import ItemExpansionPanel from '../commonComponents/UIDecorators/itemExpansionPanel'
import TestItem from './testItem'
import TestIcon from './testIcon'

class SideMenuTestList extends Component {
  render() {
    const { fallenTests, currentTestIndex, onTestItemClick } = this.props

    const testGroups = []

    fallenTests.forEach(test => {
      let groupExists = false
      for (let i = 0; i < testGroups.length; i++) {
        if (testGroups[i].name === test.fixtureName) {
          testGroups[i].tests.push(test)
          if (!test.unread) {
            testGroups[i].unread = false
          }
          if (test.markedToUpdate) {
            testGroups[i].allNotToUpdate = false
          } else {
            testGroups[i].allToUpdate = false
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
          allToUpdate: test.markedToUpdate,
          allNotToUpdate: !test.markedToUpdate
        })
      }
    })

    return (
      testGroups.map((group, index) =>
        <ItemExpansionPanel
          key={index}
          summary={
            <SummaryWrapper bold={group.unread}>
              {group.name} <TestAmount>({group.tests.length})</TestAmount>
              {
                group.allToUpdate || group.allNotToUpdate ?
                  <IconWrapper><TestIcon markedToUpdate={group.allToUpdate}/></IconWrapper>
                  :
                  null
              }
            </SummaryWrapper>
          }
          expanded
        >
          {group.tests.map((test, index) =>
            <TestItem key={index} test={test} highlighted={test.index === currentTestIndex} onClick={onTestItemClick} />
          )}
        </ItemExpansionPanel>
      )
    )
  }
}

SideMenuTestList.propTypes = {
  fallenTests: PropTypes.array.isRequired,
  currentTestIndex: PropTypes.number.isRequired,
  onTestItemClick: PropTypes.func.isRequired
}

export default SideMenuTestList

const SummaryWrapper = styled.div`
  display: inline-flex;
  align-items: center;
  font-size: 105%;

  ${props => props.bold && css`
    font-weight: bold;
	`}
`

const TestAmount = styled.span`
  opacity: 0.85;
  margin-left: 4px;
`

const IconWrapper = styled.div`
  display: inline-flex;
  padding: 0 0 0 4px;
`