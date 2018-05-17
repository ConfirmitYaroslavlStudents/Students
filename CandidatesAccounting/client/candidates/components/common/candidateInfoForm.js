import React, { Component } from 'react'
import PropTypes from 'prop-types'
import TextField from '../../../common/UIComponentDecorators/textField'
import DatePicker from '../../../common/UIComponentDecorators/datePicker'
import SelectInput from '../../../common/UIComponentDecorators/selectInput'
import TagSelect from '../../../common/UIComponentDecorators/tagSelect'
import { toDatePickerFormat, fromDatePickerFormat } from '../../../utilities/customMoment'
import { isNotEmpty, isEmail } from '../../../utilities/candidateValidators'
import IntervieweeSpecialFields from '../interviewees/specialFields'
import StudentSpecialFields from '../students/specialFields'
import TraineeSpecialFields from '../trainees/specialFields'
import styled from 'styled-components'

export default class CandidateInfoForm extends Component {
  constructor(props) {
    super(props)
    this.state = ({ status: props.candidate.status, tags: props.candidate.tags })
  }

  changeProperty = (key, value) => {
    this.props.candidate[key] = value
    this.setState({ })
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
      <CandidateFormWrapper>
        <SelectInput
          label="Candidate's status"
          options={['Interviewee', 'Student', 'Trainee']}
          selected={this.state.status}
          minWidth={240}
          onChange={this.changeCandidateStatus}/>
        <TextFieldLabel>Tags</TextFieldLabel>
        <TagSelect
          onValuesChange={this.setCandidateTags}
          options={tags}
          values={this.state.tags}/>
        <TextField
          onChange={value => {this.changeProperty('name', value)}}
          label='Name'
          placeholder='full name'
          value={candidate.name}
          checkValid={isNotEmpty}
          required
          autoFocus/>
        <TextField
          onChange={value => {this.changeProperty('email', value)}}
          label='E-mail'
          placeholder='example@mail.com'
          value={candidate.email}
          checkValid={isEmail}
          required/>
        <DatePicker
          label='Birth date'
          defaultValue={toDatePickerFormat(candidate.birthDate)}
          onChange={value => {this.changeProperty('birthDate', fromDatePickerFormat(value))}}
        />

        {this.getSpecialFields()}

        <SmallNoteSpan>* - required</SmallNoteSpan>
      </CandidateFormWrapper>
    )
  }
}

CandidateInfoForm.propTypes = {
  candidate: PropTypes.object.isRequired,
  tags: PropTypes.array.isRequired,
}

const CandidateFormWrapper = styled.div`
  padding: 15px 15px 10px;
  margin-right: 1px;
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