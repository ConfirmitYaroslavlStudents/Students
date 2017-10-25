import React from 'react';
import PropTypes from 'prop-types';
import {Comment} from '../candidatesClasses/index';
import styled from 'styled-components';
import moment from 'moment';
import CommentCloud from './commentCloud';
import AddCommentPanel from './addCommentPanel';

export default class CommentsForm extends React.Component {
  constructor(props) {
    super(props);
    this.newCommentText = '';
    this.shouldScrollDown = false;
    this.changeNewCommentText = this.changeNewCommentText.bind(this);
    this.addNewComment = this.addNewComment.bind(this);
    this.deleteComment = this.deleteComment.bind(this);
  }

  render() {
    let deleteComment = this.deleteComment;

    let comments = this.props.candidate.comments.map((comment, index) =>
      <CommentCloud key={index} comment={comment} userName="Вы" commentIndex={index} deleteComment={deleteComment}/>);

    if (this.props.candidate.comments.length === 0) {
      comments = <NoComment><p>No comments</p></NoComment>
    }

    return (
      <FormWrapper>
        {comments}
        <AddCommentPanelWrapper>
          <AddCommentPanel
            value={this.newCommentText}
            onChange={this.changeNewCommentText}
            onClick={this.addNewComment}
          />
        </AddCommentPanelWrapper>
      </FormWrapper>
    );
  }

  changeNewCommentText(text) {
    this.newCommentText = text;
  }

  componentDidMount() {
    window.scrollTo(0, document.documentElement.scrollHeight);
  }

  componentDidUpdate() {
    if (this.shouldScrollDown) {
      window.scrollTo(0, document.documentElement.scrollHeight);
      this.shouldScrollDown = false;
    }
  }

  addNewComment() {
    let newCommentText = this.newCommentText;
    if (newCommentText.replace(/<[^>]+>/g,'').trim() !== '') {
      if (newCommentText.slice(-11) === '<p><br></p>') {
        newCommentText = newCommentText.substr(0, newCommentText.length - 11);
      }
      this.props.addComment(this.props.candidate.id, new Comment(
        'Вы',
        moment().format('H:MM:SS DD MMMM YYYY'),
        newCommentText));
      this.newCommentText = '';
      this.shouldScrollDown = true;
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
  width: 100%;
  min-height: 100vmin;
  background: #EEE;
  position: absolute;
  top: 0;
  padding-top: 134px;
  padding-bottom: 161px;
  box-sizing: border-box;
`;

const NoComment = styled.div`
  padding: 5px;
  color: #aaa;
  text-align: center;
  margin-bottom: 20px;
`;

const AddCommentPanelWrapper = styled.div`
  position: fixed;
  bottom: 0;
  width: 100%;
`;