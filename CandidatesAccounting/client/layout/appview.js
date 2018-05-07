import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../applicationActions'
import { SELECTORS } from '../rootReducer'
import { Switch, Route } from 'react-router-dom'
import Navbar from '../common/UIComponentDecorators/navbar'
import Appbar from './appbar'
import TablesBar from './tablesbar'
import CandidatesTable from '../candidates/components/common/candidatesTable'
import CommentPage from '../comments/components/commentsPage'
import SnackBar from '../common/UIComponentDecorators/snackbar'
import ErrorPage from './errorPage'
import Spinner from '../common/UIComponentDecorators/spinner'
import styled from 'styled-components'
class AppView extends Component {
  render() {
    const {
      initializing,
      fetching,
      candidates,
      errorMessage,
      setErrorMessage,
      currentCandidateId
    } = this.props

    const handleSnackbarClose = () => {
      setErrorMessage({message: ''})
    }

    const tableSwitch =
      initializing ?
        <InitSpinnerWrapper >
          <Spinner size={60}/>
        </InitSpinnerWrapper>
        :
        <Switch>
          <Route exact path='/(interviewees|students|trainees)/(\w+)/comments' render={() =>
            <CommentPage candidate={candidates[currentCandidateId]} />
          }
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
          <Route psath='' render={() => <ErrorPage errorCode={404} errorMessage='Page not found'/>}/>
        </Switch>

    const refreshingSpinner =
      fetching && !initializing ?
        <RefreshSpinnerWrapper>
          <Spinner size={60}/>
        </RefreshSpinnerWrapper>
        : ''

    return (
      <div>
        <Navbar>
          <Appbar />
          <TablesBar />
        </Navbar>
        <MainWrapper>
          { tableSwitch }
          { refreshingSpinner }
        </MainWrapper>
        <SnackBar message={errorMessage} onClose={handleSnackbarClose} />
      </div>
    )
  }
}

AppView.propTypes = {
  initializing: PropTypes.bool.isRequired,
  fetching: PropTypes.bool.isRequired,
  candidates: PropTypes.object.isRequired,
  errorMessage: PropTypes.string.isRequired,
  setErrorMessage: PropTypes.func.isRequired,
  currentCandidateId: PropTypes.string.isRequired
}

export default connect(state => ({
    initializing: SELECTORS.APPLICATION.INITIALIZING(state),
    fetching: SELECTORS.APPLICATION.FETCHING(state),
    candidates: SELECTORS.CANDIDATES.CANDIDATES(state),
    errorMessage: SELECTORS.APPLICATION.ERRORMESSAGE(state),
    currentCandidateId: SELECTORS.COMMENTS.CURRENTCANDIDATEID(state)
  }
), actions)(AppView)

const InitSpinnerWrapper = styled.div`
  position: fixed;
  z-index: 100;
  top: 48%;
  width: 100%;
  text-align: center;
`

const RefreshSpinnerWrapper = styled.div`
  position: absolute;
  z-index: 100;
  top: 38%;
  width: 100%;
  text-align: center;
`

const MainWrapper = styled.div`
  position: relative;
  margin-top: 108px;
`