import React from 'react'
import PropTypes from 'prop-types'
import CandidateControls from './controls'
import TagList from '../../../tags/components/list'
import EmailWrapper from '../../../common/emailWrapper'
import PhoneNumberWrapper from '../../../common/phoneNumberWrapper'
import styled from 'styled-components'

export default function CandidateTableRow(props) {
  const { candidate } = props

  return [
    <CandidateNameWrapper>
      <span className='nowrap'>{candidate.name}</span>
      <TagList candidateTags={candidate.tags} />
    </CandidateNameWrapper>,
    <CandidateControls candidate={candidate}/>,
    <p>{candidate.status}</p>,
    <p><EmailWrapper email={candidate.email}>{candidate.email}</EmailWrapper></p>,
    <p><PhoneNumberWrapper number={candidate.phoneNumber}>{candidate.phoneNumber}</PhoneNumberWrapper></p>
  ]
}

CandidateTableRow.propTypes = {
  candidate: PropTypes.object.isRequired
}

const CandidateNameWrapper = styled.div`
  display: flex;
  align-items: center;
`