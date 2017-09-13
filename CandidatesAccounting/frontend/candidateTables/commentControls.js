import React from 'react';
import CommentsEditDialog from './editCommentDialog';
import AddCommentDialog from './addCommentDialog';

export default function AddCommentForm(props) {
  return (
    <div>
      <AddCommentDialog candidate={props.candidate} setCandidateEditComment={props.setCandidateEditComment}/>
      <CommentsEditDialog/>
      {props.candidate.comments ? props.candidate.comments.length : ''}
    </div>
  );
}