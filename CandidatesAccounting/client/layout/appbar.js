import React from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import { SELECTORS } from '../rootReducer'
import Logo from 'material-ui-icons/AccountCircle'
import SearchForm from './searchForm'
import UserControls from '../authorization/components/userControls'
import UpdateCandidateDialog from '../candidates/components/common/updateCandidateDialog'
import styled from 'styled-components'

function Appbar(props) {
  const { pageTitle, candidates, authorized } = props

  const searchForm =
    pageTitle === 'Candidate Accounting' ?
      <SearchForm />
      : ''

  // TODO: decide is the button useful
  const updateCandidateButton =
    pageTitle !== 'Candidate Accounting' && false ?
      <UpdateCandidateDialog candidate={candidates[Object.keys(candidates)[0]]} disabled={!authorized}/>
      : ''

  return (
    <AppbarWrapper>
      <AppbarTitleWrapper>
        <Logo /> {pageTitle}
        { updateCandidateButton }
      </AppbarTitleWrapper>
      <AppbarControlsWrapper>
        {searchForm}
        <UserControls />
      </AppbarControlsWrapper>
    </AppbarWrapper>
  )
}

Appbar.propTypes = {
  pageTitle: PropTypes.string.isRequired,
  authorized: PropTypes.bool.isRequired,
  candidates: PropTypes.object.isRequired
}

export default connect(state => ({
    pageTitle: SELECTORS.APPLICATION.PAGETITLE(state),
    authorized: SELECTORS.AUTHORIZATION.AUTHORIZED(state),
    candidates: SELECTORS.CANDIDATES.CANDIDATES(state)
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