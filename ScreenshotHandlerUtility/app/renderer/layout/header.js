import React from 'react'
import styled from 'styled-components'
import SideMenu from '../sideMenu/menu'

const Header = (props) => {
  return (
    <HeaderWrapper>
      <SideMenuButtonWrapper>
        <SideMenu />
      </SideMenuButtonWrapper>
      {props.children}
    </HeaderWrapper>
  )
}

export default Header

const HeaderWrapper = styled.div`
  position: relative;
  background-color: #F3F3F3;
  padding: 8px;
  text-align: center;
`

const SideMenuButtonWrapper = styled.div`
  display: inline-block;
  position: absolute;
  left: 7px;
  top: 7px;
`