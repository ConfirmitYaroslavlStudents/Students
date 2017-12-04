import React from 'react';
import PropTypes from 'prop-types';
import Tabs, {Tab} from 'material-ui/Tabs';
import AppBar from 'material-ui/AppBar';
import {NavLink} from 'react-router-dom';
import AddCandidateDialog from '../candidates/addCandidateDialog';
import styled from 'styled-components';

export default class CustomAppBar extends React.Component{
  constructor(props) {
    super(props);
    this.state = { selected: props.selected };
    this.handleChange = this.handleChange.bind(this);
    this.getNewURL = this.getNewURL.bind(this);
    this.handleNavLinkClick = this.handleNavLinkClick.bind(this);
  };

  handleChange(event, value) {
    this.setState({selected: value });
  };

  getNewURL(newTableType) {
    let newURL = '/' + newTableType;
    if (!this.props.history.location.pathname.includes('comments')) {
      newURL += this.props.history.location.search;
    }
    return newURL;
  }

  handleNavLinkClick() {
    if (this.props.history.location.pathname.includes('comments')) {
      this.props.setSearchRequest('', this.props.history, 0);
    }
  }

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
            <Tab label={<NavLink exact to={this.getNewURL('')} onClick={this.handleNavLinkClick}
                        className={'nav-link' + (this.state.selected === 0 ? ' active-nav-link' : '')}>All</NavLink>}/>
            <Tab label={<NavLink to={this.getNewURL('interviewees')} onClick={this.handleNavLinkClick}
                        className={'nav-link' + (this.state.selected === 1 ? ' active-nav-link' : '')}>Interviewees</NavLink>}/>
            <Tab label={<NavLink to={this.getNewURL('students')} onClick={this.handleNavLinkClick}
                        className={'nav-link' + (this.state.selected === 2 ? ' active-nav-link' : '')}>Students</NavLink>}/>
            <Tab label={<NavLink to={this.getNewURL('trainees')} onClick={this.handleNavLinkClick}
                        className={'nav-link' + (this.state.selected === 3 ? ' active-nav-link' : '')}>Trainees</NavLink>}/>
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
  setSearchRequest: PropTypes.func.isRequired,
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