import React from 'react';
import DialogWindow from '../materialUIDecorators/dialogWindow';
import AddIcon from 'material-ui-icons/Add';
import AddCommentForm from './addCommentForm';

export default function AddCommentDialog(props) {
  return (
    <DialogWindow
      content={
        <AddCommentForm candidate={props.candidate} setCandidateEditComment={props.setCandidateEditComment}/>
        }
      label="Add new comment"
      openButtonType="icon"
      openButtonContent={<AddIcon />}
      open={function() {
      }}
      save={function() {
      }}
      close={function() {
      }}
    />
  );
}