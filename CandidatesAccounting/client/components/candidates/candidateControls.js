import React from 'react';
import PropTypes from 'prop-types';
import IconButton from '../common/UIComponentDecorators/iconButton';
import UpdateCandidateDialog from './updateCandidateDialog';
import DeleteCandidateDialog from './deleteCandidateDialog';
import CommentIcon from 'material-ui-icons/ViewList';
import Badge from '../common/UIComponentDecorators/badge';
import {NavLink} from 'react-router-dom';
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
        userName={props.userName}
      />
      <NavLink to={'/' + props.candidate.constructor.name.toLowerCase() + 's/' + props.candidate.id + '/comments'}
               onClick={()=>{props.setSearchRequest('', props.history, 0)}}>
        <Badge badgeContent={props.candidate.comments.length} badgeStyle="comment-badge">
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
        userName={props.userName}
      />
      <DeleteCandidateDialog
        candidate={props.candidate}
        deleteCandidate={props.deleteCandidate}
      />
    </div>
  );
}

CandidateControls.propTypes = {
  candidate: PropTypes.object.isRequired,
  updateCandidate: PropTypes.func.isRequired,
  deleteCandidate: PropTypes.func.isRequired,
  userName: PropTypes.string.isRequired,
  tags: PropTypes.object.isRequired,
  setSearchRequest: PropTypes.func.isRequired,
  history: PropTypes.object.isRequired,
};