import React from 'react'
import PropTypes from 'prop-types'
import CandidateControls from '../common/candidateControls'
import { formatDateTime, formatDate, isBirthDate, isToday } from '../../../utilities/customMoment'
import TagList from '../../../tags/components/tagList'
import ResumeControls from '../interviewees/resumeControls'
import styled, { css } from 'styled-components'

export default function IntervieweeTableRow(props) {
  const { candidate, disabled } = props

  return [
    <CandidateNameWrapper>
      <span className='nowrap'>{candidate.name}</span>
      <TagList candidateTags={candidate.tags} />
    </CandidateNameWrapper>,
    <p>{candidate.email}</p>,
    <Date highlighted={isBirthDate(candidate.birthDate)}>
      {formatDate(candidate.birthDate)}
    </Date>,
    <Date highlighted={isToday(candidate.interviewDate)}>
      {formatDateTime(candidate.interviewDate)}
    </Date>,
    <ResumeControls interviewee={candidate} disabled={disabled} downloadingEnabled/>,
    <CandidateControlsWrapper>
      <CandidateControls candidate={candidate}/>
    </CandidateControlsWrapper>
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

const CandidateControlsWrapper = styled.div`
  display: flex;
  float: right;
`

const Date = styled.div`
  white-space: nowrap;
  
  ${props => props.highlighted && css`
    color: #ff4081;
    font-weight: bold;
	`}
`