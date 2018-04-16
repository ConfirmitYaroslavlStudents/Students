import React, { Component } from 'react'
import { MainWrapper } from '../common/styledComponents'
import { connect } from 'react-redux'
import { Switch, Route } from 'react-router-dom'
import actions from '../../redux/actions'
import Navbar from './navbar'
import CandidatesTable from './candidatesTable'
import CommentPage from '../comments/commentsPage'
import SnackBar from '../common/UIComponentDecorators/snackbar'
import ErrorPage from './errorPage'
import Spinner from '../common/UIComponentDecorators/spinner'

export default class AppView extends Component {
  render() {
    const tableSwitch =
      this.props.applicationStatus === 'loading' ?
        <Spinner size='big'/>
        :
        <Switch>
          <Route exact path='/(interviewees|students|trainees)/(\w+)/comments' render={() =>
            <CommentPage
              candidate={this.props.candidates[this.props.history.location.pathname.split('/')[2]]}
              applicationStatus={this.props.applicationStatus}
              addComment={this.props.addComment}
              deleteComment={this.props.deleteComment}
              username={this.props.username}
              subscribe={this.props.subscribe}
              unsubscribe={this.props.unsubscribe}
              setState={this.props.setState}/>
          }/>

          <Route exact path='/interviewees*' render={() =>
            <CandidatesTable type='Interviewee' {...this.props} />}
          />
          <Route exact path='/students*' render={() =>
            <CandidatesTable type='Student' {...this.props} />}
          />
          <Route exact path='/trainees*' render={() =>
            <CandidatesTable type='Trainee' {...this.props} />}
          />
          <Route exact path='/*' render={() =>
            <CandidatesTable type='Candidate' {...this.props} />}
          />
          <Route path='' render={() => <ErrorPage errorCode={404} errorMessage='Page not found'/>}/>
        </Switch>

    const refreshingSpinner =
      this.props.applicationStatus === 'refreshing' ?
        <Spinner size="big"/>
        : ''

    return (
      <div>
        <Navbar {...this.props}/>
        <MainWrapper>
          { tableSwitch }
        </MainWrapper>
        { refreshingSpinner }
        <SnackBar message={this.props.errorMessage} setErrorMessage={this.props.setErrorMessage}/>
      </div>
    )
  }
}

function mapStateToProps(state) {
  return {
    applicationStatus: state.applicationStatus,
    authorized: state.authorized,
    username: state.username,
    pageTitle: state.pageTitle,
    errorMessage: state.errorMessage,
    searchRequest: state.searchRequest,
    candidateStatus: state.candidateStatus,
    offset: Number(state.offset),
    candidatesPerPage: Number(state.candidatesPerPage),
    totalCount: Number(state.totalCount),
    sortingField: state.sortingField,
    sortingDirection: state.sortingDirection,
    candidates: state.candidates,
    tags: state.tags,
    notifications: state.notifications,
  }
}

module.exports = connect(mapStateToProps, actions)(AppView)