import React from 'react'
import PropTypes from 'prop-types'
import CandidateControls from '../common/candidateControls'
import { formatDate } from '../../../utilities/customMoment'
import TagList from '../../../tags/components/tagList'
import EmailWrapper from '../../../common/emailWrapper'
import PhoneNumberWrapper from '../../../common/phoneNumberWrapper'
import styled, { css } from 'styled-components'

export default function StudentTableRow(props) {
  const { candidate } = props

  return [
    <CandidateNameWrapper>
      <span className='nowrap'>{candidate.name}</span>
      <TagList candidateTags={candidate.tags} />
    </CandidateNameWrapper>,
    <CandidateControls candidate={candidate}/>,
    <Date>{formatDate(candidate.startingDate)}</Date>,
    <Date>{formatDate(candidate.endingDate)}</Date>,
    <p>{candidate.groupName}</p>,
    <p><EmailWrapper email={candidate.email}>{candidate.email}</EmailWrapper></p>,
    <p><PhoneNumberWrapper number={candidate.phoneNumber}>{candidate.phoneNumber}</PhoneNumberWrapper></p>
  ]
}

StudentTableRow.propTypes = {
  candidate: PropTypes.object.isRequired
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