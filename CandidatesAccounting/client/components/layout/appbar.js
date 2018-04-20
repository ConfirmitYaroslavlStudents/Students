import React from 'react'
import PropTypes from 'prop-types'
import Logo from 'material-ui-icons/AccountCircle'
import SearchForm from './searchForm'
import UserControls from './userControls'
import { AppbarWrapper, AppbarControlsWrapper, AppbarTitleWrapper } from '../common/styledComponents'

export default function Appbar(props) {
  const searchForm =
    props.title === 'Candidate Accounting' ?
      <SearchForm history={props.history} />
      : ''

  return (
    <AppbarWrapper>
      <AppbarTitleWrapper>
        <Logo /> {props.title}
      </AppbarTitleWrapper>
      <AppbarControlsWrapper>
        {searchForm}
        <UserControls
          history={props.history}/>
      </AppbarControlsWrapper>
    </AppbarWrapper>
  )
}

Appbar.propTypes = {
  title: PropTypes.string.isRequired,
  history: PropTypes.object.isRequired,
}