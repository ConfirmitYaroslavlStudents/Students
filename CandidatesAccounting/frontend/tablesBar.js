import React from 'react';
import PropTypes from 'prop-types';
import Tabs, { Tab } from 'material-ui/Tabs';
import AppBar from 'material-ui/AppBar';
import { NavLink } from 'react-router-dom';
import AddCandidateDialog from './candidateTables/addCandidateDialog';

export default class TablesBar extends React.Component{
  constructor(props) {
    super(props);
    this.state = { selected: props.selected };
  };

  render() {
    return (
      <AppBar className="tabs-bar" color="default">
        <div style={{display: 'inline-block', position: 'absolute', left: '15%', right: '15%'}}>
          <Tabs
            value={this.state.selected}
            indicatorColor="primary"
            textColor="primary"
            fullWidth
            centered
            onChange={this.handleChange.bind(this)}
          >
            <Tab label={<NavLink exact to="/" className="nav-link" activeStyle={{color: '#673AB7'}}>All</NavLink>}/>
            <Tab label={<NavLink to="/interviewees" className="nav-link" activeStyle={{color: '#673AB7'}}>Interviewees</NavLink>}/>
            <Tab label={<NavLink to="/students" className="nav-link" activeStyle={{color: '#673AB7'}}>Students</NavLink>}/>
            <Tab label={<NavLink to="/trainees" className="nav-link" activeStyle={{color: '#673AB7'}}>Trainees</NavLink>}/>
          </Tabs>
        </div>
          <AddCandidateDialog
            addCandidate={this.props.addCandidate}
            candidateStatus={this.props.newCandidateDefaultType}
          />
      </AppBar>
    );
  }

  handleChange(event, value) {
    this.setState({selected: value });
  };
}

TablesBar.PropTypes = {
  selected: PropTypes.number,
};