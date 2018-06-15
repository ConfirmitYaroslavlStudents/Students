import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../actions'
import * as notificationActions from '../../notifications/actions'
import { SELECTORS } from '../../rootReducer'
import Comment from './comment'
import DeletedComment from './deletedComment'
import CurrentUserComment from './currentUserComment'
import SystemComment from './systemComment'
import LoadableAddCommentPanel from './loadableAdditionPanel'
import sortComments from '../../utilities/sortComments'
import getRandomColor from '../../utilities/getRandomColor'
import Grid from '@material-ui/core/Grid'
import Spinner from '../../commonComponents/UIComponentDecorators/spinner'
import CandidateCard from '../../candidates/components/common/card'
import styled from 'styled-components'
import IconButton from '../../commonComponents/UIComponentDecorators/iconButton'
import ArrowDownIcon from '@material-ui/icons/ArrowDownward'
import { BigButtonStyle, MediumIconStyle } from '../../commonComponents/styleObjects'

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
      this.scrollToEnd()
    }
  }

  handleNewCommentAdd = () => {
    this.shouldScrollDown = true
  }

  deleteComment = (commentId) => () => {
    this.props.deleteComment({ candidateId: this.props.candidate.id, commentId })
  }

  restoreComment = (comment) => () => {
    delete comment.isDeleted
    this.props.restoreComment({ candidateId: this.props.candidate.id, comment })
  }

  scrollToEnd = () => {
    window.scrollTo(0, document.documentElement.scrollHeight)
    this.shouldScrollDown = false
  }

  render() {
    const { initializing, fetching, authorized, candidate, comments, lastDeletedComment, tags, username, addComment, subscribe, unsubscribe } = this.props

    if (initializing) {
      return <SpinnerWrapper><Spinner size={60}/></SpinnerWrapper>
    }

    if (!candidate) {
      return (
        <CommentPageWrapper>
          <NoResultWrapper>Candidate not found</NoResultWrapper>
        </CommentPageWrapper>
      )
    }

    let commentArray = Object.keys(comments).map(commentId => comments[commentId])
    if (lastDeletedComment.author) {
      lastDeletedComment.isDeleted = true;
      commentArray.push(lastDeletedComment)
    }
    commentArray = sortComments(commentArray)
    commentArray.forEach(comment => {
      if (!(comment.author in this.userColors)) {
        this.userColors[comment.author] = getRandomColor()
      }
    })

    let commentClouds = commentArray.map((comment, index) => {
      switch (comment.author) {
        case 'SYSTEM':
          return <SystemComment key={'comment-' + index} comment={comment}/>
        case username:
          if (comment.isDeleted)
            return <DeletedComment key={'comment-' + index} comment={comment} candidate={candidate} restoreComment={this.restoreComment(comment)}/>
          else
            return <CurrentUserComment key={'comment-' + index} comment={comment} candidate={candidate} deleteComment={this.deleteComment(comment.id)}/>
        default:
          return <Comment key={'comment-' + index} comment={comment} candidate={candidate} markerColor={this.userColors[comment.author]}/>
      }
    })

    if (commentArray.length === 0) {
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
        <Grid container spacing={0} justify='flex-start'>
          <Grid item className='candidate-card-grid' lg={3} md={4} sm={4}>
            <CandidateCardWrapper>
              <CandidateCard candidate={candidate} tags={tags} authorized={authorized}/>
            </CandidateCardWrapper>
          </Grid>
          <Grid container spacing={0} justify='flex-start'>
            <Grid item lg={3} md={4} sm={4}>
            </Grid>
            <Grid item lg={6} md={7} sm={8}>
              <CommentsWrapper>
                {commentClouds}
              </CommentsWrapper>
            </Grid>
          </Grid>
        </Grid>
        <CommentPageFooter>
          <Grid container spacing={0} justify='flex-start'>
            <Grid item lg={3} md={4} sm={4}>
            </Grid>
            <Grid item lg={6} md={7} sm={8}>
              <AddCommentPanelWrapper>
                <LoadableAddCommentPanel
                  username={username}
                  candidate={candidate}
                  addComment={addComment}
                  subscribe={subscribe}
                  unsubscribe={unsubscribe}
                  onCommentAdd={this.handleNewCommentAdd}
                  disabled={!authorized}
                />
              </AddCommentPanelWrapper>
            </Grid>
          </Grid>
        </CommentPageFooter>
        {fethcingSpinner}
        <ToEndButtonWrapper>
          <IconButton icon={<ArrowDownIcon style={MediumIconStyle}/>} onClick={this.scrollToEnd} style={BigButtonStyle}/>
        </ToEndButtonWrapper>
        <GreyBackground />
      </CommentPageWrapper>
    )
  }
}

