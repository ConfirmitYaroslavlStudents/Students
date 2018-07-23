import React, { Component } from 'react'
import PropTypes from 'prop-types'
import styled from 'styled-components'
import { connect } from 'react-redux'
import IconButton from '../commonComponents/UIDecorators/iconButton'
import MenuIcon from '@material-ui/icons/menu'
import ArrowLeftIcon from '@material-ui/icons/KeyboardArrowLeft'
import ExpansionPanel from '../commonComponents/UIDecorators/outlineExpansionPanel'

class SideMenu extends Component {
  constructor(props) {
    super(props)
    this.state = { open: false }
  }

  handleOpen = () => {
    this.setState({ open: true })
  }

  handleClose = () => {
    this.setState({ open: false })
  }

  render() {
    const { open } = this.state
    const { fallenTests } = this.props

    const fallenTestsArray = Object.keys(fallenTests).map(testIndex => fallenTests[testIndex])

    const fallenTestList =
      fallenTestsArray.map((test, index) =>
        <ExpansionPanel key={index} summary={test.fixtureName}>
          {test.testName}
        </ExpansionPanel>
      )

    const menu =
      open ?
        <React.Fragment>
          <Menu onClick={(e) => {e.stopPropagation()}}>
            <Controls>
              <Header>Fallen test list</Header>
              <CloseButtonWrapper>
                <IconButton icon={<ArrowLeftIcon color='secondary' />} onClick={this.handleClose} />
              </CloseButtonWrapper>
            </Controls>
            {fallenTestList}
          </Menu>
          <CloseArea onClick={this.handleClose}/>
        </React.Fragment>
        :
        null

    return (
      <React.Fragment>
        <MenuButtonWrapper>
          <IconButton icon={<MenuIcon />} onClick={this.handleOpen}/>
        </MenuButtonWrapper>
        {menu}
      </React.Fragment>
    )
  }
}

SideMenu.propTypes = {
  fallenTests: PropTypes.object.isRequired
}

export default connect(state => ({
  fallenTests: state.fallenTests
}))(SideMenu)

const MenuButtonWrapper = styled.div`
  display: inline-block;
  position: absolute;
  left: 4px;
  top: 14px;
`

const Menu = styled.div`
  position: fixed;
  left: 0;
  top: 0;
  z-index: 90;
  background-color: #EEEEEE;
  box-shadow: 0 0 5px 3px rgba(0, 0, 0, 0.2);
  width: 400px;
  height: 100%;  
`

const Controls = styled.div`
  display: flex;
  align-items: center;
  position: relative;
  background-color: #29B6F6;
  height: 40px;
  padding: 4px;
`

const Header = styled.p`
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

const CloseArea = styled.div`
  position: fixed;
  left: 0;
  top: 0;
  width: 100%;
  height: 100%;
  z-index: 80;
`