import React from 'react';
import PropTypes from 'prop-types';
import Table from '../common/UIComponentDecorators/table';
import CandidateRowControls from '../candidates/candidateControls';
import {formatDate, isBirthDate} from '../../utilities/customMoment';
import TagList from '../tags/tagList';
import styled from 'styled-components';

export default class TraineeTable extends React.Component {
  render() {
    return (
      <Table
        heads={ ['Name', 'E-mail', 'Birth Date', 'Mentor', <span style={{float: 'right'}}>Actions</span>] }
        contentRows={
          (this.props.trainees.map((trainee, index) =>
            [
              <NameWrapper>
                <span style={{whiteSpace: 'nowrap'}}>{trainee.name}</span>
                <TagList tags={trainee.tags} currentLocation="/trainees"/>
              </NameWrapper>,
              trainee.email,
              <span style={{whiteSpace: 'nowrap'}} className={isBirthDate(trainee.birthDate) ? 'today' : ''}>
                {formatDate(trainee.birthDate)}
              </span>,
              <span style={{whiteSpace: 'nowrap'}}>{trainee.mentor}</span>,
              <ControlsWrapper>
                <CandidateRowControls candidate={trainee} {...this.props}/>
              </ControlsWrapper>
            ]
          ))}
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