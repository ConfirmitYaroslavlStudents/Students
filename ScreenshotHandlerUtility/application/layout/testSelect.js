import React, { Component } from 'react'
import PropTypes from 'prop-types'
import styled from 'styled-components'
import IconButton from '../commonComponents/UIDecorators/iconButton'
import ArrowLeftIcon from '@material-ui/icons/KeyboardArrowLeft'
import ArrowRightIcon from '@material-ui/icons/KeyboardArrowRight'

class TestSelect extends Component {
  render() {
    const { onBackClick, onNextClick, currentTestIndex, testTotalAmount } = this.props

    return (
      <Wrapper>
        <IconButton
          icon={<ArrowLeftIcon color='secondary'/>}
          onClick={onBackClick}
          disabled={currentTestIndex === 0}
        />
        <TestCount>{currentTestIndex + 1} of {testTotalAmount}</TestCount>
        <IconButton
          icon={<ArrowRightIcon color='secondary'/>}
          onClick={onNextClick}
          disabled={currentTestIndex === testTotalAmount - 1}
        />
      </Wrapper>
    )
  }
}

TestSelect.propTypes = {
  onBackClick: PropTypes.func.isRequired,
  onNextClick: PropTypes.func.isRequired,
  currentTestIndex: PropTypes.number.isRequired,
  testTotalAmount: PropTypes.number.isRequired
}

export default TestSelect

const Wrapper = styled.div`
  display: inline-block;
  margin: 0 4px;
`

const TestCount = styled.span`
  margin: 0 4px;
`