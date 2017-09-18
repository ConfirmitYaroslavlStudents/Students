import React from 'react';
import AddIcon from 'material-ui-icons/Add';
import DialogWindow from '../materialUIDecorators/dialogWindow';
import AddCommentForm from './addCommentForm';
import EditCommentForm from './editCommentForm';
import ViewListIcon from 'material-ui-icons/ViewList';
import {CreateCandidate} from '../candidates';
import Badge from '../materialUIDecorators/badge';

export default function CommentControls(props) {
  return (
    <div>
      <DialogWindow
        content={
          <AddCommentForm
            commentIndex={props.candidate.comments ? props.candidate.comments.length : 0}
            setTempCandidateComment={props.setTempCandidateComment}
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
      />
      <Badge badgeContent={props.candidate.comments.length} badgeStyle="comment-badge">
        <DialogWindow
          content={
            <EditCommentForm
              candidate={props.candidate}
              setTempCandidateComment={props.setTempCandidateComment}
              editCandidate = {props.editCandidate}
            />}
          label="Comments"
          openButtonType="icon"
          openButtonContent={<ViewListIcon />}
          withoutAcceptButton={true}
        />
      </Badge>

    </div>
  );
}