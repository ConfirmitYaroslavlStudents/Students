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
          applicationStatus={props.applicationStatus}
          username={props.username}
          login={props.login}
          logout={props.logout}
          notifications={props.notifications}
          noticeNotification={props.noticeNotification}
          deleteNotification={props.deleteNotification}
          getCandidate={props.getCandidate}
          history={props.history}/>
      </AppbarControlsWrapper>
    </AppbarWrapper>
  )
}

Appbar.propTypes = {
  title: PropTypes.string.isRequired,
  applicationStatus: PropTypes.string.isRequired,
  username: PropTypes.string.isRequired,
  login: PropTypes.func.isRequired,
  logout: PropTypes.func.isRequired,
  notifications: PropTypes.object.isRequired,
  noticeNotification: PropTypes.func.isRequired,
  deleteNotification: PropTypes.func.isRequired,
  searchRequest: PropTypes.string.isRequired,
  getCandidate: PropTypes.func.isRequired,
  loadCandidates: PropTypes.func.isRequired,
  history: PropTypes.object.isRequired,
  setState: PropTypes.func.isRequired,
}