import React from 'react';
import AddIcon from 'material-ui-icons/Add';
import DialogWindow from '../materialUIDecorators/dialogWindow';
import AddCommentForm from './addCommentForm';
import EditCommentForm from './editCommentForm';
import ViewListIcon from 'material-ui-icons/ViewList';
import {CreateCandidate} from '../candidates';

export default function CommentControls(props) {
  return (
    <div>
      <DialogWindow
        content={
          <AddCommentForm commentIndex={props.candidate.comments ? props.candidate.comments.length : 0} setCandidateEditComment={props.setTempCandidateComment}/>
        }
        label="Add new comment"
        openButtonType="icon"
        openButtonContent={<AddIcon />}
        acceptButtonContent={<div><AddIcon /> add</div>}
        open={function () {
          props.setTempCandidate(CreateCandidate(props.candidate.constructor.name, props.candidate));
        }}
        accept={function() {
          props.editCandidate(props.candidate.id, CreateCandidate(props.tempCandidate.status, props.tempCandidate))
        }}
      />

      <DialogWindow
        content={
          <EditCommentForm
            setCandidateComment={props.setTempCandidateComment}
            candidateEditInfo={props.tempCandidate}
          />}
        label="Comments"
        openButtonType="icon"
        openButtonContent={<ViewListIcon />}
        withoutAcceptButton={true}
      />

      {props.candidate.comments ? props.candidate.comments.length : 0}
    </div>
  );
}