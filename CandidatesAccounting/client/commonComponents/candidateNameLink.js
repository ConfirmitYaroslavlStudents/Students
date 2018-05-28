import React from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as commentActions from '../comments/actions'
import { SELECTORS } from '../rootReducer'
import styled from 'styled-components'

function CandidateNameLink(props) {
  const { fetching, onDeleting, candidate, openCommentPage, children } = props

  const candidateOnDeleting = candidate.id === onDeleting

  const handleClick = () => {
    if (!fetching && !candidateOnDeleting) {
      openCommentPage({ candidate })
    }
  }

  return (
    <LinkWrapper onClick={handleClick}>
      {children}
    </LinkWrapper>
  )
}

CandidateNameLink.propTypes = {
  candidate: PropTypes.object.isRequired,
  fetching: PropTypes.bool.isRequired,
  onDeleting: PropTypes.string.isRequired,
  openCommentPage: PropTypes.func.isRequired
}

export default connect(state => ({
    fetching: SELECTORS.APPLICATION.FETCHING(state),
    onDeleting: SELECTORS.CANDIDATES.ONDELETING(state)
  }
), commentActions)(CandidateNameLink)

const LinkWrapper = styled.span`
  white-space: nowrap;
  cursor: pointer;
  padding: 4px;
  
  &:hover {
    color: #2196F3;
  }
`