import React from 'react';
import PropTypes from 'prop-types';
import Table from '../common/sortableTable';
import CandidateRowControls from '../candidates/candidateControls';
import {formatDateTime, formatDate, isToday, isBirthDate} from '../../utilities/customMoment';
import TagList from '../tags/tagList';
import ResumeControls from './resumeControls';
import styled from 'styled-components';

export default class IntervieweeTable extends React.Component {
  render() {
    return (
      <Table
        heads={[
          {title: 'Name', isSortable: true, sortType: 'byName'},
          {title: 'E-mail'},
          {title: 'Birth Date', isSortable: true, sortType: 'byDay'},
          {title: 'Interview date', isSortable: true, sortType: 'byTime'},
          {title: 'Resume'},
          {title: 'Actions'}]}
        contentRows={
          (this.props.interviewees.map((interviewee, index) =>
            [
              {content:
                <NameWrapper>
                  <span style={{whiteSpace: 'nowrap'}}>{interviewee.name}</span>
                  <TagList tags={interviewee.tags} currentLocation="/interviewees"/>
                </NameWrapper>,
              value: interviewee.name},
              {content: interviewee.email},
              {content:
                <span style={{whiteSpace: 'nowrap'}} className={isBirthDate(interviewee.birthDate) ? 'today' : ''}>
                  {formatDate(interviewee.birthDate)}
                </span>,
              value: interviewee.birthDate},
              {content:
                <span style={{whiteSpace: 'nowrap'}} className={isToday(interviewee.interviewDate) ? 'today' : ''}>
                  {formatDateTime(interviewee.interviewDate)}
                </span>,
              value: interviewee.interviewDate},
              {content: <ResumeControls fileName={interviewee.resume}/>},
              {content:
                <ControlsWrapper>
                  <CandidateRowControls candidate={interviewee} {...this.props}/>
                </ControlsWrapper>
              }]
          ))}
      />
    );
  }
}

IntervieweeTable.propTypes = {
  interviewees: PropTypes.oneOfType([PropTypes.array, PropTypes.object]).isRequired,
};

const NameWrapper = styled.div`
  display: flex;
  align-items: center;
`;

const ControlsWrapper = styled.div`
  display: flex;
  float: right;
`;