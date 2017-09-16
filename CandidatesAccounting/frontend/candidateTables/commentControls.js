import React from 'react';
import AddIcon from 'material-ui-icons/Add';
import DialogWindow from '../materialUIDecorators/dialogWindow';
import AddCommentForm from './addCommentForm';
import CommentsEditForm from './editCommentForm';
import EditIcon from 'material-ui-icons/ViewList';
import {CreateCandidate} from '../candidates';

export default function CommentControls(props) {
  return (
    <div>
      <DialogWindow
        content={
          <AddCommentForm commentIndex={props.candidate.comments.length} setCandidateEditComment={props.setCandidateEditComment}/>
        }
        label="Add new comment"
        openButtonType="icon"
        openButtonContent={<AddIcon />}
        withSaveButton={true}
        open={function () {
          props.setCandidateEditInfo(CreateCandidate(props.candidate.constructor.name, props.candidate));
        }}
        save={function() {
          props.editCandidate(props.candidate.id, CreateCandidate(props.candidateEditInfo.status, props.candidateEditInfo))
        }}
      />

      <DialogWindow
        content={
          <CommentsEditForm
            setCandidateComment={props.setCandidateEditComment}
            candidateEditInfo={props.candidateEditInfo}
          />}
        label="Comments"
        openButtonType="icon"
        openButtonContent={<EditIcon />}
      />

      {props.candidate.comments ? props.candidate.comments.length : ''}
    </div>
  );
}