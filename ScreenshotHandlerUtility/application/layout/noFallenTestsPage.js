import React, { Component } from 'react'
import PropTypes from 'prop-types'
import styled from 'styled-components'
import { connect } from 'react-redux'
import * as actions from '../actions'
import { CheckIcon, iconModifiers } from 'confirmit-icons-material'
import { IconButton } from 'confirmit-react-components'

class NoFallenTestsPage extends Component {
  render() {
    const { testTotalAmount, close } = this.props

    return (
      <Wrapper>
        <MessageWrapper>
          <p>All screenshots ({testTotalAmount}) have passed successfully!</p>
          <ButtonWrapper>
            <IconButton onClick={close}>
              <CheckIcon className='primary-icon' size={iconModifiers.size.size37px}/>
            </IconButton>
          </ButtonWrapper>
        </MessageWrapper>
      </Wrapper>
    )
  }
}

NoFallenTestsPage.propTypes = {
  testTotalAmount: PropTypes.number.isRequired,
  close: PropTypes.func.isRequired
}

export default connect(state => ({
  testTotalAmount: state.testTotalAmount
}), actions)(NoFallenTestsPage)

const Wrapper = styled.div`
  display: flex;
  align-items: center;
  width: 100%;
  height: 100%;
  padding-top: 300px;
`

const MessageWrapper = styled.div`
  display: inline-flex;
  flex-direction: column;
  margin: auto;
  color: #7CB342;
  font-size: 190%;
  justify-content: center;
`

const ButtonWrapper = styled.div`
  display:inline-flex;
  margin: 12px auto 0 auto;
`