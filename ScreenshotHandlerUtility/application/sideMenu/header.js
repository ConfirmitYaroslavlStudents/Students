import React, { Component } from 'react'
import PropTypes from 'prop-types'
import styled from 'styled-components'
import IconButton from '../commonComponents/UIDecorators/iconButton'
import ArrowLeftIcon from '@material-ui/icons/KeyboardArrowLeft'

class SideMenuHeader extends Component {
  render() {
    const { onClose } = this.props

    return (
      <HeaderWrapper>
        <Title>Fallen test list</Title>
        <CloseButtonWrapper>
          <IconButton icon={<ArrowLeftIcon color='secondary' />} onClick={onClose} />
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
  background-color: #29B6F6;
  height: 48px;
  padding: 4px;
`

const Title = styled.p`
  color: #fff;
  margin: auto;
  font-size: 120%;
`

const CloseButtonWrapper = styled.div`
  display: inline-block;
  position: absolute;
  right: 4px;
  top: 4px;
`