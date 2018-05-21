import React from 'react'
import PropTypes from 'prop-types'
import EmailWrapper from '../../../common/emailWrapper'
import PhoneNumberWrapper from '../../../common/phoneNumberWrapper'
import FileDownloader from '../../../common/fileDownloader'
import UpdateCandidateDialog from './updateCandidateDialog'
import styled from 'styled-components'

export default function CandidateCard(props) {
  const { candidate, tags, authorized } = props

  let fields = []

  fields.push({
    key: 'Full name: ',
    value: candidate.name
  })

  fields.push({
    key: 'Status: ',
    value: candidate.status
  })

  if (candidate.email && candidate.email !== '') {
    fields.push({
      key: 'Email: ',
      value:
        <EmailWrapper email={candidate.email}>
          {candidate.email}
        </EmailWrapper>
    })
  }

  if (candidate.phoneNumber && candidate.phoneNumber !== '') {
    fields.push({
      key: 'Phone number: ',
      value:
        <PhoneNumberWrapper number={candidate.phoneNumber}>
          {candidate.phoneNumber}
        </PhoneNumberWrapper>
    })
  }

  if (candidate.interviewDate && candidate.interviewDate !== '') {
    fields.push({
      key: 'Interview date: ',
      value: candidate.interviewDate
    })
  }

  if (candidate.resume && candidate.resume !== '') {
    fields.push({
      key: 'CV: ',
      value:
        <FileDownloader
          downloadLink={window.location.origin + '/'
          + props.candidate.status.toLowerCase() + 's/'
          + props.candidate.id
          + '/resume'}
          withoutButton>
          <CVWrapper>{candidate.resume}</CVWrapper>
        </FileDownloader>
    })
  }

  if (candidate.groupName && candidate.groupName !== '') {
    fields.push({
      key: 'Group name: ',
      value: candidate.groupName
    })
  }

  if (candidate.startingDate && candidate.startingDate !== '') {
    fields.push({
      key: 'Learning start: ',
      value: candidate.startingDate
    })
  }

  if (candidate.endingDate && candidate.endingDate !== '') {
    fields.push({
      key: 'Learning end: ',
      value: candidate.endingDate
    })
  }

  if (candidate.mentor && candidate.mentor !== '') {
    fields.push({
      key: 'Mentor: ',
      value: candidate.mentor
    })
  }

  return (
    <CandidateCardWrapper>
      <UpdateButtonWrapper>
        <UpdateCandidateDialog candidate={candidate} tags={tags} disabled={!authorized}/>
      </UpdateButtonWrapper>

      {fields.map((field, index) =>
        <KeyValueWrapper key={index}>
          <KeyWrapper>{field.key}</KeyWrapper>
          <ValueWrapper>{field.value}</ValueWrapper>
        </KeyValueWrapper>
      )}
    </CandidateCardWrapper>
  )
}

CandidateCard.propTypes = {
  candidate: PropTypes.object.isRequired,
  authorized: PropTypes.bool.isRequired,
  tags: PropTypes.array.isRequired
}

const CandidateCardWrapper = styled.div`
  display: flex;
  flex-direction: column;
`

const UpdateButtonWrapper = styled.div`
  position: absolute;
  top: 36px;
  right: 20px;
`

const KeyValueWrapper = styled.div`
  margin: 6px;
`

const KeyWrapper = styled.span`
  font-size: 105%;
  font-weight: bold;
  color: #555;
`

const ValueWrapper = styled.span`
  font-size: 105%;
  color: #555;
`

const CVWrapper = styled.div`
  color: #42A5F5;
  cursor: pointer;
  display: inline-flex;
  align-items: center;
  
  &:hover {
    color: #64B5F6;   
   }
`