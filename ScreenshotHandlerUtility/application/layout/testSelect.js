import React from 'react'
import PropTypes from 'prop-types'
import styled from 'styled-components'
import { IconButton } from 'confirmit-react-components'
import { ChevronIcon, iconModifiers } from 'confirmit-icons-material'

const TestSelect = (props) => {
  const { currentTestIndex, fallenTestAmount, onBackClick, onNextClick } = props

  return (
    <Wrapper>
      <IconButton onClick={onBackClick} disabled={currentTestIndex <= 0} className='contrast-button'>
        <ChevronIcon size={iconModifiers.size.size20px} turn={iconModifiers.turn.around} className='contrast-icon'/>
      </IconButton>
      <TestCount>{currentTestIndex + 1} of {fallenTestAmount}</TestCount>
      <IconButton onClick={onNextClick} disabled={currentTestIndex >= fallenTestAmount - 1} className='contrast-button'>
        <ChevronIcon size={iconModifiers.size.size20px} className='contrast-icon'/>
      </IconButton>
    </Wrapper>
  )
}

TestSelect.propTypes = {
  currentTestIndex: PropTypes.number.isRequired,
  fallenTestAmount: PropTypes.number.isRequired,
  onBackClick: PropTypes.func.isRequired,
  onNextClick: PropTypes.func.isRequired
}

export default TestSelect

const Wrapper = styled.div`
  display: inline-flex;
  align-items: center;
  margin: 0 4px;
`

const TestCount = styled.span`
  margin: 0 8px;
`