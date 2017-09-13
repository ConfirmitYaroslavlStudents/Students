import React from 'react';
import DialogWindow from '../materialUIDecorators/dialogWindow';
import CommentsEditForm from './commentsEditForm';
import EditIcon from 'material-ui-icons/ViewList';

export default function EditCommentDialog(props) {
  return (
      <DialogWindow
        content={
          <CommentsEditForm
            setCandidateComment={props.setCandidateEditComment}
            candidateEditInfo={props.candidateEditInfo}
          />}
        label="Comments"
        openButtonType="icon"
        openButtonContent={<EditIcon />}
        open={function() {
        }}
        save={function() {
        }}
        close={function() {
        }}
      />
  );
}