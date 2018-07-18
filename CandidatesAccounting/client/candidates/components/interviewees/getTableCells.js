import React from 'react'
import CandidateControls from '../common/controls'
import { formatDateTime, isToday } from '../../../utilities/customMoment'
import TagList from '../../../tags/components/list'
import ResumeControls from './resumeControls'
import EmailWrapper from '../../../commonComponents/emailWrapper'
import PhoneNumberWrapper from '../../../commonComponents/phoneNumberWrapper'
import CandidateNameLink from '../../../commonComponents/candidateNameLink'
import NicknameWrapper from '../../../commonComponents/nicknameWrapper'
import styled, { css } from 'styled-components'

const getIntervieweeTableCells = (candidate, disabled) => {
  return [
    <CandidateNameWrapper>
      <CandidateNameLink candidate={candidate}>
        {candidate.name}
        <NicknameWrapper nickname={candidate.nickname} />
      </CandidateNameLink>
      <TagList candidateTags={candidate.tags} />
    </CandidateNameWrapper>,
      <CandidateControls candidate={candidate}/>,
    <ResumeControls interviewee={candidate} disabled={disabled} downloadingEnabled/>,
    <Date highlighted={isToday(candidate.interviewDate)}>
      {formatDateTime(candidate.interviewDate)}
    </Date>,
    <EmailWrapper email={candidate.email}>{candidate.email}</EmailWrapper>,
    <PhoneNumberWrapper number={candidate.phoneNumber}>{candidate.phoneNumber}</PhoneNumberWrapper>
  ]
}

const CandidateNameWrapper = styled.div`
  display: flex;
  align-items: center;
`

const Date = styled.div`
  white-space: nowrap;
  
  ${props => props.highlighted && css`
    color: #FF5722;
    font-weight: bold;
	`}
`

export default getIntervieweeTableCells