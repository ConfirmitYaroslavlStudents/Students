import React from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as commentActions from '../../actions/commentActions'
import { MediumButtonStyle } from '../common/styleObjects'
import IconButton from '../common/UIComponentDecorators/iconButton'
import UpdateCandidateDialog from './updateCandidateDialog'
import DeleteCandidateDialog from './deleteCandidateDialog'
import CommentIcon from 'material-ui-icons/ViewList'
import Badge from '../common/UIComponentDecorators/badge'
import NavLink from '../../components/common/navLink'
import AddCommentDialog from '../comments/addCommentDialog'
import Spinner from '../common/UIComponentDecorators/spinner'
import styled from 'styled-components'

function CandidateControls(props) {
  const { fetching, authorized, onUpdating, onDeleting, candidate, openCommentPage } = props

  const candidateOnUpdating = candidate.id === onUpdating
  const candidateOnDeleting = candidate.id === onDeleting

  const handleCandidateCommentPageOpen = () => {
    if (!fetching && !candidateOnDeleting) {
      openCommentPage({ candidate })
    }
  }

  const updateCandidateDialog =
    candidateOnUpdating ?
      <SpinnerWrapper><Spinner size={26}/></SpinnerWrapper>
      :
      <UpdateCandidateDialog
        candidate={candidate}
        disabled={fetching || candidateOnDeleting || !authorized}
      />

  const deleteCandidateDialog =
    candidateOnDeleting ?
      <SpinnerWrapper><Spinner size={26}/></SpinnerWrapper>
      :
      <DeleteCandidateDialog
        candidateId={candidate.id}
        disabled={fetching || candidateOnUpdating || !authorized}
      />

  return (
    <div className='flex'>
      <AddCommentDialog
        candidate={candidate}
        disabled={fetching || candidateOnDeleting || !authorized}
      />
      <NavLink onClick={handleCandidateCommentPageOpen}>
        <Badge badgeContent={candidate.commentAmount} disabled={fetching}>
          <IconButton
            icon={<CommentIcon />}
            style={MediumButtonStyle}
            disabled={fetching || candidateOnDeleting}
          />
        </Badge>
      </NavLink>
      { updateCandidateDialog }
      { deleteCandidateDialog }
    </div>
  )
}

CandidateControls.propTypes = {
  candidate: PropTypes.object.isRequired
}

export default connect(state => {
  return {
    fetching: state.fetching,
    authorized: state.authorized,
    onUpdating: state.onUpdating,
    onDeleting: state.onDeleting
  }
}, commentActions)(CandidateControls)

const SpinnerWrapper = styled.div`
  display: inline-flex;
  align-items: center;
  padding-left: 7px;
  box-sizing: border-box;
  width: 40px;
  height: 40px;
`