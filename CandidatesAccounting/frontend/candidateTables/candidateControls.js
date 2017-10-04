import React from 'react';
import PropTypes from 'prop-types';
import IconButton from '../materialUIDecorators/iconButton';
import DialogWindow from '../materialUIDecorators/dialogWindow';
import EditCandidateForm from './editCandidateForm';
import {CreateCandidate} from '../candidates/index';
import SaveIcon from 'material-ui-icons/Save';
import AddIcon from 'material-ui-icons/Add';
import RemoveIcon from 'material-ui-icons/Delete';
import EditIcon from 'material-ui-icons/Edit';
import AddCommentForm from './addCommentForm';
import ViewListIcon from 'material-ui-icons/ViewList';
import Badge from '../materialUIDecorators/badge';
import {NavLink} from 'react-router-dom';

export default function CandidateControls(props) {
  return (
    <div className="float-right">
      <DialogWindow
        content={
          <AddCommentForm
            commentIndex={props.candidate.comments.length}
            setTempCandidateComment={props.setTempCandidateComment}
            candidate={props.candidate}
          />}
        label="Add new comment"
        openButtonType="icon"
        openButtonContent={<AddIcon />}
        acceptButtonContent={<div className="button-content"><AddIcon/> <span style={{marginTop: 3}}>add</span></div>}
        open={function () {
          props.setTempCandidate(CreateCandidate(props.candidate.constructor.name, props.candidate));
        }}
        accept={function() {
          props.editCandidate(props.candidate.id, CreateCandidate(props.tempCandidate.status, props.tempCandidate))
        }}
        close={function() {
          props.setTempCandidate(CreateCandidate(props.candidate.constructor.name, {}));
        }}
      />
      <NavLink to={'/' + props.candidate.constructor.name.toLowerCase() + 's/' + props.candidate.id + '/comments'}>
        <Badge badgeContent={props.candidate.comments.length} badgeStyle="comment-badge">
          <IconButton
            icon={<ViewListIcon />}
            onClick={function () { }}
          />
        </Badge>
      </NavLink>
      <DialogWindow
        content={
          <EditCandidateForm
            changeTempCandidateInfo={props.changeTempCandidateInfo}
            setTempCandidateComment={props.setTempCandidateComment}
            editCandidate={props.editCandidate}
            tempCandidate={props.tempCandidate}
            setTempCandidate={props.setTempCandidate}
          />}
        label="Candidate edit"
        openButtonType="icon"
        openButtonContent={<EditIcon/>}
        acceptButtonContent={<div className="button-content"><SaveIcon/> <span style={{marginTop: 3}}> save</span></div>}
        open={function() {
          props.setTempCandidate(CreateCandidate(props.candidate.constructor.name, props.candidate));
        }}
        accept={function() {
          props.editCandidate(props.candidate.id, CreateCandidate(props.tempCandidate.status, props.tempCandidate))
        }}
        close={function() {
          props.setTempCandidate(CreateCandidate('Candidate', {}))
        }}
      />
      <IconButton
        icon={<RemoveIcon />}
        color="accent"
        onClick={function () { props.deleteCandidate(props.candidate.id) }}
      />
    </div>
  );
}

CandidateControls.propTypes = {
  candidate: PropTypes.object.isRequired,
  tempCandidate: PropTypes.object.isRequired,
  setTempCandidate: PropTypes.func.isRequired,
  changeTempCandidateInfo: PropTypes.func.isRequired,
  editCandidate: PropTypes.func.isRequired,
  deleteCandidate: PropTypes.func.isRequired,
};