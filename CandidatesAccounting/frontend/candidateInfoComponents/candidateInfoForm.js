import React from 'react';
import PropTypes from 'prop-types';
import TextInput from '../materialUIDecorators/textInput';
import DatePicker from '../materialUIDecorators/customDatePicker';
import DateTimePicker from '../materialUIDecorators/customDateTimePicker';
import SimpleSelect from '../materialUIDecorators/simpleSelect';
import styled from 'styled-components';
import TagSelect from '../materialUIDecorators/tagSelect';
import ResumeControls from './resumeControls';
import {toDatePickerFormat, fromDatePickerFormat, toDateTimePickerFormat, fromDateTimePickerFormat} from "../customMoment";

export default class CandidateInfoForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = ({candidateStatus: props.candidate.status});
  }

  changeInfo(key, value) {
    this.props.candidate[key] = value;
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
    const changeInfo = this.changeInfo.bind(this);
    const changeCandidateStatus = this.changeCandidateStatus.bind(this);
    const setCandidateTags = this.setCandidateTags.bind(this);

    let specialFields;
    switch (this.state.candidateStatus) {
      case 'Interviewee':
        specialFields = <div>
          <DateTimePicker
            label="Interview date"
            defaultValue={toDateTimePickerFormat(this.props.candidate.interviewDate)}
            onChange={(value) => {changeInfo('interviewDate', fromDateTimePickerFormat(value))}}
          />
          <Label marginBottom="-10px">Resume</Label>
          <ResumeControls fileName={this.props.candidate.resume}/>
        </div>;
        break;

      case 'Student':
        specialFields = <div>
          <TextInput
            name="groupName"
            label="Group name"
            value={this.props.candidate.groupName}
            onChange={(value) => {changeInfo('groupName', value)}}
            multiline/>
          <DatePicker
            label="Learning start date"
            defaultValue={toDatePickerFormat(this.props.candidate.startingDate)}
            onChange={(value) => {changeInfo('startingDate', fromDatePickerFormat(value))}}
          />
          <DatePicker
            label="Learning end date"
            defaultValue={toDatePickerFormat(this.props.candidate.endingDate)}
            onChange={(value) => {changeInfo('endingDate', fromDatePickerFormat(value))}}
          />
        </div>;
        break;

      case 'Trainee':
        specialFields = <div>
          <TextInput
            name="mentor"
            label="Mentor's name"
            value={this.props.candidate.mentor}
            placeholder="full name"
            onChange={(value) => {changeInfo('mentor', value)}}
            multiline/>
        </div>;
        break;
    }

    return (
      <FormWrapper>
        <SimpleSelect
          label="Candidate's status"
          options={['Interviewee', 'Student', 'Trainee']}
          selected={this.state.candidateStatus}
          onChange={changeCandidateStatus}
        />
        <Label>Tags</Label>
        <TagSelect
          onValuesChange={setCandidateTags}
          options={this.props.tags}
          defaultValues={this.props.candidate.tags}
          style={{width: '100%'}}
        />
        <TextInput
          name="name"
          label="Name*"
          value={this.props.candidate.name}
          placeholder="full name"
          onChange={(value) => {changeInfo('name', value)}}
          validationCheck={(value) => {return value && value.trim() !== ''}}
          multiline
          autoFocus/>
        <TextInput
          name="email"
          label="E-mail*"
          value={this.props.candidate.email}
          placeholder="example@mail.com"
          onChange={(value) => {changeInfo('email', value)}}
          validationCheck={(value) => {return value && /.+@.+\..+/i.test(value)}}
          multiline/>
        <DatePicker
          label="Birth date"
          defaultValue={toDatePickerFormat(this.props.candidate.birthDate)}
          onChange={(value) => {changeInfo('birthDate', fromDatePickerFormat(value))}}
        />

        {specialFields}

        <span style={{color: 'rgba(0,0,0,0.5)', fontSize:'80%', float: 'right'}}>* - required</span>
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
`;

const Label = styled.p`
  margin-top: 16px;
  margin-bottom: ${props => props.marginBottom ? props.marginBottom : '0'};;
  color: rgba(0,0,0,0.54);
  font-size: 80%;
`;