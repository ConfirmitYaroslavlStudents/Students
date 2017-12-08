import React from 'react';
import {connect} from 'react-redux';
import {Switch, Route} from 'react-router-dom';
import actions from '../common/actions';
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

export default class AppView extends React.Component {
  render() {
    const currentLocation = this.props.location.pathname.split('/');
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
          userName={this.props.userName}
          setUserName={this.props.setUserName}
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
        />
        <div className="custom-main">
          <Switch>
            <Route exact path="/" render={() =>
              <CandidateTable allCandidates={searchByRequest(this.props.candidates, this.props.location.search !== "" ?
                                  decodeURIComponent(this.props.location.search.substr(3)) : null)} {...this.props}/>}
            />
            <Route exact path="/interviewees" render={() =>
              <IntervieweeTable interviewees={searchByRequest(searchByStatus(this.props.candidates, 'Interviewee'),
                this.props.location.search !== "" ? decodeURIComponent(this.props.location.search.substr(3)) : null)}
                                {...this.props}/>}
            />
            <Route exact path="/students" render={() =>
              <StudentTable students={searchByRequest(searchByStatus(this.props.candidates, 'Student'),
                  this.props.location.search !== "" ? decodeURIComponent(this.props.location.search.substr(3)) : null)}
                            {...this.props}/>}
            />
            <Route exact path="/trainees" render={() =>
              <TraineeTable trainees={searchByRequest(searchByStatus(this.props.candidates, 'Trainee'),
                  this.props.location.search !== "" ? decodeURIComponent(this.props.location.search.substr(3)) : null)}
                            {...this.props}/>}
            />
            <Route exact path='/(interviewees|students|trainees)/(\w+)/comments' render={() =>
              <CommentsForm
                candidate={searchById(this.props.candidates._tail.array, currentLocation[2])}
                addComment={this.props.addComment}
                deleteComment={this.props.deleteComment}
                userName={this.props.userName}
                searchRequest={decodeURIComponent(this.props.location.search.substr(3))}/>}
            />
            <Route exact path="/tag/*" render={() =>
              <CandidateTable allCandidates={searchByTag(this.props.candidates, decodeURIComponent(currentLocation[2]))}
                              {...this.props}/>}
            />
            <Route exact path="/interviewees/tag/*" render={() =>
              <IntervieweeTable {...this.props}
                interviewees={searchByTag(searchByStatus(this.props.candidates, 'Interviewee'), decodeURIComponent(currentLocation[3]))}/>}
            />
            <Route exact path="/students/tag/*" render={() =>
              <StudentTable {...this.props}
                students={searchByTag(searchByStatus(this.props.candidates, 'Student'), decodeURIComponent(currentLocation[3]))}/>}
            />
            <Route exact path="/trainees/tag/*" render={() =>
              <TraineeTable {...this.props}
                trainees={searchByTag(searchByStatus(this.props.candidates, 'Trainee'), decodeURIComponent(currentLocation[3]))}/>}
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
    candidates: state.get('candidates'),
    tags: state.get('tags'),
    searchRequest: state.get('searchRequest'),
    errorMessage: state.get('errorMessage')
  };
}

module.exports = connect(mapStateToProps, actions)(AppView);