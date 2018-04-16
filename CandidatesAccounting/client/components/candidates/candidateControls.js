import React from 'react'
import PropTypes from 'prop-types'
import { MediumButtonStyle } from '../common/styleObjects'
import IconButton from '../common/UIComponentDecorators/iconButton'
import UpdateCandidateDialog from './updateCandidateDialog'
import DeleteCandidateDialog from './deleteCandidateDialog'
import CommentIcon from 'material-ui-icons/ViewList'
import Badge from '../common/UIComponentDecorators/badge'
import NavLink from '../../components/common/navLink'
import AddCommentDialog from '../comments/addCommentDialog'
import Spinner from '../common/UIComponentDecorators/spinner'

export default function CandidateControls(props) {
  const {
    applicationStatus,
    addComment,
    candidate,
    updateCandidate,
    deleteCandidate,
    loadCandidates,
    username,
    tags,
    offset,
    candidatesPerPage,
    totalCount,
    history,
    subscribe,
    unsubscribe
  } = props

  const authorized = username !== ''
  const commentAmount = Object.keys(candidate.comments).length
  const onRefreshing = applicationStatus === 'refreshing'
  const onUpdating = applicationStatus.slice(0, 8) === 'updating' && applicationStatus.slice(9) === candidate.id
  const onDeleting = applicationStatus.slice(0, 8) === 'deleting' && applicationStatus.slice(9) === candidate.id

  const handleCandidateCommentPageOpen = () => {
    if (!onRefreshing && !onDeleting) {
      history.replace('/' + candidate.status.toLowerCase() + 's/' + candidate.id + '/comments')
    }
  }

  const handleCandidateDelete = (candidateID) => {
    deleteCandidate(candidateID)
    loadCandidates({
        applicationStatus: 'deleting-' + candidateID,
        offset: totalCount - offset <= 1 ? totalCount - 1 - candidatesPerPage : offset
      },
      history)
  }

  const updateCandidateDialog =
    onUpdating ?
      <Spinner />
      :
      <UpdateCandidateDialog
        candidate={candidate}
        updateCandidate={updateCandidate}
        tags={tags}
        username={username}
        disabled={onRefreshing || onDeleting || !authorized}
      />

  const deleteCandidateDialog =
    onDeleting ?
      <Spinner />
      :
      <DeleteCandidateDialog
        candidate={candidate}
        disabled={onRefreshing || onUpdating || !authorized}
        deleteCandidate={handleCandidateDelete}
      />

  return (
    <div className='flex'>
      <AddCommentDialog
        candidate={candidate}
        addComment={addComment}
        username={username}
        subscribe={subscribe}
        unsubscribe={unsubscribe}
        disabled={onRefreshing || onDeleting || !authorized}
      />
      <NavLink onClick={handleCandidateCommentPageOpen}>
        <Badge badgeContent={commentAmount} disabled={onRefreshing}>
          <IconButton
            icon={<CommentIcon />}
            style={MediumButtonStyle}
            disabled={onRefreshing || onDeleting}
          />
        </Badge>
      </NavLink>
      { updateCandidateDialog }
      { deleteCandidateDialog }
    </div>
  )
}

CandidateControls.propTypes = {
  candidate: PropTypes.object.isRequired,
  updateCandidate: PropTypes.func.isRequired,
  deleteCandidate: PropTypes.func.isRequired,
  loadCandidates: PropTypes.func.isRequired,
  username: PropTypes.string.isRequired,
  tags: PropTypes.array.isRequired,
  offset: PropTypes.number.isRequired,
  candidatesPerPage: PropTypes.number.isRequired,
  totalCount: PropTypes.number.isRequired,
  history: PropTypes.object.isRequired,
}