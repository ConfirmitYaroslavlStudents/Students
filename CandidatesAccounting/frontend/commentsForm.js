import React from 'react';
import PropTypes from 'prop-types';
import AvatarIcon from 'material-ui-icons/AccountCircle';
import TextInput from './materialUIDecorators/textInput';
import IconButton from './materialUIDecorators/iconButton';
import AddIcon from 'material-ui-icons/Add';
import {Comment} from './candidates/index';
import RemoveIcon from 'material-ui-icons/Delete';
import styled, { css } from 'styled-components';
import moment from 'moment';

export default class CommentsForm extends React.Component {
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
            <CommentText>{comment.text.split(/[\n\r]/).map((line, index) =>
              (<span key={index}>{line}<br/></span>))}
            </CommentText>
            <CommentFooter>{comment.date} <AuthorName right>{comment.author}</AuthorName></CommentFooter>
          </CommentMount>
        </CommentWrapper>
        :
        <CommentWrapper key={'comment' + index}>
          <CommentIcons>
            <AvatarIcon />
          </CommentIcons>
          <CommentMount>
            <CommentText>{comment.text.split(['\n']).map((line, index) =>
              (<span key={index}>{line}<br/></span>))}
            </CommentText>
            <CommentFooter right><AuthorName>{comment.author}</AuthorName> {comment.date}</CommentFooter>
          </CommentMount>
        </CommentWrapper>
    );

    if (this.props.candidate.comments.length === 0) {
      comments = <NoComment><p>No comments</p></NoComment>
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
      this.props.addComment(this.props.candidate.id, new Comment(
        'Вы',
        moment().format('H:MM:SS DD MMMM YYYY'),
        this.state.newCommentText));
      this.setState({newCommentText: ''})
    }
  }

  deleteComment(index) {
    this.props.deleteComment(this.props.candidate.id, index);
  }
}

CommentsForm.propTypes = {
  candidate: PropTypes.object.isRequired,
  addComment: PropTypes.func.isRequired,
  deleteComment: PropTypes.func.isRequired,
};

const FormWrapper = styled.div`
  display: 'inline-block';
  width: 100%;
  padding-top: 20px;
  background: #EEE;
`;

const CommentWrapper = styled.div`  
  margin: 0 0 10px;
  overflow: hidden;
  padding: 5px 0;
`;

const CommentIcons = styled.div`
  display: inline-block;
  float: left;
  margin: 0 10px 0 10px;
    
  ${props => props.right && css`
		float: right;
		margin: 0 0 0 10px;
	`}
`;

const CommentMount = styled.div`
  display: inline-block;
  padding: 10px;
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

const CommentText = styled.div`
  word-wrap: break-word;
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
  position: relative;
  width: 100%;
  padding-bottom: 10px;
  clear: both;
  background: #FFF;
`;

const CommentTextInput = styled.div`
  padding-left: 20px;
  padding-right: 56px;
`;

const AddCommentButton = styled.div`
  display: inline-block;
  position: absolute;
  right: 10px;
  bottom: 10px;
 `;