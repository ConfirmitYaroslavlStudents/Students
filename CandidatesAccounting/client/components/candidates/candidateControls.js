import React from 'react'
import PropTypes from 'prop-types'
import { FlexDiv } from '../common/styledComponents'
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
  const authorized = props.username !== ''
  const onRefreshing = props.applicationStatus === 'refreshing'
  const onUpdating = props.applicationStatus.slice(0, 8) === 'updating' && props.applicationStatus.slice(9) === props.candidate.id
  const onDeleting = props.applicationStatus.slice(0, 8) === 'deleting' && props.applicationStatus.slice(9) === props.candidate.id

  const openCandidateCommentPage = () => {
    if (!onRefreshing && !onDeleting) {
      props.history.replace('/' + props.candidate.status.toLowerCase() + 's/' + props.candidate.id + '/comments')
    }
  }

  const commentAmount = Object.keys(props.candidate.comments).length

  const deleteCandidate = (candidateID) => {
    props.deleteCandidate(candidateID)
    props.loadCandidates({
        applicationStatus: 'deleting-' + candidateID,
        offset: props.totalCount - props.offset <= 1 ? props.totalCount - 1 - props.candidatesPerPage : props.offset
      },
      props.history)
  }

  return (
    <FlexDiv>
      <AddCommentDialog
        candidate={props.candidate}
        addComment={props.addComment}
        username={props.username}
        subscribe={props.subscribe}
        unsubscribe={props.unsubscribe}
        disabled={onRefreshing || onDeleting || !authorized}
      />
      <NavLink onClick={openCandidateCommentPage}>
        <Badge badgeContent={commentAmount} disabled={onRefreshing}>
          <IconButton
            icon={<CommentIcon />}
            style={MediumButtonStyle}
            disabled={onRefreshing || onDeleting}
          />
        </Badge>
      </NavLink>
      {
        onUpdating ?
          <Spinner />
          :
          <UpdateCandidateDialog
            candidate={props.candidate}
            updateCandidate={props.updateCandidate}
            tags={props.tags}
            username={props.username}
            disabled={onRefreshing || onDeleting || !authorized}
          />
      }
      {
        onDeleting ?
          <Spinner />
          :
          <DeleteCandidateDialog
            candidate={props.candidate}
            disabled={onRefreshing || onUpdating || !authorized}
            deleteCandidate={deleteCandidate}
          />
      }
    </FlexDiv>
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
};