import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import { SELECTORS } from '../rootReducer'
import Logo from '@material-ui/icons/AccountCircle'
import SearchForm from './searchForm'
import UserControls from '../authorization/components/userControls'
import NicknameWrapper from '../commonComponents/nicknameWrapper'
import LetterAvatar from '../commonComponents/UIComponentDecorators/letterAvatar'
import ImageAvatar from '../commonComponents/UIComponentDecorators/imageAvatar'
import styled from 'styled-components'

class Appbar extends Component {
  render() {
    const { pageTitle, candidates, currentCandidateId } = this.props

    const candidate = candidates[currentCandidateId]

    let logoContent = <Logo style={{width: 38, height: 38}}/>
    let pageTitleContent = <span>{pageTitle}</span>
    let searchForm = <SearchForm/>

    if (pageTitle !== 'Candidate Accounting' && candidate) {
      logoContent =
        candidate && candidate.hasAvatar ?
          <ImageAvatar
            source={'/' + candidate.name.toLowerCase() + 's/' + candidate.id + '/avatar'}
            alternative={candidate.name[0]}
          />
          :
          <LetterAvatar letters={candidate.name[0]} invertedColors/>

      pageTitleContent =
        <React.Fragment>
          {candidates[currentCandidateId].name}
          <NicknameWrapper nickname={candidates[currentCandidateId].nickname}/>
        </React.Fragment>

      searchForm = null
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

const AppbarWrapper = styled.div`
  display: flex;
  align-items: center;
  width: 100%;
  height: 60px;
`

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