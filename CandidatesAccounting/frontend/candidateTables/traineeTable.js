import React from 'react';
import PropTypes from 'prop-types';
import BasicTable from '../UIComponentDecorators/basicTable';
import CandidateRowControls from '../candidateComponents/candidateControls';
import {formatDate, isBirthDate} from '../customMoment';
import Tags from '../candidateComponents/tags';
import styled from 'styled-components';

export default class TraineeTable extends React.Component {
  render() {
    return (
      <BasicTable
        heads={ ['#', 'Name', 'E-mail', 'Birth Date', 'Mentor', <span style={{float: 'right'}}>Actions</span>] }
        contentRows={
          (this.props.trainees.map((trainee, index) =>
            [
              index + 1,
              <NameWrapper>
                <span style={{whiteSpace: 'nowrap'}}>{trainee.name}</span>
                <Tags tags={trainee.tags} currentLocation="/trainees"/>
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