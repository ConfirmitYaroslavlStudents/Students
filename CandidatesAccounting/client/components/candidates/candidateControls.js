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
import styled from 'styled-components'

export default function CandidateControls(props) {
  const { applicationStatus, authorizationStatus, username, candidate, history } = props

  const authorized = authorizationStatus === 'authorized'
  const commentAmount = Object.keys(candidate.comments).length
  const onRefreshing = applicationStatus === 'refreshing'
  const onUpdating = applicationStatus.slice(0, 8) === 'updating' && applicationStatus.slice(9) === candidate.id
  const onDeleting = applicationStatus.slice(0, 8) === 'deleting' && applicationStatus.slice(9) === candidate.id

  const handleCandidateCommentPageOpen = () => {
    if (!onRefreshing && !onDeleting) {
      history.replace('/' + candidate.status.toLowerCase() + 's/' + candidate.id + '/comments')
    }
  }

  const updateCandidateDialog =
    onUpdating ?
      <SpinnerWrapper><Spinner size={26}/></SpinnerWrapper>
      :
      <UpdateCandidateDialog
        candidate={candidate}
        disabled={onRefreshing || onDeleting || !authorized}
      />

  const deleteCandidateDialog =
    onDeleting ?
      <SpinnerWrapper><Spinner size={26}/></SpinnerWrapper>
      :
      <DeleteCandidateDialog
        candidateID={candidate.id}
        disabled={onRefreshing || onUpdating || !authorized}
        history={props.history}
      />

  return (
    <div className='flex'>
      <AddCommentDialog
        candidate={candidate}
        username={username}
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
  authorizationStatus: PropTypes.string.isRequired,
  username: PropTypes.string.isRequired,
  history: PropTypes.object.isRequired,
}

const SpinnerWrapper = styled.div`
  display: inline-flex;
  align-items: center;
  padding-left: 7px;
  box-sizing: border-box;
  width: 40px;
  height: 40px;
`