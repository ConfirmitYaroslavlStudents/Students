import React from 'react'
import PropTypes from 'prop-types'
import CandidateControls from '../common/candidateControls'
import { formatDateTime, isToday } from '../../../utilities/customMoment'
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
      <CandidateControls candidate={candidate}/>,
    <ResumeControls interviewee={candidate} disabled={disabled} downloadingEnabled/>,
    <Date highlighted={isToday(candidate.interviewDate)}>
      {formatDateTime(candidate.interviewDate)}
    </Date>,
    <p><a className='link' href={'mailto:' + candidate.email}>{candidate.email}</a></p>,
    <p><a className='link' href={'tel:' + candidate.phoneNumber.trim()}>{candidate.phoneNumber}</a></p>
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
    color: #ff4081;
    font-weight: bold;
	`}
`