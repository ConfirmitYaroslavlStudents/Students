import React, {Component} from 'react';
import PropTypes from 'prop-types';
import Table from '../common/sortableTable';
import CandidateRowControls from '../candidates/candidateControls';
import {formatDate, isBirthDate} from '../../utilities/customMoment';
import TagList from '../tags/tagList';
import styled from 'styled-components';

export default class TraineeTable extends Component {
  componentWillMount() {
    if (this.props.pageTitle !== 'Candidate Accounting') {
      this.props.setPageTitle('Candidate Accounting');
      this.props.setSearchRequest('', this.props.history, 0);
    }
  }

  render() {
    return (
      <Table
        heads={[
          {title: 'Name', sorting: 'byAlphabet'},
          {title: 'E-mail', sorting: 'byAlphabet'},
          {title: 'Birth Date'},
          {title: 'Mentor', sorting: 'byAlphabet'},
          {title: 'Actions'}]}
        contentRows={
          (this.props.trainees.map((trainee, index) =>
            [
              {
                content:
                  <NameWrapper>
                    <span style={{whiteSpace: 'nowrap'}}>{trainee.name}</span>
                    <TagList tags={trainee.tags} currentLocation="/trainees"/>
                  </NameWrapper>,
                value: trainee.name},
              {
                content: trainee.email,
                value: trainee.email
              },
              {
                content:
                  <span style={{whiteSpace: 'nowrap'}} className={isBirthDate(trainee.birthDate) ? 'today' : ''}>
                    {formatDate(trainee.birthDate)}
                  </span>
              },
              {
                content: <span style={{whiteSpace: 'nowrap'}}>{trainee.mentor}</span>,
                value: trainee.mentor},
              {
                content: <ControlsWrapper><CandidateRowControls candidate={trainee} {...this.props}/></ControlsWrapper>
              }]
          ))}
        changeURL={this.props.changeURL}
        history={this.props.history}
        offset={this.props.candidatesOffset}
        rowsPerPage={this.props.candidatesPerPage}
        totalCount={this.props.candidatesTotalCount}
      />
    );
  }
}

TraineeTable.propTypes = {
  trainees: PropTypes.oneOfType([PropTypes.array, PropTypes.object]).isRequired,
};

const NameWrapper = styled.div`
  display: flex;
  align-items: center;
`;

const ControlsWrapper = styled.div`
  display: flex;
  float: right;
`;