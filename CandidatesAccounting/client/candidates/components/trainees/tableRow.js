import React from 'react'
import PropTypes from 'prop-types'
import CandidateControls from '../common/controls'
import TagList from '../../../tags/components/list'
import EmailWrapper from '../../../commonComponents/emailWrapper'
import PhoneNumberWrapper from '../../../commonComponents/phoneNumberWrapper'
import CandidateNameLink from '../../../commonComponents/candidateNameLink'
import NicknameWrapper from '../../../commonComponents/nicknameWrapper'
import styled from 'styled-components'

export default function TraineeTableRow(props) {
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
    <p>{candidate.mentor}</p>,
    <EmailWrapper email={candidate.email}>{candidate.email}</EmailWrapper>,
    <PhoneNumberWrapper number={candidate.phoneNumber}>{candidate.phoneNumber}</PhoneNumberWrapper>
  ]
}

TraineeTableRow.propTypes = {
  candidate: PropTypes.object.isRequired
}

const CandidateNameWrapper = styled.div`
  display: flex;
  align-items: center;
`