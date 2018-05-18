import React from 'react'
import PropTypes from 'prop-types'
import CandidateControls from '../common/candidateControls'
import TagList from '../../../tags/components/tagList'
import styled from 'styled-components'

export default function TraineeTableRow(props) {
  const { candidate } = props

  return [
    <CandidateNameWrapper>
      <span className='nowrap'>{candidate.name}</span>
      <TagList candidateTags={candidate.tags} />
    </CandidateNameWrapper>,
    <CandidateControls candidate={candidate}/>,
    <p>{candidate.mentor}</p>,
    <p><a className='link' href={'mailto:' + candidate.email}>{candidate.email}</a></p>,
    <p><a className='link' href={'tel:' + candidate.phoneNumber.trim()}>{candidate.phoneNumber}</a></p>
  ]
}

TraineeTableRow.propTypes = {
  candidate: PropTypes.object.isRequired
}

const CandidateNameWrapper = styled.div`
  display: flex;
  align-items: center;
`