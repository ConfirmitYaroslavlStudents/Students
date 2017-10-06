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
    this.state = ({newCommentText: ''});
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
        <AddCommentPanel
          value={this.state.newCommentText}
          onChange={this.changeNewCommentText}
          onClick={this.addNewComment}
        />
      </FormWrapper>
    );
  }

  changeNewCommentText(text) {
    this.setState({newCommentText: text})
  }

  addNewComment() {
    let newCommentText = this.state.newCommentText;
    if (newCommentText.replace(/<[^>]+>/g,'').trim() !== '') {
      if (newCommentText.slice(-11) === '<p><br></p>') {
        newCommentText = newCommentText.substr(0, newCommentText.length - 11);
      }
      this.props.addComment(this.props.candidate.id, new Comment(
        'Вы',
        moment().format('H:MM:SS DD MMMM YYYY'),
        newCommentText));
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

const NoComment = styled.div`
  padding: 5px;
  color: #aaa;
  text-align: center;
  margin-bottom: 20px;
`;