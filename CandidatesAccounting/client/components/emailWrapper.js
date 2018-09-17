import React, { Component } from 'react'
import PropTypes from 'prop-types'
import CopyButton from './copyButton'
import styled from 'styled-components'

class EmailWrapper extends Component {
  constructor(props) {
    super(props)
    this.state = { isFocused: false }
  }

  handleMouseOver = () => {
    this.setState({ isFocused : true })
  }

  handleMouseLeave = () => {
    this.setState({ isFocused : false })
  }

  render() {
    const { isFocused } = this.state
    const { email, children } = this.props

    const copyButton =
      isFocused && email && email !== '' ?
        <CopyButton text={email}/>
        :
        null

    return (
      <Wrapper onMouseOver={this.handleMouseOver} onMouseLeave={this.handleMouseLeave} withButton={isFocused}>
        <a className='link' href={'mailto:' + email}>
          {children}
        </a>
        {copyButton}
      </Wrapper>
    )
  }
}

EmailWrapper.propTypes = {
  email: PropTypes.string.isRequired
}

export default EmailWrapper

const Wrapper = styled.div`
  display: inline-flex;
  padding: 4px;  
  padding-right: 30px;
  
  ${props => props.withButton &&`
    padding-right: 4px;
	`}
`