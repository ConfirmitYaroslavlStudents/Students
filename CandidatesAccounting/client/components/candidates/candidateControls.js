import React from 'react';
import PropTypes from 'prop-types';
import IconButton from '../common/UIComponentDecorators/iconButton';
import RemoveIcon from 'material-ui-icons/Delete';
import EditCandidateDialog from './editCandidateDialog';
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

      <NavLink to={'/' + props.candidate.constructor.name.toLowerCase() + 's/' + props.candidate.id + '/comments'}>
        <Badge badgeContent={props.candidate.comments.length} badgeStyle="comment-badge">
          <IconButton
            icon={<CommentIcon />}
            style={iconButtonStyle}
          />
        </Badge>
      </NavLink>

      <EditCandidateDialog
        candidate={props.candidate}
        editCandidate={props.editCandidate}
        tags={props.tags}
        userName={props.userName}
      />

      <IconButton
        icon={<RemoveIcon />}
        style={iconButtonStyle}
        color="accent"
        onClick={() => { props.deleteCandidate(props.candidate.id) }}
      />
    </div>
  );
}

CandidateControls.propTypes = {
  candidate: PropTypes.object.isRequired,
  editCandidate: PropTypes.func.isRequired,
  deleteCandidate: PropTypes.func.isRequired,
  userName: PropTypes.string.isRequired,
  tags: PropTypes.object.isRequired,
};