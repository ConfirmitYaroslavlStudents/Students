import React from 'react'
import PropTypes from 'prop-types'
import CandidateControls from '../common/controls'
import { formatDate } from '../../../utilities/customMoment'
import TagList from '../../../tags/components/list'
import EmailWrapper from '../../../commonComponents/emailWrapper'
import PhoneNumberWrapper from '../../../commonComponents/phoneNumberWrapper'
import CandidateNameLink from '../../../commonComponents/candidateNameLink'
import NicknameWrapper from '../../../commonComponents/nicknameWrapper'
import styled, { css } from 'styled-components'

export default function StudentTableRow(props) {
  const { candidate } = props

  return [
    <CandidateNameWrapper>
      <CandidateNameLink candidate={candidate}>
        {candidate.name}
        <NicknameWrapper nickname={candidate.nickname} />
      </CandidateNameLink>
      <TagList candidateTags={candidate.tags} />
    </CandidateNameWrapper>,
    <CandidateControls candidate={candidate}/>,
    <Date>{formatDate(candidate.startingDate)}</Date>,
    <Date>{formatDate(candidate.endingDate)}</Date>,
    <p>{candidate.groupName}</p>,
    <EmailWrapper email={candidate.email}>{candidate.email}</EmailWrapper>,
    <PhoneNumberWrapper number={candidate.phoneNumber}>{candidate.phoneNumber}</PhoneNumberWrapper>
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