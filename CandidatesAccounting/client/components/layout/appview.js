import React, { Component } from 'react'
import { connect } from 'react-redux'
import { Switch, Route } from 'react-router-dom'
import * as applicationActions from '../../actions/applicationActions'
import Navbar from '../common/UIComponentDecorators/navbar'
import Appbar from './appbar'
import TablesBar from './tablesbar'
import CandidatesTable from './candidatesTable'
import CommentPage from '../comments/commentsPage'
import SnackBar from '../common/UIComponentDecorators/snackbar'
import ErrorPage from './errorPage'
import Spinner from '../common/UIComponentDecorators/spinner'
import styled from 'styled-components'

export default class AppView extends Component {
  render() {
    const { initializing, fetching, candidates, errorMessage, setErrorMessage, history } = this.props

    const extractCandidateId = (url) => {
      return url.split('/')[2]
    }

    const handleSnackbarClose = () => {
      setErrorMessage({message: ''})
    }

    const tableSwitch =
      initializing ?
        <SpinnerWrapper>
          <Spinner size={60}/>
        </SpinnerWrapper>
        :
        <Switch>
          <Route exact path='/(interviewees|students|trainees)/(\w+)/comments' render={() =>
            <CommentPage candidate={candidates[extractCandidateId(history.location.pathname)]} />}
          />
          <Route exact path='/interviewees*' render={() =>
            <CandidatesTable type='Interviewee' />}
          />
          <Route exact path='/students*' render={() =>
            <CandidatesTable type='Student' />}
          />
          <Route exact path='/trainees*' render={() =>
            <CandidatesTable type='Trainee' />}
          />
          <Route exact path='/*' render={() =>
            <CandidatesTable type='Candidate' />}
          />
          <Route path='' render={() => <ErrorPage errorCode={404} errorMessage='Page not found'/>}/>
        </Switch>

    const refreshingSpinner =
      fetching ?
        <SpinnerWrapper>
          <Spinner size={60}/>
        </SpinnerWrapper>
        : ''

    return (
      <div>
        <Navbar>
          <Appbar />
          <TablesBar />
        </Navbar>
        <MainWrapper>
          { tableSwitch }
        </MainWrapper>
        { refreshingSpinner }
        <SnackBar message={errorMessage} onClose={handleSnackbarClose} />
      </div>
    )
  }
}

function mapStateToProps(state) {
  return {
    initializing: state.initializing,
    fetching: state.fetching,
    candidates: state.candidates,
    errorMessage: state.errorMessage
  }
}

const SpinnerWrapper = styled.div`
  position: fixed;
  z-index: 100;
  top: 48%;
  width: 100%;
  text-align: center;
`

const MainWrapper = styled.div`
  margin-top: 108px;
`

module.exports = connect(mapStateToProps, applicationActions)(AppView)