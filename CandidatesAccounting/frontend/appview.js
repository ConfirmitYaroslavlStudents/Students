import React from 'react';
import {connect} from 'react-redux';
import actions from './actions';
import Navbar from './materialUIDecorators/navbar';
import FullWidthTabs from './materialUIDecorators/fullWidthTabs';
import TableTabAll from './tableTabAll';
import TableTabInterviewees from './tableTabInterviewees';
import TableTabStudents from './tableTabStudents';
import TableTabTrainees from './tableTabTrainees';
import { Switch, Route, NavLink } from 'react-router-dom';

export default class AppView extends React.Component {
  render() {
    const labels=[
      <NavLink to="/" style={{textDecoration: 'none'}}><div>All</div></NavLink>,
      <NavLink to="/interviewees" style={{textDecoration: 'none'}}><div>Interviewees</div></NavLink>,
      <NavLink to="/students" style={{textDecoration: 'none'}}><div>Students</div></NavLink>,
      <NavLink to="/trainees" style={{textDecoration: 'none'}}><div>Trainees</div></NavLink>
    ];
    const tabs=[
      <TableTabAll candidates={this.props.candidates}
                   {...this.props}/>,
      <TableTabInterviewees interviewees={this.props.candidates.filter((c) => c.constructor.name === 'Interviewee')}
                          {...this.props}/>,
      <TableTabStudents students={this.props.candidates.filter((c) => c.constructor.name === 'Student')}
                        {...this.props}/>,
      <TableTabTrainees trainees={this.props.candidates.filter((c) => c.constructor.name === 'Trainee')}
                        {...this.props}/>
    ];
    const getTabs = (selectedTab) => <FullWidthTabs selected={selectedTab} labels={ labels } tabs={ tabs } />;
    return (
      <div>
        <Navbar title="Candidate Accounting"/>

        <Switch>
          <Route exact path="/" render={() => getTabs(0)}/>
          <Route path="/interviewees" render={() => getTabs(1)}/>
          <Route path="/students" render={() => getTabs(2)}/>
          <Route path="/trainees" render={() => getTabs(3)}/>
        </Switch>

        <div className="footer footer-transparent">
          <span className="footer-label">2017 Test</span>
        </div>
      </div>
    );
  }
}

function mapStateToProps(state) {
  return {
    candidates: state.get('candidates'),
    candidateEditInfo: state.get('candidateEditInfo')
  };
}

module.exports = connect(mapStateToProps, actions)(AppView);