import React, { Component } from 'react'
import PropTypes from 'prop-types'
import CopyButton from './copyButton'
import styled, { css } from 'styled-components'

export default class EmailWrapper extends Component {
  constructor(props) {
    super(props)

    this.state = { isFocused: false }
  }

  handleMouseOver = () => {
    this.setState({isFocused : true})
  }

  handleMouseLeave = () => {
    this.setState({isFocused : false})
  }

  render() {
    const { isFocused } = this.state
    const { email, children } = this.props
    return (
      <Wrapper onMouseOver={this.handleMouseOver} onMouseLeave={this.handleMouseLeave} withButton={isFocused}>
        <a className='link' href={'mailto:' + email}>
          {children}
        </a>
        {
          isFocused && email && email !== ''  ?
            <CopyButton text={email}/>
            : ''
        }
      </Wrapper>
    )
  }
}

EmailWrapper.propTypes = {
  email: PropTypes.string.isRequired
}

const Wrapper = styled.div`
  display: inline-flex;
  padding: 4px;  
  padding-right: 30px;
  
  ${props => props.withButton && css`
    padding-right: 4px;
	`}
`