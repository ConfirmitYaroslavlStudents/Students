import React from 'react';
import PropTypes from 'prop-types';
import Table from '../common/UIComponentDecorators/table';
import CandidateRowControls from '../candidates/candidateControls';
import {formatDate, isBirthDate} from '../../utilities/customMoment';
import Tags from '../tags/tags';
import styled from 'styled-components';

export default class StudentTable extends React.Component {
  render() {
    return (
      <Table
        heads={ ['#', 'Name', 'E-mail', 'Birth Date',  'Group', 'Learning start', 'Learning end',
          <span style={{float: 'right'}}>Actions</span>]}
        contentRows={
          (this.props.students.map((student, index) =>
            [
              index + 1,
              <NameWrapper>
                <span style={{whiteSpace: 'nowrap'}}>{student.name}</span>
                <Tags tags={student.tags} currentLocation="/students"/>
              </NameWrapper>,
              student.email,
              <span style={{whiteSpace: 'nowrap'}} className={isBirthDate(student.birthDate) ? 'today' : ''}>
                {formatDate(student.birthDate)}
              </span>,
              student.groupName,
              <span style={{whiteSpace: 'nowrap'}}>{formatDate(student.startingDate)}</span>,
              <span style={{whiteSpace: 'nowrap'}}>{formatDate(student.endingDate)}</span>,
              <ControlsWrapper>
                <CandidateRowControls candidate={student} {...this.props}/>
              </ControlsWrapper>
            ]
          ))}
      />
    );
  }
}

StudentTable.propTypes = {
  students: PropTypes.oneOfType([PropTypes.array, PropTypes.object]).isRequired,
};

const NameWrapper = styled.div`
  display: flex;
  align-items: center;
`;

const ControlsWrapper = styled.div`
  display: flex;
  float: right;
`;