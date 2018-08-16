import React from 'react'
import PropTypes from 'prop-types'
import EmailWrapper from '../../../components/emailWrapper'
import PhoneNumberWrapper from '../../../components/phoneNumberWrapper'
import FileDownloader from '../../../components/fileDownloader'
import UpdateCandidateDialog from './updateCandidateDialog'
import TagList from '../../../tags/components/list'
import NicknameWrapper from '../../../components/nicknameWrapper'
import styled from 'styled-components'
import { SmallerIconStyle } from '../../../components/styleObjects'

function CandidateCard(props) {
  const { candidate, tags, authorized } = props

  const fields = []

  const addField = (label, value) => {
    fields.push({ label: label, value: value })
  }

  addField(
    'Full name',
    <span>
      {candidate.name}
      <NicknameWrapper nickname={candidate.nickname}/>
    </span>
  )

  if (candidate.tags && candidate.tags.length !== 0) {
    addField('Tags', <TagList candidateTags={candidate.tags} />)
  }

  addField('Status', candidate.status)

  if (candidate.email && candidate.email !== '') {
    addField(
      'Email',
      <EmailWrapper email={candidate.email}>
        {candidate.email}
      </EmailWrapper>
    )
  }

  if (candidate.phoneNumber && candidate.phoneNumber !== '') {
    addField(
      'Phone number',
      <PhoneNumberWrapper number={candidate.phoneNumber}>
        {candidate.phoneNumber}
      </PhoneNumberWrapper>
    )
  }

  if (candidate.interviewDate && candidate.interviewDate !== '') {
    addField(
      'Interview date',
      candidate.interviewDate
    )
  }

  if (candidate.resume && candidate.resume !== '') {
    addField(
      'CV',
      <FileDownloader
        downloadLink={window.location.origin + '/'
        + props.candidate.status.toLowerCase() + 's/'
        + props.candidate.id
        + '/resume'}
        withoutButton>
        <CVWrapper>{candidate.resume}</CVWrapper>
      </FileDownloader>
    )
  }

  if (candidate.groupName && candidate.groupName !== '') {
    addField(
      'Group name',
      candidate.groupName
    )
  }

  if (candidate.startingDate && candidate.startingDate !== '') {
    addField(
      'Learning start',
      candidate.startingDate
    )
  }

  if (candidate.endingDate && candidate.endingDate !== '') {
    addField(
      'Learning end',
      candidate.endingDate
    )
  }

  if (candidate.mentor && candidate.mentor !== '') {
    addField(
      'Mentor',
      candidate.mentor
    )
  }

  return (
    <CandidateCardWrapper>
      <UpdateButtonWrapper>
        <UpdateCandidateDialog candidate={candidate} tags={tags} disabled={!authorized} iconStyle={SmallerIconStyle}/>
      </UpdateButtonWrapper>

      {fields.map((field, index) =>
        <FieldWrapper key={index}>
          <LabelWrapper>{field.label + ': '}</LabelWrapper>
          <ValueWrapper>{field.value}</ValueWrapper>
        </FieldWrapper>
      )}
    </CandidateCardWrapper>
  )
}

CandidateCard.propTypes = {
  candidate: PropTypes.object.isRequired,
  authorized: PropTypes.bool.isRequired,
  tags: PropTypes.array.isRequired
}

export default CandidateCard

const CandidateCardWrapper = styled.div`
  display: flex;
  flex-direction: column;
  overflow: hidden;
`

const UpdateButtonWrapper = styled.div`
  position: absolute;
  top: 36px;
  right: 20px;
`

const FieldWrapper = styled.div`
  margin: 6px;
`

const LabelWrapper = styled.span`
  font-size: 105%;
  font-weight: bold;
  color: #555;
`

const ValueWrapper = styled.span`
  font-size: 105%;
  color: #555;
`

const CVWrapper = styled.div`
  color: #1565C0;
  cursor: pointer;
  display: inline-flex;
  align-items: center;
  
  &:hover {
    color: #1E88E5;   
   }
`