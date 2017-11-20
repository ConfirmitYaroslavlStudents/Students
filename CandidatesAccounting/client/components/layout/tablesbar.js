import React from 'react';
import PropTypes from 'prop-types';
import Tabs, {Tab} from 'material-ui/Tabs';
import AppBar from 'material-ui/AppBar';
import {NavLink} from 'react-router-dom';
import AddCandidateDialog from '../candidates/addCandidateDialog';
import styled from 'styled-components';

function getNewURL(currentLocation, newTableType) {
  let newURL = '';
  let location = currentLocation.pathname.split('/', 1);
  location[0] = newTableType;
  location.forEach((loc) => {
    newURL += '/' + loc;
  });
  newURL += currentLocation.search;
  return newURL;
}

export default class CustomAppBar extends React.Component{
  constructor(props) {
    super(props);
    this.state = { selected: props.selected };
    this.handleChange = this.handleChange.bind(this);
  };

  handleChange(event, value) {
    this.setState({selected: value });
  };

  render() {
    this.state = { selected: this.props.selected };
    return (
      <AppBar className="tabs-bar" color="default">
        <TabsWrapper>
          <Tabs
            value={this.state.selected}
            indicatorColor="primary"
            textColor="primary"
            fullWidth
            centered
            onChange={this.handleChange}
          >
            <Tab label={<NavLink exact to={getNewURL(this.props.history.location, '')} className="nav-link"
                                 activeStyle={{color: '#3F51B5'}}>All</NavLink>}/>
            <Tab label={<NavLink to={getNewURL(this.props.history.location, 'interviewees')} className="nav-link"
                                 activeStyle={{color: '#3F51B5'}}>Interviewees</NavLink>}/>
            <Tab label={<NavLink to={getNewURL(this.props.history.location, 'students')} className="nav-link"
                                 activeStyle={{color: '#3F51B5'}}>Students</NavLink>}/>
            <Tab label={<NavLink to={getNewURL(this.props.history.location, 'trainees')} className="nav-link"
                                 activeStyle={{color: '#3F51B5'}}>Trainees</NavLink>}/>
          </Tabs>
        </TabsWrapper>
        <AddButtonWrapper>
          <AddCandidateDialog
            addCandidate={this.props.addCandidate}
            candidateStatus={this.props.newCandidateDefaultType}
            tags={this.props.tags}
            userName={this.props.userName}
          />
        </AddButtonWrapper>
      </AppBar>
    );
  }
}

CustomAppBar.PropTypes = {
  tags: PropTypes.object.isRequired,
  userName: PropTypes.string.isRequired,
  addCandidate: PropTypes.func.isRequired,
  newCandidateDefaultType: PropTypes.string.isRequired,
  history: PropTypes.object.isRequired,
  selected: PropTypes.number,
};

const TabsWrapper = styled.div`
  display: inline-block;
  position: absolute;
  left: 15%;
  right: 15%;
`;

const AddButtonWrapper = styled.div`
  display: inline-block;
  position: absolute;
  right: 5px;
  top: 5px;
`;