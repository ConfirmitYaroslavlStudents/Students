import React from 'react';
import PropTypes from 'prop-types';
import AvatarIcon from 'material-ui-icons/AccountCircle';
import TextInput from '../materialUIDecorators/textInput';
import IconButton from '../materialUIDecorators/iconButton';
import AddIcon from 'material-ui-icons/Add';
import {Comment} from '../candidates';
import RemoveIcon from 'material-ui-icons/Delete';
import styled, { css } from 'styled-components';

export default class EditCommentForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = ({newCommentText: ''});
    this.changeNewCommentText = this.changeNewCommentText.bind(this);
    this.addNewComment = this.addNewComment.bind(this);
    this.deleteComment = this.deleteComment.bind(this);
  }

  render() {
    let deleteComment = this.deleteComment;
    const userName = 'вы';

    let comments = this.props.candidate.comments.map((comment, index) =>
      comment.author.toLowerCase() === userName ?
        <CommentWrapper key={'comment' + index}>
          <CommentIcons right>
            <AvatarIcon />
            <DeleteComment>
              <IconButton
                icon={<RemoveIcon/>}
                onClick={function () {deleteComment(index)}}
              />
            </DeleteComment>
          </CommentIcons>
          <CommentMount userComment>
            <div style={{wordWrap: 'break-word'}}>{comment.text}</div>
            <CommentFooter>{comment.date} <AuthorName right>{comment.author}</AuthorName></CommentFooter>
          </CommentMount>
        </CommentWrapper>
        :
        <CommentWrapper key={'comment' + index}>
          <CommentIcons>
            <AvatarIcon />
          </CommentIcons>
          <CommentMount>
            <div style={{wordWrap: 'break-word'}}>{comment.text}</div>
            <CommentFooter right><AuthorName>{comment.author}</AuthorName> {comment.date}</CommentFooter>
          </CommentMount>
        </CommentWrapper>
    );

    if (this.props.candidate.comments.length === 0) {
      comments = <NoComment><p>There is no any comment</p></NoComment>
    }

    return (
      <FormWrapper>

        {comments}

        <AddCommentWrapper>
          <CommentTextInput>
            <TextInput
              name="comment"
              placeholder="New comment"
              value={this.state.newCommentText}
              autoFocus={true}
              multiline={true}
              onChange={this.changeNewCommentText}
            />
          </CommentTextInput>
          <AddCommentButton>
            <IconButton
              icon={<AddIcon />}
              onClick = {this.addNewComment}
            />
          </AddCommentButton>
        </AddCommentWrapper>
      </FormWrapper>
    );
  }

  changeNewCommentText(text) {
    this.setState({newCommentText: text})
  }

  addNewComment() {
    if (this.state.newCommentText.trim() !== '') {
      let date = new Date();
      let candidate = this.props.candidate;
      candidate.comments.push(new Comment(
        'Вы',
        date.getHours() + ':' + date.getMinutes() + ' ' + date.getDate() + '.' + (date.getMonth() + 1) + '.' + date.getFullYear(),
        this.state.newCommentText));
      this.props.editCandidate(this.props.candidate.id, candidate);
      this.setState({newCommentText: ''})
    }
  }

  deleteComment(index) {
    let candidate = this.props.candidate;
    candidate.comments.splice(index, 1);
    this.props.editCandidate(this.props.candidate.id, candidate);
  }
}

EditCommentForm.propTypes = {
  candidate: PropTypes.object.isRequired,
  editCandidate: PropTypes.func.isRequired,
};

const FormWrapper = styled.div`
  display: 'inline-block';
  width: 500px;
  padding: 20px 0;
  background: #EEE;
`;

const CommentWrapper = styled.div`  
  margin: 0 0 10px;
  overflow: hidden;
  padding: 5px 0;
`;

const CommentIcons = styled.div`
  display: inline-block;
  width: 10%;
  float: left;
  margin: 0 -10px 0 8px;
  
  ${props => props.right && css`
		float: right;
		margin: 0 8x 0 -10px;
	`}
`;

const CommentMount = styled.div`
  display: inline-block;
  padding: 7px;
  box-shadow: 0 0 15px rgba(0, 0, 0, 0.2);
  max-width: 70%;
  float: left;
  background: #FFF;
  border-radius: 2px 7px 7px 7px;
  
  ${props => props.userComment && css`
		float: right;
		border-radius: 7px 2px 7px 7px;
	`}
`;

const CommentFooter = styled.div`  
  text-align: left;
  font-size: smaller;
  color: dimgray;
  text-align = left;
  
  ${props => props.right && css`
		text-align: right;
	`}
`;

const DeleteComment = styled.div`  
  margin-left: -12px;
`;

const AuthorName = styled.div`
  float: left;
  margin-right: 20px;
  
  ${props => props.right && css`
    float: right;
    margin-right: 0px;
    margin-left: 20px;
	`}
`;

const NoComment = styled.div`
  padding: 10px;
  color: dimgray;
  text-align: center;
`;

const AddCommentWrapper = styled.div`
  box-shadow: 0 0 15px rgba(0, 0, 0, 0.2);
  width: 100%;
  margin-top: 10px;
  margin-bottom: -20px;
  padding-bottom: 20px;
  clear: both;
  background: #FFF;
`;

const CommentTextInput = styled.div`  
  display: inline-block;
  width: 86%;
  margin-left: 20px;
`;

const AddCommentButton = styled.div`
  display: inline-block;
  width: 14%;
  position: absolute;
  bottom: 11px;
 `;