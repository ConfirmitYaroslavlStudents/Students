import React from 'react';
import {connect} from 'react-redux';
import {Switch, Route} from 'react-router-dom';
import actions from '../common/actions';
import Navbar from './navbar';
import AppBar from './tablesbar';
import {searchCandidates} from '../candidates/candidateFunctions';
import CommentsForm from '../comments/commentsForm';
import CandidateTable from '../candidates/candidateTable';
import IntervieweeTable from '../interviewees/intervieweeTable';
import StudentTable from '../students/studentTable';
import TraineeTable from '../trainees/traineeTable';
import SnackBar from '../common/UIComponentDecorators/snackbar';

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
        />
        <AppBar
          selected={selectedTableNumber}
          newCandidateDefaultType={candidateType}
          addCandidate={this.props.addCandidate}
          tags={this.props.tags}
          userName={this.props.userName}
        />
        <div className="custom-main">
          <Switch>
            <Route exact path="/" render={() =>
              <CandidateTable allCandidates={this.props.candidates}
                              {...this.props}/>}/>
            <Route exact path="/interviewees" render={() =>
              <IntervieweeTable interviewees={this.props.candidates.filter((c) => c.constructor.name === 'Interviewee')}
                                {...this.props}/>}/>
            <Route exact path="/students" render={() =>
              <StudentTable students={this.props.candidates.filter((c) => c.constructor.name === 'Student')}
                            {...this.props}/>}/>
            <Route exact path="/trainees" render={() =>
              <TraineeTable trainees={this.props.candidates.filter((c) => c.constructor.name === 'Trainee')}
                            {...this.props}/>}/>

            <Route exact path='/(interviewees|students|trainees)/(\d+)/comments' render={() =>
              <CommentsForm
                candidate={this.props.candidates.find(c => c.constructor.name === candidateType && c.id === parseInt(currentLocation[2]))}
                addComment={this.props.addComment}
                deleteComment={this.props.deleteComment}
                userName={this.props.userName} />}/>

            <Route exact path="/tag/*" render={() =>
              <CandidateTable allCandidates={this.props.candidates.filter((c) => c.tags.includes(decodeURIComponent(currentLocation[2])))}
                              {...this.props}/>}/>
            <Route exact path="/interviewees/tag/*" render={() =>
              <IntervieweeTable {...this.props}
                interviewees={this.props.candidates.filter((c) => c.constructor.name === 'Interviewee'
                && c.tags.includes(decodeURIComponent(currentLocation[3])))}/>}
            />
            <Route exact path="/students/tag/*" render={() =>
              <StudentTable {...this.props}
                students={this.props.candidates.filter((c) => c.constructor.name === 'Student'
                && c.tags.includes(decodeURIComponent(currentLocation[3])))}/>}
            />
            <Route exact path="/trainees/tag/*" render={() =>
              <TraineeTable {...this.props}
                trainees={this.props.candidates.filter((c) => c.constructor.name === 'Trainee'
                && c.tags.includes(decodeURIComponent(currentLocation[3])))}/>}
            />

            <Route exact path="/search/*" render={() =>
              <CandidateTable allCandidates={searchCandidates(this.props.candidates._tail.array, decodeURIComponent(currentLocation[2]))}
                              {...this.props}/>}/>
            <Route exact path="/interviewees/search/*" render={() =>
              <IntervieweeTable interviewees={searchCandidates(this.props.candidates.filter((c) => c.constructor.name === 'Interviewee'),
                                              decodeURIComponent(currentLocation[3]))} {...this.props}/>}
            />
            <Route exact path="/students/search/*" render={() =>
              <StudentTable students={searchCandidates(this.props.candidates.filter((c) => c.constructor.name === 'Student'),
                                      decodeURIComponent(currentLocation[3]))} {...this.props}/>}
            />
            <Route exact path="/trainees/search/*" render={() =>
              <TraineeTable trainees={searchCandidates(this.props.candidates.filter((c) => c.constructor.name === 'Trainee'),
                                      decodeURIComponent(currentLocation[3]))} {...this.props}/>}
            />
            <Route path="">
              <div>404 Page not found</div>
            </Route>
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
    errorMessage: state.get('errorMessage')
  };
}

module.exports = connect(mapStateToProps, actions)(AppView);