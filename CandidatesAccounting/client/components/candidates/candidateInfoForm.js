import React, {Component} from 'react';
import PropTypes from 'prop-types';
import TextField from '../common/UIComponentDecorators/textField';
import DatePicker from '../common/UIComponentDecorators/datePicker';
import SelectInput from '../common/UIComponentDecorators/selectInput';
import styled from 'styled-components';
import TagSelect from '../common/UIComponentDecorators/tagSelect';
import {toDatePickerFormat, fromDatePickerFormat} from '../../utilities/customMoment';
import {isNotEmpty, isEmail} from '../../utilities/candidateValidators';
import IntervieweeSpecialFields from '../interviewees/intervieweeSpecialFields';
import StudentSpecialFields from '../students/studentSpecialFields';
import TraineeSpecialFields from '../trainees/traineeSpecialFields';

export default class CandidateInfoForm extends Component {
  constructor(props) {
    super(props);
    this.state = ({candidateStatus: props.candidate.status});
    this.changeInfo = this.changeInfo.bind(this);
    this.changeCandidateStatus = this.changeCandidateStatus.bind(this);
    this.setCandidateTags = this.setCandidateTags.bind(this);
    this.getSpecialFields = this.getSpecialFields.bind(this);
  }

  changeInfo(key, value) {
    this.props.candidate[key] = value;
  }

  changeCandidateStatus(status) {
    this.setState({candidateStatus: status});
    this.props.candidate.status = status;
  }

  setCandidateTags(tags) {
    this.props.candidate.tags = tags.map((tag) => (tag.label));
  }

  getSpecialFields() {
    switch (this.state.candidateStatus) {
      case 'Interviewee':
          return <IntervieweeSpecialFields interviewee={this.props.candidate} changeInfo={this.changeInfo}/>;
      case 'Student':
        return <StudentSpecialFields student={this.props.candidate} changeInfo={this.changeInfo}/>;
      case 'Trainee':
        return <TraineeSpecialFields trainee={this.props.candidate} changeInfo={this.changeInfo}/>;
    }
  }

  render() {
    return (
      <FormWrapper>
        <SelectInput
          label="Candidate's status"
          options={['Interviewee', 'Student', 'Trainee']}
          selected={this.state.candidateStatus}
          onChange={this.changeCandidateStatus}/>
        <Label>Tags</Label>
        <TagSelect
          onValuesChange={this.setCandidateTags}
          options={this.props.tags}
          defaultValues={this.props.candidate.tags}
          style={{width: '100%'}}/>
        <TextField
          onChange={(value) => {this.changeInfo('name', value)}}
          label="Name"
          placeholder="full name"
          value={this.props.candidate.name}
          checkValid={isNotEmpty}
          required
          multiline
          fullWidth
          autoFocus/>
        <TextField
          onChange={(value) => {this.changeInfo('email', value)}}
          label="E-mail"
          placeholder="example@mail.com"
          value={this.props.candidate.email}
          checkValid={isEmail}
          required
          multiline
          fullWidth/>
        <DatePicker
          label="Birth date"
          defaultValue={toDatePickerFormat(this.props.candidate.birthDate)}
          onChange={(value) => {this.changeInfo('birthDate', fromDatePickerFormat(value))}}
        />

        {this.getSpecialFields()}

        <SmallSpan>* - required</SmallSpan>
      </FormWrapper>
    );
  }
}

CandidateInfoForm.propTypes = {
  candidate: PropTypes.object.isRequired,
  tags: PropTypes.array.isRequired,
};

const FormWrapper = styled.div`
  padding: 15px 15px 10px;
  margin-right: 1px;
`;

const Label = styled.p`
  margin-top: 16px;
  margin-bottom: ${props => props.marginBottom ? props.marginBottom : '0'};;
  color: rgba(0,0,0,0.54);
  font-size: 80%;
`;

const SmallSpan = styled.span`
  color: rgba(0,0,0,0.5);
  fontSize: 80%;
  float: right;
`;