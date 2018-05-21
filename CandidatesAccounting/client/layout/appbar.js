import React from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import { SELECTORS } from '../rootReducer'
import Logo from 'material-ui-icons/AccountCircle'
import SearchForm from './searchForm'
import UserControls from '../authorization/components/userControls'
import styled from 'styled-components'

function Appbar(props) {
  const { pageTitle } = props

  const searchForm =
    pageTitle === 'Candidate Accounting' ?
      <SearchForm />
      : ''

  return (
    <AppbarWrapper>
      <AppbarTitleWrapper>
        <Logo /> {pageTitle}
      </AppbarTitleWrapper>
      <AppbarControlsWrapper>
        {searchForm}
        <UserControls />
      </AppbarControlsWrapper>
    </AppbarWrapper>
  )
}

Appbar.propTypes = {
  pageTitle: PropTypes.string.isRequired
}

export default connect(state => ({
    pageTitle: SELECTORS.APPLICATION.PAGETITLE(state)
  }
))(Appbar)

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