import React from 'react';
import PropTypes from 'prop-types';
import IconButton from '../UIComponentDecorators/iconButton';
import RemoveIcon from 'material-ui-icons/Delete';
import EditCandidateDialog from './editCandidateDialog';
import CommentIcon from 'material-ui-icons/ViewList';
import Badge from '../UIComponentDecorators/badge';
import {NavLink} from 'react-router-dom';
import AddCommentDialog from '../commentComponents/addCommentDialog';
import Tooltip from '../UIComponentDecorators/tooltip';

const iconButtonStyle = {
  height: 40,
  width: 40,
};

export default function CandidateControls(props) {
  return (
    <div style={{float: 'right'}}>
      <Tooltip title="Add comment">
        <AddCommentDialog
          candidate={props.candidate}
          addComment={props.addComment}
          userName={props.userName}
        />
      </Tooltip>

      <Tooltip title="View comment list">
        <NavLink to={'/' + props.candidate.constructor.name.toLowerCase() + 's/' + props.candidate.id + '/comments'}>
          <Badge badgeContent={props.candidate.comments.length} badgeStyle="comment-badge">
            <IconButton
              icon={<CommentIcon />}
              style={iconButtonStyle}
            />
          </Badge>
        </NavLink>
      </Tooltip>

      <Tooltip title="Edit candidate">
        <EditCandidateDialog
          candidate={props.candidate}
          editCandidate={props.editCandidate}
          tags={props.tags}
          userName={props.userName}
        />
      </Tooltip>

      <Tooltip title="Remove candidate">
        <IconButton
          icon={<RemoveIcon />}
          style={iconButtonStyle}
          color="accent"
          onClick={() => { props.deleteCandidate(props.candidate.id) }}
        />
      </Tooltip>
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