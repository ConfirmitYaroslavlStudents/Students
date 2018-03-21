import React, { Component } from 'react';
import { connect } from 'react-redux';
import {
  Switch,
  Route
} from 'react-router-dom';
import actions from '../../redux/actions';
import Navbar from './navbar';
import TablesBar from './tablesbar';
import { searchById } from '../../utilities/candidateFilters';
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
      this.props.loadCandidates(
        {
          candidateStatus: candidateStatus,
          offset: argsObject.skip ? Number(argsObject.skip) : 0,
          candidatesPerPage: argsObject.take ? Number(argsObject.take) : 15,
          sortingField: argsObject.sort ? argsObject.sort : '',
          sortingDirection: argsObject.sortDir ? argsObject.sortDir : 'desc',
          searchRequest: argsObject.q ? decodeURIComponent(argsObject.q) : ''
        },
        this.props.history);
    }
  }

  render() {
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
          searchRequest={this.props.searchRequest}
          loadCandidates={this.props.loadCandidates}
          getCandidate={this.props.getCandidate}
        />
        <TablesBar
          newCandidateDefaultType={this.props.candidateStatus}
          addCandidate={this.props.addCandidate}
          tags={this.props.tags}
          setApplicationStatus={this.props.setApplicationStatus}
          username={this.props.username}
          history={this.props.history}
          pageTitle={this.props.pageTitle}
          candidatesPerPage={this.props.candidatesPerPage}
          totalCount={this.props.totalCount}
          loadCandidates={this.props.loadCandidates}
          candidateStatus={this.props.candidateStatus}
        />
        <div className='custom-main'>
          {
            this.props.applicationStatus === 'loading'?
              <div style={{textAlign: 'center', zIndex: 100, position: 'fixed', top: '20%', width: '100%'}}>
                <CircularProgress size={60}/>
              </div>
              :
              <Switch>
                <Route exact path='/(interviewees|students|trainees)/(\w+)/comments' render={() =>
                  <CommentsForm
                    candidate={searchById(this.props.candidates, this.props.history.location.pathname.split('/')[2])}
                    addComment={this.props.addComment}
                    deleteComment={this.props.deleteComment}
                    username={this.props.username}
                    subscribe={this.props.subscribe}
                    unsubscribe={this.props.unsubscribe}
                    setState={this.props.setState}/>}
                />
                <Route exact path='/interviewees*' render={() =>
                  <IntervieweeTable interviewees={this.props.candidates} {...this.props}/>}
                />
                <Route exact path='/students*' render={() =>
                  <StudentTable students={this.props.candidates} {...this.props}/>}
                />
                <Route exact path='/trainees*' render={() =>
                  <TraineeTable trainees={this.props.candidates} {...this.props}/>}
                />
                <Route exact path='/*' render={() =>
                  <CandidateTable allCandidates={this.props.candidates} {...this.props}/>}
                />
                <Route path='' render={() => <ErrorPage errorCode={404} errorMessage='Page not found'/>}/>
              </Switch>
          }
        </div>
        {
          this.props.applicationStatus === 'refreshing' ?
            <div style={{textAlign: 'center', zIndex: 100, position: 'fixed', top: '20%', width: '100%'}}>
              <CircularProgress size={60}/>
            </div>
            : ''
        }
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