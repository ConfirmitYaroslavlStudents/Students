import React from 'react';
import PropTypes from 'prop-types';
import TextField from '../common/UIComponentDecorators/textField';
import DatePicker from '../common/UIComponentDecorators/datePicker';
import DateTimePicker from '../common/UIComponentDecorators/dateTimePicker';
import SelectInput from '../common/UIComponentDecorators/selectInput';
import styled from 'styled-components';
import TagSelect from '../common/UIComponentDecorators/tagSelect';
import ResumeControls from '../interviewees/resumeControls';
import {toDatePickerFormat, fromDatePickerFormat, toDateTimePickerFormat, fromDateTimePickerFormat} from '../../utilities/customMoment';
import {checkName, checkEmail} from '../../utilities/candidateValidators';

export default class CandidateInfoForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = ({candidateStatus: props.candidate.status});
    this.changeInfo = this.changeInfo.bind(this);
    this.changeCandidateStatus = this.changeCandidateStatus.bind(this);
    this.setCandidateTags = this.setCandidateTags.bind(this);
  }

  changeInfo(key, value) {
    this.props.candidate[key] = value;
    this.forceUpdate();
  }

  changeCandidateStatus(status) {
    this.setState({ candidateStatus: status });
    this.props.candidate.status = status;
    this.props.candidate['interviewDate'] = '';
    this.props.candidate['resume'] = '';
    this.props.candidate['startingDate'] = '';
    this.props.candidate['endingDate'] = '';
    this.props.candidate['groupName'] = '';
    this.props.candidate['mentor'] = '';
  }

  setCandidateTags(tags) {
    this.props.candidate.tags = tags.map((tag) => (tag.label));
  }

  render() {
    let specialFields;
    switch (this.state.candidateStatus) {
      case 'Interviewee':
        specialFields = <div>
          <DateTimePicker
            label="Interview date"
            defaultValue={toDateTimePickerFormat(this.props.candidate.interviewDate)}
            onChange={(value) => {this.changeInfo('interviewDate', fromDateTimePickerFormat(value))}}
          />
          <Label marginBottom="-10px">Resume</Label>
          <ResumeControls fileName={this.props.candidate.resume}/>
        </div>;
        break;

      case 'Student':
        specialFields = <div>
          <TextField
            onChange={(value) => {this.changeInfo('groupName', value)}}
            label="Group name"
            defaultValue={this.props.candidate.groupName}
            multiline
            fullWidth/>
          <DatePicker
            label="Learning start date"
            defaultValue={toDatePickerFormat(this.props.candidate.startingDate)}
            onChange={(value) => {this.changeInfo('startingDate', fromDatePickerFormat(value))}}
          />
          <DatePicker
            label="Learning end date"
            defaultValue={toDatePickerFormat(this.props.candidate.endingDate)}
            onChange={(value) => {this.changeInfo('endingDate', fromDatePickerFormat(value))}}
          />
        </div>;
        break;

      case 'Trainee':
        specialFields = <div>
          <TextField
            onChange={(value) => {this.changeInfo('mentor', value)}}
            label="Mentor's name"
            placeholder="full name"
            defaultValue={this.props.candidate.mentor}
            multiline
            fullWidth/>
        </div>;
        break;
    }

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
          defaultValue={this.props.candidate.name}
          error={!checkName(this.props.candidate.name)}
          required
          multiline
          fullWidth
          autoFocus/>
        <TextField
          onChange={(value) => {this.changeInfo('email', value)}}
          label="E-mail"
          placeholder="example@mail.com"
          defaultValue={this.props.candidate.email}
          error={!checkEmail(this.props.candidate.email)}
          required
          multiline
          fullWidth/>
        <DatePicker
          label="Birth date"
          defaultValue={toDatePickerFormat(this.props.candidate.birthDate)}
          onChange={(value) => {this.changeInfo('birthDate', fromDatePickerFormat(value))}}
        />

        {specialFields}

        <SmallSpan>* - required</SmallSpan>
      </FormWrapper>
    );
  }
}

CandidateInfoForm.propTypes = {
  candidate: PropTypes.object.isRequired,
  tags: PropTypes.object.isRequired,
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