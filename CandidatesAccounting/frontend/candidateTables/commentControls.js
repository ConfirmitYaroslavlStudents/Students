import React from 'react';
import CommentsEditDialog from './commentsEditDialog';
import AddCommentDialog from './addCommentDialog';

export default function AddCommentForm(props) {
  return (
    <div>
      <AddCommentDialog candidate={props.candidate}/>
      <CommentsEditDialog/>
      {props.candidate.comments ? props.candidate.comments.length : ''}
    </div>
  );
}