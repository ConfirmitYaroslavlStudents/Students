import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../../actions/actions'
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

  UNSAFE_componentWillMount() {
    const { candidate } = this.props

    this.userColors = {}
    Object.keys(this.props.candidate.comments).forEach((commentID) => {
      const comment = candidate.comments[commentID]
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

  deleteComment = (commentId) => () => {
    this.props.deleteComment({ candidateId: this.props.candidate.id, commentId })
  }

  render() {
    const { initializing, fetching, authorized, candidate, username, addComment, subscribe, unsubscribe } = this.props
    const comments = Object.keys(candidate.comments).map(commentId => candidate.comments[commentId])

    if (initializing || fetching) {
      return <SpinnerWrapper><Spinner size={60}/></SpinnerWrapper>
    }

    if (!candidate) {
      return (
        <FormWrapper>
          <NoResultWrapper>Candidate not found</NoResultWrapper>
        </FormWrapper>
      )
    }

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

    return (
      <CommentPageWrapper>
        <Grid container spacing={0} justify='center'>
          <Grid item className='comment-grid' lg={6} md={9} sm={12}>
            {commentClouds}
          </Grid>
        </Grid>
        <CommentPageFooter>
          <LoadableAddCommentPanel
            candidate={candidate}
            username={username}
            addComment={addComment}
            subscribe={subscribe}
            unsubscribe={unsubscribe}
            onCommentAdd={this.handleNewCommentAdd}
            disabled={!authorized}
          />
        </CommentPageFooter>
      </CommentPageWrapper>
    )
  }
}

CommentsPage.propTypes = {
  candidate: PropTypes.object
}

export default connect(state => {
  return {
    initializing: state.initializing,
    fetching: state.fetching,
    authorized: state.authorized,
    username: state.username
  }
}, actions)(CommentsPage)

const SpinnerWrapper = styled.div`
  position: fixed;
  z-index: 100;
  top: 48%;
  width: 100%;
  text-align: center;
`

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
  padding-top: 110px;
  padding-bottom: 161px;
  box-sizing: border-box;
`

const NoResultWrapper = styled.div`
  padding: 5px;
  color: #bbb;
  text-align: center;
  margin-bottom: 20px;
`