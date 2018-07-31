import React, { Component } from 'react'
import PropTypes from 'prop-types'
import styled, { css } from 'styled-components'
import TestItem from './testItem'
import { IconButton } from 'confirmit-react-components'
import { CheckIcon, CloseIcon, ChevronIcon, iconModifiers } from 'confirmit-icons-material'

class SideMenuTestList extends Component {
  constructor(props) {
    super(props)
    this.state = { expanded: true }
  }

  handleToggle = () => {
    if (this.state.expanded) {
      this.setState({ expanded: false })
    } else {
      this.setState({ expanded: true })
    }
  }

  render() {
    const { expanded } = this.state
    const { testGroup, currentTestIndex, onTestItemClick } = this.props

    return (
      <ListWrapper>
        <SummaryWrapper unread={testGroup.unread} selected={testGroup.selected} expanded={expanded} >
          <GroupNameWrapper>
            {testGroup.name} <TestAmount>({testGroup.tests.length})</TestAmount>
            <IconWrapper>
              {
                testGroup.markedToUpdate && (
                  <CheckIcon className='primary-icon' size={iconModifiers.size.size20px} />
                )
              }
              {
                testGroup.markedAsError && (
                  <CloseIcon className='error-icon' size={iconModifiers.size.size20px} />
                )
              }
            </IconWrapper>
          </GroupNameWrapper>
          <ToggleButtonWrapper>
            <IconButton onClick={this.handleToggle}>
              <ChevronIcon
                size={iconModifiers.size.size12px}
                turn={expanded ? iconModifiers.turn.counterclockwise : iconModifiers.turn.clockwise}
              />
            </IconButton>
          </ToggleButtonWrapper>
        </SummaryWrapper>
        <ContentWrapper expanded={expanded}>
          {
            testGroup.tests.map((test, index) =>
              <TestItem key={index} test={test} highlighted={test.index === currentTestIndex} onClick={onTestItemClick} />
            )
          }
        </ContentWrapper>
      </ListWrapper>
    )
  }
}

SideMenuTestList.propTypes = {
  testGroup: PropTypes.object.isRequired,
  currentTestIndex: PropTypes.number.isRequired,
  onTestItemClick: PropTypes.func.isRequired
}

export default SideMenuTestList

const ListWrapper = styled.div`
  display: flex;
  flex-direction: column;
  border-bottom: 1px solid #aaa;
  cursor: pointer;
  overflow: hidden;
`

const SummaryWrapper = styled.div`
  display: inline-flex;
  align-items: center;
  font-size: 105%;  
  padding: 2px 8px;

  ${props => props.unread && css`
    font-weight: bold;
	`}
	
	${props => props.selected && !props.expanded && css`
	  border-right: 6px solid #7CB342;
    padding-right: 2px;
	`}
`

const GroupNameWrapper = styled.div`
  display: inline-flex;
  width: 80%;
  justify-content: flex-start;
  align-items: center;
`

const ToggleButtonWrapper = styled.div`
  display: inline-flex;
  width: 20%;
  justify-content: flex-end;
  align-items: center;
`

const ContentWrapper = styled.div`
  display: inline-flex;
  flex-direction: column;
  
  ${props => !props.expanded && css`    
    visibility: collapse;
    height: 0;
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