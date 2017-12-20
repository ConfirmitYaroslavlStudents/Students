import React from 'react';
import PropTypes from 'prop-types';
import Table from '../common/sortableTable';
import CandidateRowControls from '../candidates/candidateControls';
import {formatDate, isBirthDate} from '../../utilities/customMoment';
import TagList from '../tags/tagList';
import styled from 'styled-components';

export default class TraineeTable extends React.Component {
  render() {
    return (
      <Table
        heads={[
          {title: 'Name', isSortable: true, sortType: 'byName'},
          {title: 'E-mail'},
          {title: 'Birth Date', isSortable: true, sortType: 'byDay'},
          {title: 'Mentor', isSortable: true, sortType: 'byName'},
          {title: 'Actions'}]}
        contentRows={
          (this.props.trainees.map((trainee, index) =>
            [
              {content:
                <NameWrapper>
                  <span style={{whiteSpace: 'nowrap'}}>{trainee.name}</span>
                  <TagList tags={trainee.tags} currentLocation="/trainees"/>
                </NameWrapper>,
               value: trainee.name},
              {content: trainee.email},
              {content:
                <span style={{whiteSpace: 'nowrap'}} className={isBirthDate(trainee.birthDate) ? 'today' : ''}>
                  {formatDate(trainee.birthDate)}
                </span>,
               value: trainee.birthDate},
              {content: <span style={{whiteSpace: 'nowrap'}}>{trainee.mentor}</span>,
              value: trainee.mentor},
              {content:
                <ControlsWrapper>
                  <CandidateRowControls candidate={trainee} {...this.props}/>
                </ControlsWrapper>}
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