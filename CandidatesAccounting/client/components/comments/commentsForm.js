import React from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import CommentCloud from './commentCloud';
import AddCommentPanel from './addCommentPanel';

export default class CommentsForm extends React.Component {
  constructor(props) {
    super(props);
    this.shouldScrollDown = false;
    this.handleNewCommentAdd = this.handleNewCommentAdd.bind(this);
    this.deleteComment = this.deleteComment.bind(this);
  }

  componentWillMount() {
    if (this.props.pageTitle === 'Candidate Accounting') {
      this.props.setSearchRequest('', this.props.history, 0);
      this.props.setPageTitle(this.props.candidate.name);
    }
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

  deleteComment(comment) {
    this.props.deleteComment(this.props.candidate.id, comment);
  }

  render() {
    if (this.props.candidate) {
      let comments = this.props.candidate.comments.map((comment, index) =>
        <CommentCloud
          key={index}
          comment={comment}
          userName={this.props.userName}
          commentIndex={index}
          highlighted={
            this.props.searchRequest ?
              comment.author.toLowerCase().includes(this.props.searchRequest.toLowerCase()) ||
              comment.text.toLowerCase().includes(this.props.searchRequest.toLowerCase())
              : false}
          deleteComment={() => {this.deleteComment(comment)}}
        />
      );
      if (this.props.candidate.comments.length === 0) {
        comments = <NoResultWrapper><p>No comments</p></NoResultWrapper>
      }

      return (
        <FormWrapper>
          {comments}
          <AddCommentPanelWrapper>
            <AddCommentPanel
              addComment={this.props.addComment}
              candidateID={this.props.candidate.id}
              onClick={this.handleNewCommentAdd}
              userName={this.props.userName}
            />
          </AddCommentPanelWrapper>
        </FormWrapper>
      );
    } else {
      return (
        <FormWrapper>
          <NoResultWrapper><p>Candidate not found</p></NoResultWrapper>
        </FormWrapper>
      );
    }
  }
}

CommentsForm.propTypes = {
  addComment: PropTypes.func.isRequired,
  deleteComment: PropTypes.func.isRequired,
  userName: PropTypes.string.isRequired,
  pageTitle: PropTypes.string.isRequired,
  setPageTitle: PropTypes.func.isRequired,
  history: PropTypes.object.isRequired,
  searchRequest: PropTypes.string.isRequired,
  setSearchRequest: PropTypes.func.isRequired,
  candidate: PropTypes.object,
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

const NoResultWrapper = styled.div`
  padding: 5px;
  color: #bbb;
  text-align: center;
  margin-bottom: 20px;
`;

const AddCommentPanelWrapper = styled.div`
  position: fixed;
  bottom: 0;
  width: 100%;
`;