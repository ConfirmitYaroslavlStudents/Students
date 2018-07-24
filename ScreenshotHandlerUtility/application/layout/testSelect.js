import React from 'react'
import PropTypes from 'prop-types'
import styled from 'styled-components'
import IconButton from '../commonComponents/UIDecorators/iconButton'
import ArrowLeftIcon from '@material-ui/icons/KeyboardArrowLeft'
import ArrowRightIcon from '@material-ui/icons/KeyboardArrowRight'

const TestSelect = (props) => {
  const { currentTestIndex, fallenTestAmount, onBackClick, onNextClick } = props

  return (
    <Wrapper>
      <IconButton
        icon={<ArrowLeftIcon color='secondary' />}
        onClick={onBackClick}
        disabled={currentTestIndex <= 0}
      />
      <TestCount>{currentTestIndex + 1} of {fallenTestAmount}</TestCount>
      <IconButton
        icon={<ArrowRightIcon color='secondary' />}
        onClick={onNextClick}
        disabled={currentTestIndex >= fallenTestAmount - 1}
      />
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
  display: inline-block;
  margin: 0 4px;
`

const TestCount = styled.span`
  margin: 0 4px;
`