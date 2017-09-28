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
import Logo from 'material-ui-icons/AccountCircle';
import FlatButton from './materialUIDecorators/flatButton';

export default class AppView extends React.Component {
  render() {
    const labels=[
      <NavLink exact to="/" className="nav-link" activeStyle={{color: '#673AB7'}}>All</NavLink>,
      <NavLink to="/interviewees" className="nav-link" activeStyle={{color: '#673AB7'}}>Interviewees</NavLink>,
      <NavLink to="/students" className="nav-link" activeStyle={{color: '#673AB7'}}>Students</NavLink>,
      <NavLink to="/trainees" className="nav-link" activeStyle={{color: '#673AB7'}}>Trainees</NavLink>
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
        <Navbar icon={<Logo />} title="Candidate Accounting" controls={<FlatButton color="contrast" onClick={()=>alert('TODO')} text="Sign out"/>}/>

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