CommentsPage.propTypes = {
  comments: PropTypes.object.isRequired,
  lastDeletedComment: PropTypes.object.isRequired,
  initializing: PropTypes.bool.isRequired,
  fetching: PropTypes.bool.isRequired,
  authorized: PropTypes.bool.isRequired,
  username: PropTypes.string.isRequired,
  addComment: PropTypes.func.isRequired,
  deleteComment: PropTypes.func.isRequired,
  restoreComment: PropTypes.func.isRequired,
  subscribe:PropTypes.func.isRequired,
  unsubscribe: PropTypes.func.isRequired,
  tags: PropTypes.array.isRequired,
  candidate: PropTypes.object
}

export default connect(state => ({
    comments: SELECTORS.COMMENTS.COMMENTS(state),
    lastDeletedComment: SELECTORS.COMMENTS.LASTDELETEDCOMMENT(state),
    initializing: SELECTORS.APPLICATION.INITIALIZING(state),
    fetching: SELECTORS.APPLICATION.FETCHING(state),
    authorized: SELECTORS.AUTHORIZATION.AUTHORIZED(state),
    username: SELECTORS.AUTHORIZATION.USERNAME(state),
    tags: SELECTORS.TAGS.TAGS(state)
  }
), {...actions, ...notificationActions})(CommentsPage)

const CommentPageWrapper = styled.div`
  display: flex;
  width: 100%;
  min-height: 100vmin;
  margin-top: -156px;
  padding-top: 156px;
  padding-bottom: 224px;
  box-sizing: border-box;
  z-index: 4;
`

const CommentPageFooter = styled.div`
  position: fixed;
  bottom: 0;
  width: 100%;
`

const AddCommentPanelWrapper = styled.div`
    border-top: 1px solid #ccc;
`

const NoResultWrapper = styled.div`
  width: 100%;
  padding: 100px 5px;
  font-size: 200%;
  font-weight: bold;
  color: #aaa;
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
  box-shadow: 2px 2px 4px 2px rgba(0,0,0,0.1);
`

const CandidateCardWrapper = styled.div`
  background-color: #f6f6f6;
  padding: 24px;
  margin: 32px 16px 32px 32px;
  border-radius: 4px;
  box-shadow: 2px 2px 4px 2px rgba(0,0,0,0.1);
`

const CommentsWrapper = styled.div`
  height: 100%;
  background-color: #f6f6f6;
  padding: 24px 4px;
  margin: 0 1px 0 1px;
  box-shadow: 0 0 7px 5px rgba(0, 0, 0, 0.05);
  overflow: hidden;
  min-width: 500px;
`

const SpinnerWrapper = styled.div`
  position: fixed;
  z-index: 100;
  top: 48%;
  width: 100%;
  text-align: center;
`

const ToEndButtonWrapper = styled.div`
  position: fixed;
  right: 16px;
  bottom: 16px;
`

const GreyBackground = styled.div`
  position: fixed;
  left: 0;
  top: 0;
  width: 100%;
  height: 1920px;
  background-color: #eee;
  z-index: -1;
`