import React from 'react';
import {connect} from 'react-redux';
import { Switch, Route } from 'react-router-dom';
import actions from './actions';
import Navbar from './materialUIDecorators/navbar';
import Logo from 'material-ui-icons/AccountCircle';
import IconButton from './materialUIDecorators/iconButton';
import SignOutIcon from 'material-ui-icons/ExitToApp';
import AppBar from './appBar';
import CommentsForm from './commentInfoComponents/commentsForm';
import CandidateTable from './candidateInfoComponents/candidateTable';
import IntervieweeTable from './candidateInfoComponents/intervieweeTable';
import StudentTable from './candidateInfoComponents/studentTable';
import TraineeTable from './candidateInfoComponents/traineeTable';
import SnackBar from './materialUIDecorators/snackbar';

export default class AppView extends React.Component {
  render() {
    let props = this.props;
    const currentLocation = props.location.pathname.split('/');
    let selectedTableNumber = 0;
    let newCandidateDefaultType = 'Interviewee';
    switch (currentLocation[1]) {
      case 'interviewees':
        selectedTableNumber = 1;
        newCandidateDefaultType = 'Interviewee';
        break;
      case 'students':
        selectedTableNumber = 2;
        newCandidateDefaultType = 'Student';
        break;
      case 'trainees':
        selectedTableNumber = 3;
        newCandidateDefaultType = 'Trainee';
        break;
    }

    let candidateId = 0;
    if (currentLocation[2] !== '') {
      candidateId = parseInt(currentLocation[2]);
    }

    return (
      <div>
        <Navbar
          icon={<Logo />}
          title="Candidate Accounting"
          rightPart={<div style={{display: 'flex', alignItems: 'center'}}>
            <span>{this.props.userName}</span>
            <IconButton color="contrast" onClick={()=>alert('TODO')} icon={<SignOutIcon/>} />
          </div>}
        />
        <AppBar
          selected={selectedTableNumber}
          newCandidateDefaultType = {newCandidateDefaultType}
          addCandidate={props.addCandidate}
        />
        <div className="custom-main">
          <Switch>
            <Route exact path="/" render={() =>
              <CandidateTable candidates={props.candidates}
                              {...this.props}/>}/>
            <Route exact path="/interviewees" render={() =>
              <IntervieweeTable interviewees={props.candidates.filter((c) => c.constructor.name === 'Interviewee')}
                                {...this.props}/>}/>
            <Route exact path="/students" render={() =>
              <StudentTable students={props.candidates.filter((c) => c.constructor.name === 'Student')}
                            {...this.props}/>}/>
            <Route exact path="/trainees" render={() =>
              <TraineeTable trainees={props.candidates.filter((c) => c.constructor.name === 'Trainee')}
                            {...this.props}/>}/>
            <Route path="/*/comments" render={() =>
              <CommentsForm
                candidate={props.candidates.find(c => c.id === candidateId)}
                addComment={props.addComment}
                deleteComment={props.deleteComment}
                userName={this.props.userName} />}/>
          </Switch>
        </div>
        <SnackBar message={props.errorMessage} setErrorMessage={props.setErrorMessage}/>
      </div>
    );
  }
}

function mapStateToProps(state) {
  return {
    userName: state.get('userName'),
    candidates: state.get('candidates'),
    errorMessage: state.get('errorMessage')
  };
}

module.exports = connect(mapStateToProps, actions)(AppView);