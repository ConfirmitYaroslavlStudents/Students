import React from 'react';
import DialogWindow from '../materialUIDecorators/dialogWindow';
import CommentsEditForm from './commentsEditForm';
import EditIcon from 'material-ui-icons/ViewList';

export default function CommentsEditDialog(props) {
  return (
      <DialogWindow
        content={
          <CommentsEditForm
            setCandidateComment={props.setCandidateEditComment}
            candidateEditInfo={props.candidateEditInfo}
          />}
        fullScreen={true}
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