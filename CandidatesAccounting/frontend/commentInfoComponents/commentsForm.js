import React from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import CommentCloud from './commentCloud';
import AddCommentPanel from './addCommentPanel';

export default class CommentsForm extends React.Component {
  constructor(props) {
    super(props);
    this.shouldScrollDown = false;
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

  handleNewCommentAdd() {
    this.shouldScrollDown = true;
  }

  deleteComment(index) {
    this.props.deleteComment(this.props.candidate.id, index);
  }

  render() {
    let comments = this.props.candidate.comments.map((comment, index) =>
      <CommentCloud key={index}
                    comment={comment}
                    userName="Вы"
                    commentIndex={index}
                    deleteComment={this.deleteComment.bind(this)}
      />
    );
    if (this.props.candidate.comments.length === 0) {
      comments = <NoComment><p>No comments</p></NoComment>
    }

    return (
      <FormWrapper>
        {comments}
        <AddCommentPanelWrapper>
          <AddCommentPanel
            addComment={this.props.addComment}
            candidateId={this.props.candidate.id}
            onClick={this.handleNewCommentAdd.bind(this)}
          />
        </AddCommentPanelWrapper>
      </FormWrapper>
    );
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