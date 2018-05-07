import React from 'react'
import PropTypes from 'prop-types'
import CandidateControls from '../common/candidateControls'
import { formatDate, isBirthDate } from '../../../utilities/customMoment'
import TagList from '../../../tags/components/tagList'
import styled, { css } from 'styled-components'

export default function StudentTableRow(props) {
  const { candidate } = props

  return [
    <CandidateNameWrapper>
      <span className='nowrap'>{candidate.name}</span>
      <TagList candidateTags={candidate.tags} />
    </CandidateNameWrapper>,
    <p>{candidate.email}</p>,
    <Date highlighted={isBirthDate(candidate.birthDate)}>
      {formatDate(candidate.birthDate)}
    </Date>,
    <p>{candidate.groupName}</p>,
    <Date>{formatDate(candidate.startingDate)}</Date>,
    <Date>{formatDate(candidate.endingDate)}</Date>,
    <CandidateControlsWrapper>
      <CandidateControls candidate={candidate}/>
    </CandidateControlsWrapper>
  ]
}

StudentTableRow.propTypes = {
  candidate: PropTypes.object.isRequired
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