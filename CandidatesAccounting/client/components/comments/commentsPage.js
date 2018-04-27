import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as commentActions from '../../actions/commentActions'
import * as notificationActions from '../../actions/notificationActions'
import Comment from './comment'
import CurrentUserComment from './currentUserComment'
import SystemComment from './systemComment'
import LoadableAddCommentPanel from './loadableAddCommentPanel'
import getRandomColor from '../../utilities/getRandomColor'
import Grid from 'material-ui/Grid'
import Spinner from '../common/UIComponentDecorators/spinner'
import styled from 'styled-components'

class CommentsPage extends Component {
  constructor(props) {
    super(props)
    this.shouldScrollDown = false
    this.userColors = {}
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

  deleteComment = (commentId) => () => {
    this.props.deleteComment({ candidateId: this.props.candidate.id, commentId })
  }

  render() {
    const { initializing, fetching, authorized, candidate, comments, username, addComment, subscribe, unsubscribe } = this.props

    if (initializing) {
      return <SpinnerWrapper><Spinner size={60}/></SpinnerWrapper>
    }

    if (!candidate) {
      return (
        <FormWrapper>
          <NoResultWrapper>Candidate not found</NoResultWrapper>
        </FormWrapper>
      )
    }

    comments.forEach(comment => {
      if (!(comment.author in this.userColors)) {
        this.userColors[comment.author] = getRandomColor()
      }
    })

    let commentClouds = comments.map((comment, index) => {
      switch (comment.author) {
        case 'SYSTEM':
          return <SystemComment key={'comment-' + index} comment={comment}/>
        case username:
          return <CurrentUserComment key={'comment-' + index} comment={comment} candidate={candidate} deleteComment={this.deleteComment(comment.id)}/>
        default:
          return <Comment key={'comment-' + index} comment={comment} candidate={candidate} markerColor={this.userColors[comment.author]}/>
      }
    })

    if (comments.length === 0) {
      commentClouds = <NoResultWrapper>No comments</NoResultWrapper>
    }

    const fethcingSpinner =
      fetching && !initializing ?
        <FetchingWrapper>
          <SpinnerWrapper><Spinner size={60}/></SpinnerWrapper>
        </FetchingWrapper>
        : ''

    return (
      <CommentPageWrapper>
        <Grid container spacing={0} justify='center'>
          <Grid item className='comment-grid' lg={6} md={9} sm={12}>
            {commentClouds}
          </Grid>
        </Grid>
        <CommentPageFooter>
          <LoadableAddCommentPanel
            username={username}
            candidate={candidate}
            addComment={addComment}
            subscribe={subscribe}
            unsubscribe={unsubscribe}
            onCommentAdd={this.handleNewCommentAdd}
            disabled={!authorized}
          />
        </CommentPageFooter>
        {fethcingSpinner}
      </CommentPageWrapper>
    )
  }
}

CommentsPage.propTypes = {
  candidate: PropTypes.object
}

export default connect(state => {
  return {
    comments: Object.keys(state.comments.comments).map(commentId => state.comments.comments[commentId]),
    initializing: state.application.initializing,
    fetching: state.application.fetching,
    authorized: state.authorization.authorized,
    username: state.authorization.username
  }
}, {...commentActions, ...notificationActions})(CommentsPage)


const CommentPageFooter = styled.div`
  position: fixed;
  bottom: 0;
  width: 100%;
`

const CommentPageWrapper = styled.div`
  display: flex;
  width: 100%;
  min-height: 100vmin;
  background: #EEE;
  position: absolute;
  top: 0;
  padding-bottom: 161px;
  box-sizing: border-box;
`

const NoResultWrapper = styled.div`
  padding: 5px;
  color: #bbb;
  text-align: center;
  margin-bottom: 20px;
`

const FetchingWrapper = styled.div`
  position: fixed;
  z-index: 100;
  height: 100%;
  width: 100%;
  top: 0;
  left: 0;
  background-color: rgb(255, 255, 255, 0.5);
`

const SpinnerWrapper = styled.div`
  position: fixed;
  z-index: 100;
  top: 48%;
  width: 100%;
  text-align: center;
`