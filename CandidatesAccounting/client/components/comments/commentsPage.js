import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { CommentPageWrapper, NoResultWrapper, CommentPageFooter } from '../common/styledComponents'
import Comment from './comment'
import CurrentUserComment from './currentUserComment'
import SystemComment from './systemComment'
import LoadableAddCommentPanel from './loadableAddCommentPanel'
import getRandomColor from '../../utilities/getRandomColor'
import Grid from 'material-ui/Grid'
import Spinner from '../common/UIComponentDecorators/spinner'

export default class CommentsPage extends Component {
  constructor(props) {
    super(props)
    this.shouldScrollDown = false
    this.userColors = {}
  }

  UNSAFE_componentWillMount() {
    this.props.setState(
      {
        pageTitle: this.props.candidate.name,
        candidateStatus: this.props.candidate.status
      }
    )
    this.userColors = {}
    Object.keys(this.props.candidate.comments).forEach((commentID) => {
      const comment = this.props.candidate.comments[commentID]
      if (!(comment.author in this.userColors)) {
        this.userColors[comment.author] = getRandomColor()
      }
    })
  }

  componentDidMount() {
    window.scrollTo(0, document.documentElement.scrollHeight)
  }

  componentDidUpdate() {
    if (this.shouldScrollDown) {
      window.scrollTo(0, document.documentElement.scrollHeight)
      this.shouldScrollDown = false
    }
  }

  handleNewCommentAdd = () => {
    this.shouldScrollDown = true
  }

  deleteComment = (commentID) => {
    this.props.deleteComment(this.props.candidate.id, commentID)
  }

  render() {
    if (this.props.applicationStatus !== 'ok') {
      return <Spinner size='big'/>
    }

    if (!this.props.candidate) {
      return (
        <FormWrapper>
          <NoResultWrapper>Candidate not found</NoResultWrapper>
        </FormWrapper>
      )
    }

    let comments = Object.keys(this.props.candidate.comments).map(commentId => {
      const comment = this.props.candidate.comments[commentId]
      switch (comment.author) {
        case 'SYSTEM':
          return <SystemComment key={commentId} comment={comment}/>
        case this.props.username:
          return <CurrentUserComment key={commentId}  comment={comment} candidate={this.props.candidate} deleteComment={() => {this.deleteComment(commentId)}}/>
        default:
          return <Comment key={commentId} comment={comment} candidate={this.props.candidate} markerColor={this.userColors[comment.author]}/>
      }
    })

    if (comments.length === 0) {
      comments = <NoResultWrapper>No comments</NoResultWrapper>
    }

    return (
      <CommentPageWrapper>
        <Grid container spacing={0} justify='center'>
          <Grid item className='comment-grid' lg={6} md={9} sm={12}>
            {comments}
          </Grid>
        </Grid>
        <CommentPageFooter>
          <LoadableAddCommentPanel
            addComment={this.props.addComment}
            candidate={this.props.candidate}
            onClick={this.handleNewCommentAdd}
            username={this.props.username}
            subscribe={this.props.subscribe}
            unsubscribe={this.props.unsubscribe}
            disabled={this.props.username === ''}
          />
        </CommentPageFooter>
      </CommentPageWrapper>
    )
  }
}

CommentsPage.propTypes = {
  candidate: PropTypes.object,
  applicationStatus: PropTypes.string.isRequired,
  addComment: PropTypes.func.isRequired,
  deleteComment: PropTypes.func.isRequired,
  username: PropTypes.string.isRequired,
  subscribe: PropTypes.func.isRequired,
  unsubscribe: PropTypes.func.isRequired,
  setState: PropTypes.func.isRequired
}