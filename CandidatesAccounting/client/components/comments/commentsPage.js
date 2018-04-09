import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { CommentPageWrapper, NoResultWrapper, CommentPageFooter } from '../common/styledComponents'
import CommentCloud from './commentCloud'
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

    let commentClouds = Object.keys(this.props.candidate.comments).map(commentId => {
      const comment = this.props.candidate.comments[commentId]
      return (
        <CommentCloud
          key={commentId}
          comment={comment}
          candidate={this.props.candidate}
          markerColor={this.userColors[comment.author]}
          deleteComment={() => {this.deleteComment(comment.id)}}
          isSystem={comment.author === 'SYSTEM'}
          isCurrentUserComment={comment.author === this.props.username}
        />)
    })

    if (Object.keys(this.props.candidate.comments).length === 0) {
      commentClouds = <NoResultWrapper>No comments</NoResultWrapper>
    }

    return (
      <CommentPageWrapper>
        <Grid container spacing={0} justify='center'>
          <Grid item className='comment-grid' lg={6} md={9} sm={12}>
            {commentClouds}
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