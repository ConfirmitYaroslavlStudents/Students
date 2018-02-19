import React, {Component} from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import Tabs from '../common/UIComponentDecorators/tabs';
import NavLink from '../common/navLink';
import AddCandidateDialog from '../candidates/addCandidateDialog';

export default class TablesBar extends Component{
  constructor(props) {
    super(props);
    this.state = { selected: props.selected };
    this.handleChange = this.handleChange.bind(this);
    this.handleLinkClick = this.handleLinkClick.bind(this);
    this.handleAddCandidate = this.handleAddCandidate.bind(this);
  };

  handleChange(event, value) {
    this.setState({selected: value });
  };

  handleLinkClick(candidateStatus) {
    this.props.setCandidateStatus(candidateStatus);
    this.props.setOffset(0);
    this.props.loadCandidates(this.props.history);
  }

  handleAddCandidate(candidate) {
    this.props.addCandidate(candidate);
    this.props.setOffset(this.props.totalCount - this.props.totalCount % this.props.candidatesPerPage);
    this.props.loadCandidates(this.props.history);
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
              <NavLink active={this.state.selected === 0} onClick={() => {this.handleLinkClick('');}}>All</NavLink>,
              <NavLink active={this.state.selected === 1} onClick={() => {this.handleLinkClick('Interviewee');}}>Interviewees</NavLink>,
              <NavLink active={this.state.selected === 2} onClick={() => {this.handleLinkClick('Student');}}>Students</NavLink>,
              <NavLink active={this.state.selected === 3} onClick={() => {this.handleLinkClick('Trainee');}}>Trainees</NavLink>
            ]}
          />
        </TabsWrapper>
        <AddButtonWrapper>
          <AddCandidateDialog
            addCandidate={this.handleAddCandidate}
            candidateStatus={this.props.newCandidateDefaultType}
            tags={this.props.tags}
            disabled={this.props.username === ''}
          />
        </AddButtonWrapper>
      </TabsBar>
    );
  }
}

TablesBar.propTypes = {
  username: PropTypes.string.isRequired,
  pageTitle: PropTypes.string.isRequired,
  tags: PropTypes.array.isRequired,
  addCandidate: PropTypes.func.isRequired,
  newCandidateDefaultType: PropTypes.string.isRequired,
  history: PropTypes.object.isRequired,
  setSearchRequest: PropTypes.func.isRequired,
  setCandidateStatus: PropTypes.func.isRequired,
  setOffset: PropTypes.func.isRequired,
  candidatesPerPage: PropTypes.number.isRequired,
  loadCandidates: PropTypes.func.isRequired,
  totalCount: PropTypes.number.isRequired,
  selected: PropTypes.number,
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