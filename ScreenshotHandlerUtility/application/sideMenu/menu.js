import React, { Component } from 'react'
import PropTypes from 'prop-types'
import styled from 'styled-components'
import { connect } from 'react-redux'
import * as actions from '../actions'
import IconButton from '../commonComponents/UIDecorators/iconButton'
import MenuIcon from '@material-ui/icons/menu'
import Header from './header'
import TestList from './testList'

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

  handleTestItemClick = (test) => {
    this.props.setCurrentTestIndex({ index: test.index })
    this.handleClose()
  }

  render() {
    const { open } = this.state
    const { fallenTests, currentTestIndex } = this.props

    const fallenTestsArray = Object.keys(fallenTests).map(testIndex => fallenTests[testIndex])

    const sideMenu =
      open ?
        <React.Fragment>
          <SideMenuWrapper onClick={(e) => {e.stopPropagation()}}>
            <Header onClose={this.handleClose} />
            <TestList fallenTests={fallenTestsArray} currentTestIndex={currentTestIndex} onTestItemClick={this.handleTestItemClick} />
          </SideMenuWrapper>
          <SideMenuCloseArea onClick={this.handleClose} />
        </React.Fragment>
        :
        null

    return (
      <React.Fragment>
        <IconButton icon={<MenuIcon />} onClick={this.handleOpen} />
        {sideMenu}
      </React.Fragment>
    )
  }
}

SideMenu.propTypes = {
  fallenTests: PropTypes.object.isRequired,
  currentTestIndex: PropTypes.number.isRequired,
  setCurrentTestIndex: PropTypes.func.isRequired
}

export default connect(state => ({
  fallenTests: state.fallenTests,
  currentTestIndex: state.currentTestIndex
}), actions)(SideMenu)

const SideMenuWrapper = styled.div`
  position: fixed;
  left: 0;
  top: 0;
  z-index: 90;
  background-color: #fff;
  box-shadow: 0 0 6px 4px rgba(0, 0, 0, 0.2);
  height: 100%;  
`

const SideMenuCloseArea = styled.div`
  position: fixed;
  left: 0;
  top: 0;
  width: 100%;
  height: 100%;
  z-index: 80;
  background-color: rgba(0, 0, 0, 0.5)
`