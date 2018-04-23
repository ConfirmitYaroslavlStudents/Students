import React from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import Logo from 'material-ui-icons/AccountCircle'
import SearchForm from './searchForm'
import UserControls from './userControls'
import styled from 'styled-components'

function Appbar(props) {
  const { pageTitle, history } = props

  const searchForm =
    pageTitle === 'Candidate Accounting' ?
      <SearchForm history={history} />
      : ''

  return (
    <AppbarWrapper>
      <AppbarTitleWrapper>
        <Logo /> {pageTitle}
      </AppbarTitleWrapper>
      <AppbarControlsWrapper>
        {searchForm}
        <UserControls history={history} />
      </AppbarControlsWrapper>
    </AppbarWrapper>
  )
}

Appbar.propTypes = {
  history: PropTypes.object.isRequired,
}

export default connect(state => {
  return {
    pageTitle: state.pageTitle
  }
})(Appbar)

const AppbarControlsWrapper = styled.div`
  display: inline-flex;
  position: absolute;
  right: 6px;
`

const AppbarTitleWrapper = styled.div`
  display: inline-flex;
  position: absolute;
  left: 16px;
  font-size: 125%;
`

const AppbarWrapper = styled.div`
  display: flex;
  align-items: center;
  width: 100%;
  height: 60px;
`