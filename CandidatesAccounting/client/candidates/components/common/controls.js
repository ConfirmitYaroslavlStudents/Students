import React from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as commentActions from '../../../comments/actions'
import { SELECTORS } from '../../../rootReducer'
import AddCommentDialog from '../../../comments/components/addCommentDialog'
import OpenCommentPageButton from './openCommentPageButton'
import UpdateCandidateControl from './updateCandidateControl'
import DeleteCandidateControl from './deleteCandidateControl'

function CandidateControls(props) {
  const { fetching, authorized, onUpdating, onDeleting, candidate, openCommentPage } = props

  const candidateOnUpdating = candidate.id === onUpdating
  const candidateOnDeleting = candidate.id === onDeleting

  const handleCandidateCommentPageOpen = () => {
    if (!fetching && !candidateOnDeleting) {
      openCommentPage({ candidate })
    }
  }

  return (
    <div className='block-div'>
      <AddCommentDialog
        candidate={candidate}
        disabled={fetching || candidateOnDeleting || !authorized}
      />
      <OpenCommentPageButton
        onClick={handleCandidateCommentPageOpen}
        commentAmount={candidate.commentAmount}
        disabled={fetching}
      />
      <UpdateCandidateControl
        candidate={candidate}
        candidateIdOnUpdating={onUpdating}
        disabled={fetching || candidateOnDeleting || !authorized}
      />
      <DeleteCandidateControl
        candidateId={candidate.id}
        candidateIdOnDeleting={onDeleting}
        disabled={fetching || candidateOnUpdating || !authorized}
      />
    </div>
  )
}

CandidateControls.propTypes = {
  candidate: PropTypes.object.isRequired,
  fetching: PropTypes.bool.isRequired,
  authorized: PropTypes.bool.isRequired,
  onUpdating: PropTypes.string.isRequired,
  onDeleting: PropTypes.string.isRequired,
  openCommentPage: PropTypes.func.isRequired
}

export default connect(state => ({
    fetching: SELECTORS.APPLICATION.FETCHING(state),
    authorized: SELECTORS.AUTHORIZATION.AUTHORIZED(state),
    onUpdating: SELECTORS.CANDIDATES.ONUPDATING(state),
    onDeleting: SELECTORS.CANDIDATES.ONDELETING(state)
  }
), commentActions)(CandidateControls)