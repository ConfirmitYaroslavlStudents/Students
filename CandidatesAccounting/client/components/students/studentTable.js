import React from 'react';
import PropTypes from 'prop-types';
import Table from '../common/sortableTable';
import CandidateRowControls from '../candidates/candidateControls';
import {formatDate, isBirthDate} from '../../utilities/customMoment';
import TagList from '../tags/tagList';
import styled from 'styled-components';

export default class StudentTable extends React.Component {
  render() {
    return (
      <Table
        heads={[
          {title: 'Name', isSortable: true, sortType: 'byName'},
          {title: 'E-mail'},
          {title: 'Birth Date', isSortable: true, sortType: 'byDay'},
          {title: 'Group'},
          {title: 'Learning start', isSortable: true, sortType: 'byDate'},
          {title: 'Learning end', isSortable: true, sortType: 'byDate'},
          {title: 'Actions'}
        ]}
        contentRows={
          (this.props.students.map((student, index) =>
            [
              {content:
                <NameWrapper>
                  <span style={{whiteSpace: 'nowrap'}}>{student.name}</span>
                  <TagList tags={student.tags} currentLocation="/students"/>
                </NameWrapper>,
               value: student.name
              },
              {content: student.email},
              {content:
                <span style={{whiteSpace: 'nowrap'}} className={isBirthDate(student.birthDate) ? 'today' : ''}>
                  {formatDate(student.birthDate)}
                </span>,
                value: student.birthDate},
              {content: student.groupName},
              {content:
                <span style={{whiteSpace: 'nowrap'}}>{formatDate(student.startingDate)}</span>,
               value: student.startingDate},
              {content: <span style={{whiteSpace: 'nowrap'}}>{formatDate(student.endingDate)}</span>,
               value: student.endingDate},
              {content: <ControlsWrapper>
                <CandidateRowControls candidate={student} {...this.props}/>
              </ControlsWrapper>}
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