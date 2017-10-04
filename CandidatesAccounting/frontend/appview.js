import React from 'react';
import {connect} from 'react-redux';
import { Switch, Route } from 'react-router-dom';
import actions from './actions';
import Navbar from './materialUIDecorators/navbar';
import TableTabAll from './tableTabAll';
import TableTabInterviewees from './tableTabInterviewees';
import TableTabStudents from './tableTabStudents';
import TableTabTrainees from './tableTabTrainees';
import Logo from 'material-ui-icons/AccountCircle';
import FlatButton from './materialUIDecorators/flatButton';
import TablesBar from './tablesBar';
import EditCommentForm from './commentsForm';

export default class AppView extends React.Component {
  render() {
    let props = this.props;
    let candidateId = 0;
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
    if (currentLocation[2]) {
      candidateId = parseInt(currentLocation[2]);
    }
    return (
      <div>
        <Navbar
          icon={<Logo />}
          title="Candidate Accounting"
          controls={<FlatButton color="contrast" onClick={()=>alert('TODO')} text="Sign out"/>}
        />
        <TablesBar
          selected={selectedTableNumber}
          newCandidateDefaultType = {newCandidateDefaultType}
          {...this.props}
        />
        <div className="custom-main">
          <Switch>
            <Route exact path="/" render={() =>
              <TableTabAll {...this.props}/>}/>
            <Route exact path="/interviewees" render={() =>
              <TableTabInterviewees {...this.props}/>}/>
            <Route exact path="/students" render={() =>
              <TableTabStudents {...this.props}/>}/>
            <Route exact path="/trainees" render={() =>
              <TableTabTrainees {...this.props}/>}/>
            <Route path="/*/comments" render={() =>
              <EditCommentForm
                candidate={props.candidates.toArray()[candidateId - 1]}
                editCandidate={props.editCandidate} />} />
          </Switch>
        </div>
      </div>
    );
  }
}

function mapStateToProps(state) {
  return {
    candidates: state.get('candidates'),
    tempCandidate: state.get('tempCandidate')
  };
}

module.exports = connect(mapStateToProps, actions)(AppView);