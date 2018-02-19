import React, {Component} from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import CommentCloud from './commentCloud';
import AddCommentPanel from './addCommentPanel';
import getRandomColor from '../../utilities/getRandomColor';
import Grid from 'material-ui/Grid';

export default class CommentsForm extends Component {
  constructor(props) {
    super(props);
    this.shouldScrollDown = false;
    this.userColors = {};
    this.handleNewCommentAdd = this.handleNewCommentAdd.bind(this);
    this.deleteComment = this.deleteComment.bind(this);
  }

  componentWillMount() {
    if (this.props.pageTitle === 'Candidate Accounting') {
      this.props.setSearchRequest('', this.props.history, 0);
      this.props.setPageTitle(this.props.candidate.name);
    }
    this.userColors = {};
    this.props.candidate.comments.forEach((comment) => {
      if (!(comment.author in this.userColors)) {
        this.userColors[comment.author] = getRandomColor();
      }
    });
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

  deleteComment(commentID) {
    this.props.deleteComment(this.props.candidate.id, commentID);
  }

  render() {
    if (this.props.candidate) {
      let comments = this.props.candidate.comments.map((comment, index) =>
        <CommentCloud
          key={index}
          comment={comment}
          markerColor={this.userColors[comment.author]}
          deleteComment={() => {this.deleteComment(comment.id)}}
          isSystem={comment.author === 'SYSTEM'}
          isCurrentUserComment={comment.author === this.props.username}
          highlighted={
            this.props.searchRequest ?
              comment.author.toLowerCase().includes(this.props.searchRequest.toLowerCase()) ||
              comment.text.toLowerCase().includes(this.props.searchRequest.toLowerCase())
              : false}
        />
      );
      if (this.props.candidate.comments.length === 0) {
        comments = <NoResultWrapper><p>No comments</p></NoResultWrapper>
      }

      return (
        <FormWrapper>
          <Grid container spacing={0} justify="center">
            <Grid item className="comment-grid" lg={6} md={9} sm={12}>
              {comments}
            </Grid>
          </Grid>
          <AddCommentPanelWrapper>
            <AddCommentPanel
              addComment={this.props.addComment}
              candidate={this.props.candidate}
              onClick={this.handleNewCommentAdd}
              username={this.props.username}
              subscribe={this.props.subscribe}
              unsubscribe={this.props.unsubscribe}
              disabled={this.props.username === ''}
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
  candidate: PropTypes.object.isRequired,
  addComment: PropTypes.func.isRequired,
  deleteComment: PropTypes.func.isRequired,
  username: PropTypes.string.isRequired,
  subscribe: PropTypes.func.isRequired,
  unsubscribe: PropTypes.func.isRequired,
  pageTitle: PropTypes.string.isRequired,
  setPageTitle: PropTypes.func.isRequired,
  history: PropTypes.object.isRequired,
  searchRequest: PropTypes.string.isRequired,
  setSearchRequest: PropTypes.func.isRequired
};

const FormWrapper = styled.div`
  display: flex;
  width: 100%;
  min-height: 100vmin;
  background: #EEE;
  position: absolute;
  top: 0;
  padding-top: 112px;
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