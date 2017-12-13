import React from 'react';
import PropTypes from 'prop-types';
import Table from '../common/UIComponentDecorators/table';
import CandidateRowControls from '../candidates/candidateControls';
import {formatDateTime, formatDate, isToday, isBirthDate} from '../../utilities/customMoment';
import TagList from '../tags/tagList';
import ResumeControls from './resumeControls';
import styled from 'styled-components';

export default class IntervieweeTable extends React.Component {
  render() {
    return (
      <Table
        heads={ ['Name', 'E-mail', 'Birth Date',  'Interview date', 'Resume', <span style={{float: 'right'}}>Actions</span>] }
        contentRows={
          (this.props.interviewees.map((interviewee, index) =>
            [
              <NameWrapper>
                <span style={{whiteSpace: 'nowrap'}}>{interviewee.name}</span>
                <TagList tags={interviewee.tags} currentLocation="/interviewees"/>
              </NameWrapper>,
              interviewee.email,
              <span style={{whiteSpace: 'nowrap'}} className={isBirthDate(interviewee.birthDate) ? 'today' : ''}>
                {formatDate(interviewee.birthDate)}
              </span>,
              <span style={{whiteSpace: 'nowrap'}} className={isToday(interviewee.interviewDate) ? 'today' : ''}>
                {formatDateTime(interviewee.interviewDate)}
              </span>,
              <ResumeControls fileName={interviewee.resume}/>,
              <ControlsWrapper>
                <CandidateRowControls candidate={interviewee} {...this.props}/>
              </ControlsWrapper>
            ]
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