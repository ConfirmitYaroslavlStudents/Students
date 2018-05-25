import React from 'react'
import PropTypes from 'prop-types'
import CandidateControls from './controls'
import TagList from '../../../tags/components/list'
import EmailWrapper from '../../../common/emailWrapper'
import PhoneNumberWrapper from '../../../common/phoneNumberWrapper'
import CandidateNameLink from '../../../common/candidateNameLink'
import styled from 'styled-components'

export default function CandidateTableRow(props) {
  const { candidate } = props

  return [
    <CandidateNameWrapper>
      <CandidateNameLink candidate={candidate}>{candidate.name}</CandidateNameLink>
      <TagList candidateTags={candidate.tags} />
    </CandidateNameWrapper>,
    <CandidateControls candidate={candidate}/>,
    <p>{candidate.status}</p>,
    <EmailWrapper email={candidate.email}>{candidate.email}</EmailWrapper>,
    <PhoneNumberWrapper number={candidate.phoneNumber}>{candidate.phoneNumber}</PhoneNumberWrapper>
  ]
}

CandidateTableRow.propTypes = {
  candidate: PropTypes.object.isRequired
}

const CandidateNameWrapper = styled.div`
  display: flex;
  align-items: center;
`