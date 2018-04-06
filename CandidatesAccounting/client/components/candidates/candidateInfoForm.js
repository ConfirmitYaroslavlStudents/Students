import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { CandidateFormWrapper, TextFieldLabel, SmallNoteSpan } from '../common/styledComponents'
import TextField from '../common/UIComponentDecorators/textField'
import DatePicker from '../common/UIComponentDecorators/datePicker'
import SelectInput from '../common/UIComponentDecorators/selectInput'
import TagSelect from '../common/UIComponentDecorators/tagSelect'
import { toDatePickerFormat, fromDatePickerFormat } from '../../utilities/customMoment'
import { isNotEmpty, isEmail } from '../../utilities/candidateValidators'
import IntervieweeSpecialFields from '../interviewees/intervieweeSpecialFields'
import StudentSpecialFields from '../students/studentSpecialFields'
import TraineeSpecialFields from '../trainees/traineeSpecialFields'

export default class CandidateInfoForm extends Component {
  constructor(props) {
    super(props);
    this.state = ({candidateStatus: props.candidate.status});
  }

  changeProperty = (key, value) => {
    this.props.candidate[key] = value;
  }

  changeCandidateStatus = (status) => {
    this.setState({candidateStatus: status});
    this.props.candidate.status = status;
  }

  setCandidateTags = (tags) => {
    this.props.candidate.tags = tags.map(tag => tag.label);
  }

  getSpecialFields = () => {
    switch (this.state.candidateStatus) {
      case 'Interviewee':
          return <IntervieweeSpecialFields interviewee={this.props.candidate} changeProperty={this.changeProperty}/>;
      case 'Student':
        return <StudentSpecialFields student={this.props.candidate} changeProperty={this.changeProperty}/>;
      case 'Trainee':
        return <TraineeSpecialFields trainee={this.props.candidate} changeProperty={this.changeProperty}/>;
    }
  }

  render() {
    return (
      <CandidateFormWrapper>
        <SelectInput
          label="Candidate's status"
          options={['Interviewee', 'Student', 'Trainee']}
          selected={this.state.candidateStatus}
          minWidth={240}
          onChange={this.changeCandidateStatus}/>
        <TextFieldLabel>Tags</TextFieldLabel>
        <TagSelect
          onValuesChange={this.setCandidateTags}
          options={this.props.tags}
          defaultValues={this.props.candidate.tags}/>
        <TextField
          onChange={(value) => {this.changeProperty('name', value)}}
          label='Name'
          placeholder='full name'
          value={this.props.candidate.name}
          checkValid={isNotEmpty}
          required
          autoFocus/>
        <TextField
          onChange={(value) => {this.changeProperty('email', value)}}
          label='E-mail'
          placeholder='example@mail.com'
          value={this.props.candidate.email}
          checkValid={isEmail}
          required/>
        <DatePicker
          label='Birth date'
          defaultValue={toDatePickerFormat(this.props.candidate.birthDate)}
          onChange={(value) => {this.changeProperty('birthDate', fromDatePickerFormat(value))}}
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