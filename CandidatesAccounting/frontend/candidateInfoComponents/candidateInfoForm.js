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

  render() {
    const props = this.props;
    const changeInfo = this.changeInfo.bind(this);
    const changeCandidateType = this.changeCandidateStatus.bind(this);
    let specialFields;
    switch (this.state.candidateStatus) {
      case 'Interviewee':
        specialFields = <div>
          <TextInput
            name="interviewDate"
            label="Interview date"
            value={props.candidate.interviewDate}
            placeholder="dd.mm.yyyy hh:mm"
            onChange={function(value) {changeInfo('interviewDate', value)}}
            multiline={true}/>
          <TextInput
            name="interviewRoom"
            label="Interview room"
            value={props.candidate.interviewRoom}
            placeholder="interview placement"
            onChange={function(value) {changeInfo('interviewRoom', value)}}
            multiline={true}/>
        </div>;
        break;

      case 'Student':
        specialFields = <div>
          <TextInput
            name="groupName"
            label="Group name"
            value={props.candidate.groupName}
            onChange={function(value) {changeInfo('groupName', value)}}
            multiline={true}/>
        </div>;
        break;

      case 'Trainee':
        specialFields = <div>
          <TextInput
            name="mentor"
            label="Mentor's name"
            value={props.candidate.mentor}
            onChange={function(value) {changeInfo('mentor', value)}}
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
          onChange={changeCandidateType}
        />
        <TextInput
          name="name"
          label="Name"
          value={props.candidate.name}
          onChange={function(value) {changeInfo('name', value)}}
          autoFocus={true}
          multiline={true}/>
        <TextInput
          name="birthDate"
          label="Birth date"
          value={props.candidate.birthDate}
          placeholder="dd.mm.yyyy"
          onChange={function(value) {changeInfo('birthDate', value)}}
          multiline={true}/>
        <TextInput
          name="email"
          label="E-mail"
          value={props.candidate.email}
          placeholder="example@mail.com"
          onChange={function(value) {changeInfo('email', value)}}
          multiline={true}/>

        {specialFields}
      </FormWrapper>
    );
  }

  changeInfo(key, value) {
    this.props.candidate[key] = value;
  }

  changeCandidateStatus(status) {
    this.setState({ candidateStatus: status });
    this.props.candidate.status = status;
    this.props.candidate['interviewDate'] = '';
    this.props.candidate['interviewRoom'] = '';
    this.props.candidate['groupName'] = '';
    this.props.candidate['mentor'] = '';
  }
}

CandidateInfoForm.propTypes = {
  candidate: PropTypes.object.isRequired,
};

const FormWrapper = styled.div`
  padding: 15px 15px 10px;
`;