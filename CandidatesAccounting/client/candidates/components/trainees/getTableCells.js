import React from 'react'
import CandidateControls from '../common/controls'
import TagList from '../../../tags/components/list'
import EmailWrapper from '../../../commonComponents/emailWrapper'
import PhoneNumberWrapper from '../../../commonComponents/phoneNumberWrapper'
import CandidateNameLink from '../../../commonComponents/candidateNameLink'
import NicknameWrapper from '../../../commonComponents/nicknameWrapper'
import styled from 'styled-components'

const getTraineeTableCells = (candidate) => {
  return [
    <CandidateNameWrapper>
      <CandidateNameLink candidate={candidate}>
        {candidate.name}
        <NicknameWrapper nickname={candidate.nickname} />
      </CandidateNameLink>
      <TagList candidateTags={candidate.tags} />
    </CandidateNameWrapper>,
    <CandidateControls candidate={candidate}/>,
    <p>{candidate.mentor}</p>,
    <EmailWrapper email={candidate.email}>{candidate.email}</EmailWrapper>,
    <PhoneNumberWrapper number={candidate.phoneNumber}>{candidate.phoneNumber}</PhoneNumberWrapper>
  ]
}

const CandidateNameWrapper = styled.div`
  display: flex;
  align-items: center;
`

export default getTraineeTableCells