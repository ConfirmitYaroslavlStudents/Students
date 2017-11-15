import React from 'react';
import {connect} from 'react-redux';
import { Switch, Route } from 'react-router-dom';
import actions from './actions';
import Navbar from './mainNavbar';
import AppBar from './appBar';
import CommentsForm from './commentComponents/commentsForm';
import CandidateTable from './candidateTables/candidateTable';
import IntervieweeTable from './candidateTables/intervieweeTable';
import StudentTable from './candidateTables/studentTable';
import TraineeTable from './candidateTables/traineeTable';
import SnackBar from './UIComponentDecorators/snackbar';

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
            <Route path="/*/comments" render={() =>
              <CommentsForm
                candidate={this.props.candidates.find(c => c.id === parseInt(currentLocation[2]))}
                addComment={this.props.addComment}
                deleteComment={this.props.deleteComment}
                userName={this.props.userName} />}/>
            <Route path="/tag/*" render={() =>
              <CandidateTable allCandidates={this.props.candidates.filter((c) => c.tags.includes(decodeURIComponent(currentLocation[2])))}
                              {...this.props}/>}/>
            <Route path="/*/tag/*" render={() => {
              switch (candidateType) {
                case 'Interviewee':
                  return (
                    <IntervieweeTable {...this.props}
                      interviewees={this.props.candidates.filter((c) => c.constructor.name === 'Interviewee'
                      && c.tags.includes(decodeURIComponent(currentLocation[3])))}
                />);
                case 'Student':
                  return (
                    <StudentTable {...this.props}
                      students={this.props.candidates.filter((c) => c.constructor.name === 'Student'
                        && c.tags.includes(decodeURIComponent(currentLocation[3])))}
                    />);
                case 'Trainee':
                  return (
                    <TraineeTable {...this.props}
                      trainees={this.props.candidates.filter((c) => c.constructor.name === 'Trainee'
                      && c.tags.includes(decodeURIComponent(currentLocation[3])))}
                    />)
              }}}
            />
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