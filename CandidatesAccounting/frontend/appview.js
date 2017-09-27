import React from 'react';
import PropTypes from 'prop-types';
import {connect} from 'react-redux';
import { NavLink } from 'react-router-dom';
import actions from './actions';
import Navbar from './materialUIDecorators/navbar';
import FullWidthTabs from './materialUIDecorators/fullWidthTabs';
import TableTabAll from './tableTabAll';
import TableTabInterviewees from './tableTabInterviewees';
import TableTabStudents from './tableTabStudents';
import TableTabTrainees from './tableTabTrainees';

export default class AppView extends React.Component {
  render() {
    const labels=[
      <NavLink to="/" className="nav-link" >All</NavLink>,
      <NavLink to="/interviewees" className="nav-link" activeStyle={{color: 'purple'}}>Interviewees</NavLink>,
      <NavLink to="/students" className="nav-link" activeStyle={{color: 'purple'}}>Students</NavLink>,
      <NavLink to="/trainees" className="nav-link" activeStyle={{color: 'purple'}}>Trainees</NavLink>
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

        <div className="footer">
          <span>2017 Work in progress</span>
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

AppView.propTypes = {
  selectedTab: PropTypes.number.isRequired,
};

module.exports = connect(mapStateToProps, actions)(AppView);