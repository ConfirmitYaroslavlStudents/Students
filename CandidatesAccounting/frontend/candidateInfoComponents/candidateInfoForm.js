import React from 'react';
import PropTypes from 'prop-types';
import TextInput from '../materialUIDecorators/textInput';
import SimpleSelect from '../materialUIDecorators/simpleSelect';
import styled from 'styled-components';

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

  render() {
    const changeInfo = this.changeInfo.bind(this);
    const changeCandidateStatus = this.changeCandidateStatus.bind(this);

    let specialFields;
    switch (this.state.candidateStatus) {
      case 'Interviewee':
        specialFields = <div>
          <TextInput
            name="interviewDate"
            label="Interview date"
            value={this.props.candidate.interviewDate}
            placeholder="dd.mm.yyyy hh:mm"
            onChange={(value) => {changeInfo('interviewDate', value)}}
            multiline={true}/>
          <div>
            <span>
              TODO: resume
            </span>
          </div>
        </div>;
        break;

      case 'Student':
        specialFields = <div>
          <TextInput
            name="groupName"
            label="Group name"
            value={this.props.candidate.groupName}
            onChange={(value) => {changeInfo('groupName', value)}}
            multiline={true}/>
          <TextInput
            name="startingDate"
            label="Learning starting date"
            value={this.props.candidate.startingDate}
            placeholder="dd.mm.yyyy"
            onChange={(value) => {changeInfo('startingDate', value)}}
            multiline={true}/>
          <TextInput
            name="endingDate"
            label="Learning edning date"
            value={this.props.candidate.endingDate}
            placeholder="dd.mm.yyyy"
            onChange={(value) => {changeInfo('endingDate', value)}}
            multiline={true}/>
        </div>;
        break;

      case 'Trainee':
        specialFields = <div>
          <TextInput
            name="mentor"
            label="Mentor's name"
            value={this.props.candidate.mentor}
            onChange={(value) => {changeInfo('mentor', value)}}
            multiline={true}/>
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
        <TextInput
          name="name"
          label="Name"
          value={this.props.candidate.name}
          onChange={(value) => {changeInfo('name', value)}}
          autoFocus={true}
          multiline={true}/>
        <TextInput
          name="birthDate"
          label="Birth date"
          value={this.props.candidate.birthDate}
          placeholder="dd.mm.yyyy"
          onChange={(value) => {changeInfo('birthDate', value)}}
          multiline={true}/>
        <TextInput
          name="email"
          label="E-mail"
          value={this.props.candidate.email}
          placeholder="example@mail.com"
          onChange={(value) => {changeInfo('email', value)}}
          multiline={true}/>

        {specialFields}
      </FormWrapper>
    );
  }
}

CandidateInfoForm.propTypes = {
  candidate: PropTypes.object.isRequired,
};

const FormWrapper = styled.div`
  padding: 15px 15px 10px;
`;