import React, { Component } from 'react'
import PropTypes from 'prop-types'
import CopyButton from './copyButton'
import styled from 'styled-components'

class PhoneNumberWrapper extends Component {
  constructor(props) {
    super(props)
    this.state = { isFocused: false }
  }

  handleMouseOver = () => {
    this.setState({ isFocused: true })
  }

  handleMouseLeave = () => {
    this.setState({ isFocused: false })
  }

  render() {
    const { isFocused } = this.state
    const { number, children } = this.props

    const copyButton =
      isFocused && number !== '' ?
        <CopyButton text={number}/>
        :
        null

    return (
      <Wrapper onMouseOver={this.handleMouseOver} onMouseLeave={this.handleMouseLeave} withButton={isFocused}>
        <a className='link' href={'tel:' + number}>
          {children}
        </a>
        {copyButton}
      </Wrapper>
    )
  }
}

PhoneNumberWrapper.propTypes = {
  number: PropTypes.string.isRequired
}

export default PhoneNumberWrapper

const Wrapper = styled.div`
  display: inline-flex;
  padding: 4px;
  padding-right: 30px;
  
  ${props => props.withButton &&`
    padding-right: 4px;
	`}
`