import React, {Component} from 'react';
import PropTypes from 'prop-types';
import {NavLink} from 'react-router-dom';
import styled from 'styled-components';
import Tabs from '../common/UIComponentDecorators/tabs';
import AddCandidateDialog from '../candidates/addCandidateDialog';

export default class TablesBar extends Component{
  constructor(props) {
    super(props);
    this.state = { selected: props.selected };
    this.handleChange = this.handleChange.bind(this);
    this.getNewURL = this.getNewURL.bind(this);
  };

  handleChange(event, value) {
    this.setState({selected: value });
  };

  getNewURL(newTableType) {
    if (this.props.pageTitle === 'Candidate Accounting' && this.props.history.location.pathname !== '/' + newTableType) {
      return '/' + newTableType + this.props.history.location.search;
    } else {
      return '/' + newTableType;
    }
  }

  render() {
    this.state = { selected: this.props.selected };
    return (
      <TabsBar>
        <TabsWrapper>
          <Tabs
            selected={this.state.selected}
            onChange={this.handleChange}
            tabs={[
              <NavLink exact to={this.getNewURL('')}
                       className={'nav-link' + (this.state.selected === 0 ? ' active-nav-link' : '')}>All</NavLink>,
              <NavLink to={this.getNewURL('interviewees')}
                       className={'nav-link' + (this.state.selected === 1 ? ' active-nav-link' : '')}>Interviewees</NavLink>,
              <NavLink to={this.getNewURL('students')}
                       className={'nav-link' + (this.state.selected === 2 ? ' active-nav-link' : '')}>Students</NavLink>,
              <NavLink to={this.getNewURL('trainees')}
                       className={'nav-link' + (this.state.selected === 3 ? ' active-nav-link' : '')}>Trainees</NavLink>
            ]}
          />
        </TabsWrapper>
        <AddButtonWrapper>
          <AddCandidateDialog
            addCandidate={this.props.addCandidate}
            candidateStatus={this.props.newCandidateDefaultType}
            tags={this.props.tags}
            userName={this.props.userName}
            disabled={this.props.authorizationStatus === 'not-authorized'}
          />
        </AddButtonWrapper>
      </TabsBar>
    );
  }
}

TablesBar.propTypes = {
  tags: PropTypes.array.isRequired,
  userName: PropTypes.string.isRequired,
  addCandidate: PropTypes.func.isRequired,
  newCandidateDefaultType: PropTypes.string.isRequired,
  history: PropTypes.object.isRequired,
  setSearchRequest: PropTypes.func.isRequired,
  pageTitle: PropTypes.string.isRequired,
  selected: PropTypes.number,
  authorizationStatus: PropTypes.string.isRequired,
};

const TabsBar = styled.div`
  display: flex;
  z-index: 110;
  color: rgba(0, 0, 0, 0.87);
  background-color: #f5f5f5;
  top: 0;
  left: auto;
  right: 0;
  position: fixed;
  width: 100%;
  flex-shrink: 0;
  flex-direction: column;
  box-shadow: 0px 2px 4px -1px rgba(0, 0, 0, 0.2), 0px 4px 5px 0px rgba(0, 0, 0, 0.14), 0px 1px 10px 0px rgba(0, 0, 0, 0.12);
  margin-top: 60px;
  height: 48px;
`;

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