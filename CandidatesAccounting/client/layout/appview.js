import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../applicationActions'
import { SELECTORS } from '../rootReducer'
import Switch from 'react-router-dom/Switch'
import Route from 'react-router-dom/Route'
import Navbar from '../commonComponents/UIComponentDecorators/navbar'
import Appbar from './appbar'
import TablesBar from './tablebar'
import CandidatesTable from '../candidates/components/common/table'
import CommentPage from '../comments/components/page'
import SnackBar from '../commonComponents/UIComponentDecorators/snackbar'
import ErrorPage from './errorPage'
import Spinner from '../commonComponents/UIComponentDecorators/spinner'
import styled from 'styled-components'

class AppView extends Component {
  render() {
    const {
      initializing,
      fetching,
      pageTitle,
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
          <Route exact path='/interviewees*' render={props =>
            <CandidatesTable type='Interviewee' {...props} />}
          />
          <Route exact path='/students*' render={props =>
            <CandidatesTable type='Student' {...props} />}
          />
          <Route exact path='/trainees*' render={props =>
            <CandidatesTable type='Trainee' {...props} />}
          />
          <Route exact path='/*' render={props =>
            <CandidatesTable type='Candidate' {...props} />}
          />
          <Route path='' render={() => <ErrorPage errorCode={404} errorMessage='Page not found'/>}/>
        </Switch>

    const refreshingSpinner =
      fetching && !initializing && pageTitle === 'Candidate Accounting'?
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
  pageTitle: PropTypes.string.isRequired,
  currentCandidateId: PropTypes.string.isRequired
}

export default connect(state => ({
    initializing: SELECTORS.APPLICATION.INITIALIZING(state),
    fetching: SELECTORS.APPLICATION.FETCHING(state),
    candidates: SELECTORS.CANDIDATES.CANDIDATES(state),
    errorMessage: SELECTORS.APPLICATION.ERRORMESSAGE(state),
    pageTitle: SELECTORS.APPLICATION.PAGETITLE(state),
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
  z-index: 3;
  top: 38%;
  width: 100%;
  text-align: center;
`

const MainWrapper = styled.div`
  position: relative;
  margin-top: 108px;
`