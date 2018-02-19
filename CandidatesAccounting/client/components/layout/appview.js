import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Switch, Route } from 'react-router-dom';
import actions from '../../redux/actions';
import Navbar from './navbar';
import TablesBar from './tablesbar';
import { searchById, searchByRequest, searchByTag } from '../../utilities/candidateFilters';
import CommentsForm from '../comments/commentsForm';
import CandidateTable from '../candidates/candidateTable';
import IntervieweeTable from '../interviewees/intervieweeTable';
import StudentTable from '../students/studentTable';
import TraineeTable from '../trainees/traineeTable';
import SnackBar from '../common/UIComponentDecorators/snackbar';
import ErrorPage from './errorPage';
import { CircularProgress } from 'material-ui/Progress';

export default class AppView extends Component {
  componentWillMount() {
    let splitedURL = (this.props.history.location.pathname + this.props.history.location.search).split('?');
    let path = splitedURL[0];
    let splitedPath = path.split('/');
    if (splitedPath[3] === 'comments') {
      this.props.getCandidate(splitedPath[2]);
    } else {
      let candidateStatus = '';
      switch (splitedPath[1]) {
        case 'interviewees':
          candidateStatus = 'Interviewee';
          break;
        case 'students':
          candidateStatus = 'Student';
          break;
        case 'trainees':
          candidateStatus = 'Trainee';
          break;
      }
      let args = splitedURL[1];
      let argsObject = {};
      if (args) {
        let argsArray = args.split('&');
        argsArray.forEach((arg) => {
          let splited = arg.split('=');
          argsObject[splited[0]] = splited[1];
        });
      }
      this.props.setCandidateStatus(candidateStatus);
      this.props.setOffset(argsObject.skip ? Number(argsObject.skip) : 0);
      this.props.setCandidatesPerPage(argsObject.take ? Number(argsObject.take) : 15);
      this.props.setSortingField(argsObject.sort ? argsObject.sort : '');
      this.props.setSortingDirection(argsObject.sortDir ? argsObject.sortDir : 'desc');
      this.props.setSearchRequest(argsObject.q ? argsObject.q : '', this.props.history, 0);
      this.props.loadCandidates(this.props.history);
    }
  }

  render() {
    const currentLocation = this.props.location.pathname.split('/');
    const search = this.props.location.search;
    const filter = function(candidates) {
      if (true || search === '') {
        return candidates;
      }
      if (search[1] === 't') {
        return searchByTag(candidates, decodeURIComponent(search.substr(3)));
      }
      return searchByRequest(candidates, decodeURIComponent(search.substr(3)));
    };
    let selectedTableNumber = 0;
    let candidateType = 'Interviewee';
    switch (currentLocation[1]) {
      case 'interviewees':
        selectedTableNumber = 1;
        candidateType = 'Interviewee';
        break;
      case 'students':
        selectedTableNumber = 2;
        candidateType = 'Student';
        break;
      case 'trainees':
        selectedTableNumber = 3;
        candidateType = 'Trainee';
        break;
    }

    return (
      <div>
        <Navbar
          title={this.props.pageTitle}
          username={this.props.username}
          login={this.props.login}
          logout={this.props.logout}
          notifications={this.props.notifications}
          noticeNotification={this.props.noticeNotification}
          deleteNotification={this.props.deleteNotification}
          history={this.props.history}
          setSearchRequest={this.props.setSearchRequest}
          searchRequest={this.props.searchRequest}
          getCandidate={this.props.getCandidate}
        />
        <TablesBar
          selected={selectedTableNumber}
          newCandidateDefaultType={candidateType}
          addCandidate={this.props.addCandidate}
          tags={this.props.tags}
          username={this.props.username}
          history={this.props.history}
          setSearchRequest={this.props.setSearchRequest}
          pageTitle={this.props.pageTitle}
          setPageTitle={this.props.setPageTitle}
          candidatesPerPage={this.props.candidatesPerPage}
          totalCount={this.props.totalCount}
          setCandidateStatus={this.props.setCandidateStatus}
          setOffset={this.props.setOffset}
          loadCandidates={this.props.loadCandidates}
          setSortingField={this.props.setSortingField}
          setSortingDirection={this.props.setSortingDirection}
        />
        <div className='custom-main'>
          {
            this.props.applicationStatus === 'loading'?
              <div style={{textAlign: 'center', padding: 50}}>
                <CircularProgress size={60}/>
              </div>
              :
              <Switch>
                <Route exact path='/(interviewees|students|trainees)/(\w+)/comments' render={() =>
                  <CommentsForm
                    candidate={searchById(this.props.candidates, currentLocation[2])}
                    addComment={this.props.addComment}
                    deleteComment={this.props.deleteComment}
                    username={this.props.username}
                    subscribe={this.props.subscribe}
                    unsubscribe={this.props.unsubscribe}
                    pageTitle={this.props.pageTitle}
                    setPageTitle={this.props.setPageTitle}
                    history={this.props.history}
                    searchRequest={decodeURIComponent(this.props.location.search.substr(3))}
                    setSearchRequest={this.props.setSearchRequest}/>}
                />
                <Route exact path='/interviewees*' render={() =>
                  <IntervieweeTable interviewees={filter(this.props.candidates)} {...this.props}/>}
                />
                <Route exact path='/students*' render={() =>
                  <StudentTable students={filter(this.props.candidates)} {...this.props}/>}
                />
                <Route exact path='/trainees*' render={() =>
                  <TraineeTable trainees={filter(this.props.candidates)} {...this.props}/>}
                />
                <Route exact path='/*' render={() =>
                  <CandidateTable allCandidates={filter(this.props.candidates)} {...this.props}/>}
                />
                <Route path='' render={() => <ErrorPage errorCode={404} errorMessage='Page not found'/>}/>
              </Switch>
          }
        </div>
        <SnackBar message={this.props.errorMessage} setErrorMessage={this.props.setErrorMessage}/>
      </div>
    );
  }
}

function mapStateToProps(state) {
  return {
    applicationStatus: state.get('applicationStatus'),
    username: state.get('username'),
    pageTitle: state.get('pageTitle'),
    errorMessage: state.get('errorMessage'),
    searchRequest: state.get('searchRequest'),
    candidateStatus: state.get('candidateStatus'),
    offset: Number(state.get('offset')),
    candidatesPerPage: Number(state.get('candidatesPerPage')),
    totalCount: Number(state.get('totalCount')),
    sortingField: state.get('sortingField'),
    sortingDirection: state.get('sortingDirection'),
    candidates: state.get('candidates').toJS(),
    tags: state.get('tags').toArray(),
    notifications: state.get('notifications').toJS(),
  };
}

module.exports = connect(mapStateToProps, actions)(AppView);