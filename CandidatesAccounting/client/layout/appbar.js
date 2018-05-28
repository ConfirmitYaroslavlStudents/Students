import React from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import { SELECTORS } from '../rootReducer'
import Logo from '@material-ui/icons/AccountCircle'
import SearchForm from './searchForm'
import UserControls from '../authorization/components/userControls'
import NicknameWrapper from '../commonComponents/nicknameWrapper'
import Avatar from '../commonComponents/UIComponentDecorators/avatar'
import styled from 'styled-components'

function Appbar(props) {
  const { pageTitle, candidates, currentCandidateId } = props

  let logoContent = ''
  let pageTitleContent = ''
  let searchForm = ''

  if (pageTitle === 'Candidate Accounting' && currentCandidateId === '') {
    logoContent = <Logo />
    pageTitleContent = <span>{pageTitle}</span>
    searchForm = <SearchForm />
  } else {
    logoContent =
      <Avatar
        src={'/' + candidates[currentCandidateId].name.toLowerCase() + 's/' + currentCandidateId + '/avatar'}
        alt='L'
      />
    pageTitleContent =
      <div className='inline-flex'>
        {candidates[currentCandidateId].name}
        <NicknameWrapper nickname={candidates[currentCandidateId].nickname} />
      </div>
    searchForm = ''
  }

  return (
    <AppbarWrapper>
      <AppbarTitleWrapper>
        {logoContent}
        <PageTitleWrapper>{pageTitleContent}</PageTitleWrapper>
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
  candidates: PropTypes.object.isRequired,
  currentCandidateId: PropTypes.string.isRequired
}

export default connect(state => ({
    pageTitle: SELECTORS.APPLICATION.PAGETITLE(state),
    candidates: SELECTORS.CANDIDATES.CANDIDATES(state),
    currentCandidateId: SELECTORS.COMMENTS.CURRENTCANDIDATEID(state)
  }
))(Appbar)

const AppbarControlsWrapper = styled.div`
  display: inline-flex;
  position: absolute;
  right: 6px;
`

const AppbarTitleWrapper = styled.div`
  display: inline-flex;
  align-items: center;
  position: absolute;
  left: 16px;
  font-size: 125%;
`

const PageTitleWrapper = styled.div`
  display: inline-flex;
  margin-left: 4px;
`

const AppbarWrapper = styled.div`
  display: flex;
  align-items: center;
  width: 100%;
  height: 60px;
`