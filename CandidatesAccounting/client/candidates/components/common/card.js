import React from 'react'
import PropTypes from 'prop-types'
import EmailWrapper from '../../../commonComponents/emailWrapper'
import PhoneNumberWrapper from '../../../commonComponents/phoneNumberWrapper'
import FileDownloader from '../../../commonComponents/fileDownloader'
import UpdateCandidateDialog from './updateDialog'
import TagList from '../../../tags/components/list'
import NicknameWrapper from '../../../commonComponents/nicknameWrapper'
import styled from 'styled-components'
import { SmallerIconStyle } from '../../../commonComponents/styleObjects'

export default function CandidateCard(props) {
  const { candidate, tags, authorized } = props

  let fields = []

  fields.push({
    key: 'Full name: ',
    value:
      <div>
        {candidate.name}
        <NicknameWrapper nickname={candidate.nickname}/>
      </div>
  })

  if (candidate.tags && candidate.tags.length !== []) {
    fields.push({
      key: 'Tags: ',
      value: <TagList candidateTags={candidate.tags} />
    })
  }

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
        <UpdateCandidateDialog candidate={candidate} tags={tags} disabled={!authorized} iconStyle={SmallerIconStyle}/>
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
  overflow: hidden;
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
  color: #08c;
  cursor: pointer;
  display: inline-flex;
  align-items: center;
  
  &:hover {
    color: #2196F3;   
   }
`