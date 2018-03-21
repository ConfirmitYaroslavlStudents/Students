import React, {Component} from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import Tabs from '../common/UIComponentDecorators/tabs';
import NavLink from '../common/navLink';
import AddCandidateDialog from '../candidates/addCandidateDialog';

export default class TablesBar extends Component{
  constructor(props) {
    super(props);
    this.handleLinkClick = this.handleLinkClick.bind(this);
  };

  handleLinkClick(candidateStatus) {
    let stateChanges = {};
    if (candidateStatus === this.props.candidateStatus) {
      stateChanges['searchRequest'] = '';
    } else {
      stateChanges['candidateStatus'] = candidateStatus;
    }
    stateChanges['offset'] = 0;
    stateChanges['sortingField'] = '';
    stateChanges['sortingDirection'] = 'desc';
    this.props.loadCandidates(
      stateChanges,
      this.props.history
    );
  }

  render() {
    let selected = 0;
    switch(this.props.candidateStatus) {
      case 'Interviewee':
        selected = 1;
        break;
      case 'Student':
        selected = 2;
        break;
      case 'Trainee':
        selected = 3;
        break;
    }

    return (
      <TabsBar>
        <TabsWrapper>
          <Tabs
            tabs={[
              <NavLink className="table-link" active={selected === 0} onClick={() => {this.handleLinkClick('');}}>All</NavLink>,
              <NavLink className="table-link" active={selected === 1} onClick={() => {this.handleLinkClick('Interviewee');}}>Interviewees</NavLink>,
              <NavLink className="table-link" active={selected === 2} onClick={() => {this.handleLinkClick('Student');}}>Students</NavLink>,
              <NavLink className="table-link" active={selected === 3} onClick={() => {this.handleLinkClick('Trainee');}}>Trainees</NavLink>
            ]}
          />
        </TabsWrapper>
        <AddButtonWrapper>
          <AddCandidateDialog
            addCandidate={
              (candidate) => {
                this.props.addCandidate(candidate);
                this.props.loadCandidates(
                  {
                    applicationStatus: 'refreshing',
                    offset: this.props.totalCount - this.props.totalCount % this.props.candidatesPerPage
                  },
                  this.props.history
                )
              }}
            candidateStatus={this.props.newCandidateDefaultType === '' ? 'Interviewee' : this.props.newCandidateDefaultType}
            tags={this.props.tags}
            disabled={this.props.username === ''}
          />
        </AddButtonWrapper>
      </TabsBar>
    );
  }
}

TablesBar.propTypes = {
  setApplicationStatus: PropTypes.func.isRequired,
  username: PropTypes.string.isRequired,
  pageTitle: PropTypes.string.isRequired,
  tags: PropTypes.array.isRequired,
  addCandidate: PropTypes.func.isRequired,
  newCandidateDefaultType: PropTypes.string.isRequired,
  history: PropTypes.object.isRequired,
  loadCandidates: PropTypes.func.isRequired,
  candidateStatus: PropTypes.string.isRequired,
  candidatesPerPage: PropTypes.number.isRequired,
  totalCount: PropTypes.number.isRequired
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