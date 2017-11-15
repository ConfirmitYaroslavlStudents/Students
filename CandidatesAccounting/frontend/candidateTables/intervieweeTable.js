import React from 'react';
import PropTypes from 'prop-types';
import BasicTable from '../UIComponentDecorators/basicTable';
import CandidateRowControls from '../candidateComponents/candidateControls';
import {formatDateTime, formatDate, isToday, isBirthDate} from '../customMoment';
import Tags from '../UIComponentDecorators/tags';
import ResumeControls from '../candidateComponents/resumeControls';
import styled from 'styled-components';

export default class IntervieweeTable extends React.Component {
  render() {
    return (
      <BasicTable
        heads={ ['#', 'Name', 'E-mail', 'Birth Date',  'Interview date', 'Resume', <span style={{float: 'right'}}>Actions</span>] }
        contentRows={
          (this.props.interviewees.map((interviewee, index) =>
            [
              index + 1,
              <NameWrapper>
                <span style={{whiteSpace: 'nowrap'}}>{interviewee.name}</span>
                <Tags tags={interviewee.tags} />
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