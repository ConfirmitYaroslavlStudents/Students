import React from 'react'
import CandidateControls from '../common/controls'
import { formatDate } from '../../../utilities/customMoment'
import TagList from '../../../tags/components/list'
import EmailWrapper from '../../../components/emailWrapper'
import PhoneNumberWrapper from '../../../components/phoneNumberWrapper'
import CandidateNameLink from '../../../components/candidateNameLink'
import NicknameWrapper from '../../../components/nicknameWrapper'
import styled, { css } from 'styled-components'

const getStudentTableCells = (candidate) => {
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

export default getStudentTableCells