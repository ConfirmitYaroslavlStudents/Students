import React from 'react';
import PropTypes from 'prop-types';
import IconButton from '../common/UIComponentDecorators/iconButton';
import UpdateCandidateDialog from './updateCandidateDialog';
import DeleteCandidateDialog from './deleteCandidateDialog';
import CommentIcon from 'material-ui-icons/ViewList';
import Badge from '../common/UIComponentDecorators/badge';
import NavLink from '../../components/common/navLink';
import AddCommentDialog from '../comments/addCommentDialog';

const iconButtonStyle = {
  height: 40,
  width: 40,
};

export default function CandidateControls(props) {
  return (
    <div style={{display: 'flex'}}>
      <AddCommentDialog
        candidate={props.candidate}
        addComment={props.addComment}
        username={props.username}
        subscribe={props.subscribe}
        unsubscribe={props.unsubscribe}
        disabled={props.username === ''}
      />
      <NavLink onClick={() => {
        props.setPageTitle(props.candidate.name);
        props.history.replace('/' + props.candidate.status.toLowerCase() + 's/' + props.candidate.id + '/comments');
      }}>
        <Badge badgeContent={props.candidate.comments.length}>
          <IconButton
            icon={<CommentIcon />}
            style={iconButtonStyle}
          />
        </Badge>
      </NavLink>
      <UpdateCandidateDialog
        candidate={props.candidate}
        updateCandidate={props.updateCandidate}
        tags={props.tags}
        username={props.username}
        disabled={props.username === ''}
      />
      <DeleteCandidateDialog
        candidate={props.candidate}
        deleteCandidate={(candidateID) => {
          props.deleteCandidate(candidateID);
          if (props.totalCount - props.offset <= 1) {
            props.setOffset(props.totalCount - 1 - props.candidatesPerPage);
          }
          props.loadCandidates(props.history);
        }}
        disabled={props.username === ''}
      />
    </div>
  );
}

CandidateControls.propTypes = {
  candidate: PropTypes.object.isRequired,
  updateCandidate: PropTypes.func.isRequired,
  deleteCandidate: PropTypes.func.isRequired,
  loadCandidates: PropTypes.func.isRequired,
  username: PropTypes.string.isRequired,
  tags: PropTypes.array.isRequired,
  setSearchRequest: PropTypes.func.isRequired,
  offset: PropTypes.number.isRequired,
  candidatesPerPage: PropTypes.number.isRequired,
  totalCount: PropTypes.number.isRequired,
  setOffset: PropTypes.func.isRequired,
  history: PropTypes.object.isRequired,
};