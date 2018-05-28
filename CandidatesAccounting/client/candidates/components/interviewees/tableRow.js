import React from 'react'
import PropTypes from 'prop-types'
import CandidateControls from '../common/controls'
import { formatDateTime, isToday } from '../../../utilities/customMoment'
import TagList from '../../../tags/components/list'
import ResumeControls from '../interviewees/resumeControls'
import EmailWrapper from '../../../commonComponents/emailWrapper'
import PhoneNumberWrapper from '../../../commonComponents/phoneNumberWrapper'
import CandidateNameLink from '../../../commonComponents/candidateNameLink'
import NicknameWrapper from '../../../commonComponents/nicknameWrapper'
import styled, { css } from 'styled-components'

export default function IntervieweeTableRow(props) {
  const { candidate, disabled } = props

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

IntervieweeTableRow.propTypes = {
  candidate: PropTypes.object.isRequired,
  disabled: PropTypes.bool
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