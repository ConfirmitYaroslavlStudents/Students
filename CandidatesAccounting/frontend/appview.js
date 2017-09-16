import React from 'react';
import {connect} from 'react-redux';
import actions from './actions';
import Navbar from './materialUIDecorators/navbar';
import FullWidthTabs from './materialUIDecorators/fullWidthTabs';
import TableTabAll from './tableTabAll';
import TableTabInterviewees from './tableTabInterviewees';
import TableTabStudents from './tableTabStudents';
import TableTabTrainees from './tableTabTrainees';
import { NavLink } from 'react-router-dom';

export default class AppView extends React.Component {
  render() {
    const labels=[
      <NavLink to="/" className="nav-link">All</NavLink>,
      <NavLink to="/interviewees" className="nav-link">Interviewees</NavLink>,
      <NavLink to="/students" className="nav-link">Students</NavLink>,
      <NavLink to="/trainees" className="nav-link">Trainees</NavLink>
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
    return (
      <div>
        <Navbar title="Candidate Accounting"/>

        <FullWidthTabs
          selected={this.props.selectedTab}
          labels={ labels }
          tabs={tabs}
        />

        <div className="footer footer-transparent">
          <span>2017 Test</span>
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