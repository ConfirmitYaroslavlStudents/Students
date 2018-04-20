import React, { Component } from 'react'
import { MainWrapper } from '../common/styledComponents'
import { connect } from 'react-redux'
import { Switch, Route } from 'react-router-dom'
import * as actions from '../../actions/actions'
import Navbar from './navbar'
import CandidatesTable from './candidatesTable'
import CommentPage from '../comments/commentsPage'
import SnackBar from '../common/UIComponentDecorators/snackbar'
import ErrorPage from './errorPage'
import Spinner from '../common/UIComponentDecorators/spinner'
import styled from 'styled-components'

export default class AppView extends Component {
  render() {
    const extractCandidateId = (url) => {
      return this.props.candidates[url.split('/')[2]]
    }

    const tableSwitch =
      this.props.initializing || this.props.fetching ?  //TODO: blur old table instead of removing it
        <SpinnerWrapper>
          <Spinner size={60}/>
        </SpinnerWrapper>
        :
        <Switch>
          <Route exact path='/(interviewees|students|trainees)/(\w+)/comments' render={() =>
            <CommentPage candidate={extractCandidateId(this.props.history.location.pathname)} />}
          />
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
      this.props.fetching ?
        <SpinnerWrapper>
          <Spinner size={60}/>
        </SpinnerWrapper>
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
    ...state
  }
}

const SpinnerWrapper = styled.div`
  position: fixed;
  z-index: 100;
  top: 48%;
  width: 100%;
  text-align: center;
`

module.exports = connect(mapStateToProps, actions)(AppView)