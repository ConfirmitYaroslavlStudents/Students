import React from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import Logo from 'material-ui-icons/AccountCircle'
import SearchForm from './searchForm'
import UserControls from './userControls'
import { AppbarWrapper, AppbarControlsWrapper, AppbarTitleWrapper } from '../common/styledComponents'

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