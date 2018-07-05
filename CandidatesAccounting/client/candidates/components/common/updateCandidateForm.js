import React, { Component } from 'react'
import PropTypes from 'prop-types'
import AvatarUploader from './avatarUploader'
import TextField from '../../../commonComponents/UIComponentDecorators/textField'
import PhoneNumberField from '../../../commonComponents/UIComponentDecorators/phoneNumberField'
import SelectInput from '../../../commonComponents/UIComponentDecorators/selectInput'
import TagSelect from '../../../commonComponents/UIComponentDecorators/tagSelect'
import { isNotEmpty } from '../../../utilities/candidateValidators'
import IntervieweeSpecialFields from '../interviewees/specialFields'
import StudentSpecialFields from '../students/specialFields'
import TraineeSpecialFields from '../trainees/specialFields'
import styled from 'styled-components'

export default class CandidateInfoForm extends Component {
  constructor(props) {
    super(props)
    this.state = ({ status: props.candidate.status, tags: props.candidate.tags, updateAvatarMessage: '' })
  }

  changeProperty = (key, value) => {
    this.props.candidate[key] = value
    if (key === 'avatarFile') {
      this.setState({ updateAvatarMessage: 'avatar will be updated' })
    } else {
      this.setState({ })
    }
  }

  changeCandidateStatus = (status) => {
    this.props.candidate.status = status
    this.setState({ status })
  }

  setCandidateTags = (tags) => {
    this.props.candidate.tags = tags.map(tag => tag.label)
    this.setState({ tags: tags.map(tag => tag.label) })
  }

  getSpecialFields = () => {
    const { candidate } = this.props

    switch (this.state.status) {
      case 'Interviewee':
        return <IntervieweeSpecialFields interviewee={candidate} changeProperty={this.changeProperty}/>
      case 'Student':
        return <StudentSpecialFields student={candidate} changeProperty={this.changeProperty}/>
      case 'Trainee':
        return <TraineeSpecialFields trainee={candidate} changeProperty={this.changeProperty}/>
    }
  }

  render() {
    const { candidate, tags } = this.props

    return (
      <CandidateFormWrapper data-test-candidate-form>
        <StatusAvatarWrapper>
          <SelectInput
            label="Candidate's status"
            options={['Interviewee', 'Student', 'Trainee']}
            selected={this.state.status}
            minWidth={240}
            onChange={this.changeCandidateStatus}
          />
          <AvatarWrapper>
            <AvatarUploader candidate={candidate} onAvatarUpload={
              (file) => {
                if (file) {
                  this.changeProperty('avatarFile', file)
                  this.changeProperty('hasAvatar', true)
                } else {
                  this.changeProperty('avatarFile', null)
                  this.changeProperty('hasAvatar', false)
                }
              }
            } />
            {this.state.updateAvatarMessage}
          </AvatarWrapper>
        </StatusAvatarWrapper>
        <TextFieldLabel>Tags</TextFieldLabel>
        <TagSelect
          onValuesChange={this.setCandidateTags}
          options={tags}
          values={this.state.tags}
        />
        <TextField
          onChange={value => {this.changeProperty('name', value)}}
          label='Name'
          placeholder='full name'
          value={candidate.name}
          checkValid={isNotEmpty}
          required
          autoFocus
          mark='data-test-candidate-name-input'
        />
        <TextField
          onChange={value => {this.changeProperty('nickname', value)}}
          label='Nickname'
          placeholder='nickname'
          value={candidate.nickname}
          mark='data-test-candidate-nickname-input'
        />
        <TextField
          onChange={value => {this.changeProperty('email', value)}}
          label='E-mail'
          placeholder='example@mail.com'
          value={candidate.email}
          mark='data-test-candidate-email-input'
        />
        <PhoneNumberField
          onChange={value => {this.changeProperty('phoneNumber', value)}}
          label='Phone number'
          value={candidate.phoneNumber}/>

        {this.getSpecialFields()}

        <SmallNoteSpan>* - required</SmallNoteSpan>
      </CandidateFormWrapper>
    )
  }
}

CandidateInfoForm.propTypes = {
  candidate: PropTypes.object.isRequired,
  tags: PropTypes.array.isRequired
}

const CandidateFormWrapper = styled.div`
  padding: 15px 15px 10px;
`

const StatusAvatarWrapper = styled.div`
  display: flex;
  align-items: center;
`

const AvatarWrapper = styled.div`
  display: inline-flex;
  align-items: center;
  padding-top: 4px;
  margin-left: 32px;
`

const TextFieldLabel = styled.p`
  margin-top: 16px;
  margin-bottom: ${props => props.marginBottom ? props.marginBottom : '0'};
  color: rgba(0,0,0,0.54);
  font-size: 80%;
`

const SmallNoteSpan = styled.span`
  color: rgba(0,0,0,0.5);
  fontSize: 80%;
  float: right;
`