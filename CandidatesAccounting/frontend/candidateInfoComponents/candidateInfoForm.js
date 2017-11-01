import React from 'react';
import PropTypes from 'prop-types';
import TextInput from '../materialUIDecorators/textInput';
import SimpleSelect from '../materialUIDecorators/simpleSelect';
import styled from 'styled-components';
import TagSelect from '../materialUIDecorators/tagSelect';

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
          <TextInput
            name="interviewDate"
            label="Interview date"
            value={this.props.candidate.interviewDate}
            placeholder="dd.mm.yyyy hh:mm"
            onChange={(value) => {changeInfo('interviewDate', value)}}
            multiline={true}/>
          <div>
            <p>Resume</p>
            <span>resume.pdf </span>
            <button>
              view
            </button>
            <button>
              upload
            </button>
            <button>
              download
            </button>
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
            multiline/>
          <TextInput
            name="startingDate"
            label="Learning starting date"
            value={this.props.candidate.startingDate}
            placeholder="dd.mm.yyyy"
            onChange={(value) => {changeInfo('startingDate', value)}}
            multiline/>
          <TextInput
            name="endingDate"
            label="Learning edning date"
            value={this.props.candidate.endingDate}
            placeholder="dd.mm.yyyy"
            onChange={(value) => {changeInfo('endingDate', value)}}
            multiline/>
        </div>;
        break;

      case 'Trainee':
        specialFields = <div>
          <TextInput
            name="mentor"
            label="Mentor's name"
            value={this.props.candidate.mentor}
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
          label="Name"
          value={this.props.candidate.name}
          onChange={(value) => {changeInfo('name', value)}}
          multiline/>
        <TextInput
          name="birthDate"
          label="Birth date"
          value={this.props.candidate.birthDate}
          placeholder="dd.mm.yyyy"
          onChange={(value) => {changeInfo('birthDate', value)}}
          multiline/>
        <TextInput
          name="email"
          label="E-mail"
          value={this.props.candidate.email}
          placeholder="example@mail.com"
          onChange={(value) => {changeInfo('email', value)}}
          multiline/>

        {specialFields}
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
  color: rgba(0,0,0,0.54);
  font-size: 80%;
`;