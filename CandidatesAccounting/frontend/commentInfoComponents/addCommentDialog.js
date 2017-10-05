import React from 'react';
import PropTypes from 'prop-types';
import DialogWindow from '../materialUIDecorators/dialogWindow';
import CommentIcon from 'material-ui-icons/InsertComment';
import CloseIcon from 'material-ui-icons/Close';
import { Comment } from '../candidatesClasses/index';
import moment from 'moment';
import IconButton from '../materialUIDecorators/iconButton';
import AddCommentPanel from './addCommentPanel';
import styled from 'styled-components';

export default class AddCommentDialog extends React.Component {
  constructor(props) {
    super(props);
    this.state = ({isOpen: false, commentText: ''});
    this.handleOpenClose = this.handleOpenClose.bind(this);
    this.addNewComment = this.addNewComment.bind(this);
  }

  render() {
    const handleOpenClose = this.handleOpenClose;
    return (
      <DialogWindow
        open={this.state.isOpen}
        content={
          <FormWrapper>
            <AddCommentPanel
              value={this.state.commentText}
              onChange={this.changeCommentText.bind(this)}
              onClick={this.addNewComment}
            />
          </FormWrapper>}
        label="Add new comment"
        openButton={
          <IconButton icon={<CommentIcon />} onClick={() => {handleOpenClose(true)}}/>
        }
        controls={
          <div style={{display: 'inline-block'}}>
            <IconButton color="inherit" icon={<CloseIcon />} onClick={() => {handleOpenClose(false)}}/>
          </div>
        }
      />
    );
  }

  changeCommentText(text) {
    this.setState({commentText: text});
  }

  handleOpenClose(isOpen) {
    this.setState({isOpen: isOpen});
    this.changeCommentText('');
  }

  addNewComment() {
    let commentText = this.state.commentText;
    if (commentText.replace(/<[^>]+>/g,'').trim() !== '') {
      if (commentText.slice(-11) === '<p><br></p>') {
        commentText = commentText.substr(0, commentText.length-11);
      }
      this.props.addComment(this.props.candidate.id, new Comment(
        'Вы',
        moment().format('H:MM:SS DD MMMM YYYY'),
        commentText));
      this.handleOpenClose(false);
    }
  }
}

AddCommentDialog.propTypes = {
  candidate: PropTypes.object.isRequired,
  addComment: PropTypes.func.isRequired,
};

const FormWrapper = styled.div`
  width: 500px;
`;