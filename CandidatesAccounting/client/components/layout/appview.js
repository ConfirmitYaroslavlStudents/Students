import React, {Component} from 'react';
import {connect} from 'react-redux';
import {Switch, Route} from 'react-router-dom';
import actions from '../../redux/actions';
import Navbar from './navbar';
import TablesBar from './tablesbar';
import {searchByStatus, searchById, searchByRequest, searchByTag} from '../../utilities/candidateFilters';
import CommentsForm from '../comments/commentsForm';
import CandidateTable from '../candidates/candidateTable';
import IntervieweeTable from '../interviewees/intervieweeTable';
import StudentTable from '../students/studentTable';
import TraineeTable from '../trainees/traineeTable';
import SnackBar from '../common/UIComponentDecorators/snackbar';
import ErrorPage from './errorPage';

export default class AppView extends Component {
  render() {
    const currentLocation = this.props.location.pathname.split('/');
    const search = this.props.location.search;
    const filter = function(candidates) {
      if (search === '') {
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
          userName={this.props.userName}
          login={this.props.login}
          history={this.props.history}
          setSearchRequest={this.props.setSearchRequest}
          searchRequest={this.props.searchRequest}
        />
        <TablesBar
          selected={selectedTableNumber}
          newCandidateDefaultType={candidateType}
          addCandidate={this.props.addCandidate}
          tags={this.props.tags}
          userName={this.props.userName}
          history={this.props.history}
          setSearchRequest={this.props.setSearchRequest}
          pageTitle={this.props.pageTitle}
          setPageTitle={this.props.setPageTitle}
        />
        <div className="custom-main">
          <Switch>
            <Route exact path="/" render={() =>
              <CandidateTable allCandidates={filter(this.props.candidates)} {...this.props}/>}
            />
            <Route exact path="/interviewees" render={() =>
              <IntervieweeTable interviewees={filter(searchByStatus(this.props.candidates, 'Interviewee'))} {...this.props}/>}
            />
            <Route exact path="/students" render={() =>
              <StudentTable students={filter(searchByStatus(this.props.candidates, 'Student'))} {...this.props}/>}
            />
            <Route exact path="/trainees" render={() =>
              <TraineeTable trainees={filter(searchByStatus(this.props.candidates, 'Trainee'))} {...this.props}/>}
            />
            <Route exact path='/(interviewees|students|trainees)/(\w+)/comments' render={() =>
              <CommentsForm
                candidate={searchById(this.props.candidates, currentLocation[2])}
                addComment={this.props.addComment}
                deleteComment={this.props.deleteComment}
                userName={this.props.userName}
                pageTitle={this.props.pageTitle}
                setPageTitle={this.props.setPageTitle}
                history={this.props.history}
                searchRequest={decodeURIComponent(this.props.location.search.substr(3))}
                setSearchRequest={this.props.setSearchRequest}/>}
            />
            <Route path="" render={() => <ErrorPage errorCode={404} errorMessage="Page not found"/>}/>
          </Switch>
        </div>
        <SnackBar message={this.props.errorMessage} setErrorMessage={this.props.setErrorMessage}/>
      </div>
    );
  }
}

function mapStateToProps(state) {
  return {
    userName: state.get('userName'),
    candidates: state.get('candidates').toJS(),
    tags: state.get('tags').toArray(),
    pageTitle: state.get('pageTitle'),
    searchRequest: state.get('searchRequest'),
    errorMessage: state.get('errorMessage')
  };
}

module.exports = connect(mapStateToProps, actions)(AppView);