import React, { Component } from 'react'
import PropTypes from 'prop-types'
import styled from 'styled-components'
import { IconButton } from 'confirmit-react-components'
import { ChevronIcon, iconModifiers } from 'confirmit-icons-material'

class SideMenuHeader extends Component {
  render() {
    const { onClose } = this.props

    return (
      <HeaderWrapper>
        <Title>Fallen screenshot list</Title>
        <CloseButtonWrapper>
          <IconButton onClick={onClose}>
            <ChevronIcon className='contrast-icon' size={iconModifiers.size.size20px} turn={iconModifiers.turn.around} />
          </IconButton>
        </CloseButtonWrapper>
      </HeaderWrapper>
    )
  }
}

SideMenuHeader.propTypes = {
  onClose: PropTypes.func.isRequired
}

export default SideMenuHeader

const HeaderWrapper = styled.div`
  display: flex;
  align-items: center;
  position: relative;
  background-color: #7CB342;
  height: 55px;
  padding: 4px;
`

const Title = styled.p`
  color: #fff;
  margin: auto;
  font-size: 120%;
  font-weight: bold; 
`

const CloseButtonWrapper = styled.div`
  display: inline-block;
  position: absolute;
  right: 4px;
  top: 4px;
`