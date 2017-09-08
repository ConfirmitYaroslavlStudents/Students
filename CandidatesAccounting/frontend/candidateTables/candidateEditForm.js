import React from 'react';
import TextInput from '../materialUIDecorators/textInput';
import SelectMenu from '../materialUIDecorators/selectMenu';
import {Comment} from '../candidates';
import CommentsEditDialog from './commentsEditDialog';

export default class CandidateEditForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = ({candidateType: props.candidateEditInfo.status});
    this.changeEditInfo = this.changeEditInfo.bind(this);
    this.changeCandidateType = this.changeCandidateType.bind(this);
    this.setCandidateComment = this.setCandidateComment.bind(this);
  }

  render() {
    const changeEditInfo = this.changeEditInfo;
    const changeCandidateType = this.changeCandidateType;
    const setCandidateComment = this.setCandidateComment;
    let specialFields;
    switch (this.state.candidateType) {
      case 'Interviewee':
        specialFields = <div>
          <TextInput
            name="interviewDate"
            label="Interview date"
            value={this.props.candidateEditInfo.interviewDate}
            placeholder="dd.mm.yyyy hh:mm"
            onChange={function(value) {changeEditInfo('interviewDate', value)}}/>
          <TextInput
            name="interviewRoom"
            label="Interview room"
            value={this.props.candidateEditInfo.interviewRoom}
            placeholder="interview placement"
            onChange={function(value) {changeEditInfo('interviewRoom', value)}}/>
        </div>;
        break;
      case 'Student':
        specialFields = <div>
          <TextInput
            name="groupName"
            label="Group name"
            value={this.props.candidateEditInfo.groupName}
            onChange={function(value) {changeEditInfo('groupName', value)}}/>
        </div>;
        break;
      case 'Trainee':
        specialFields = <div>
          <TextInput
            name="mentor"
            label="Mentor's name"
            value={this.props.candidateEditInfo.mentor}
            onChange={function(value) {changeEditInfo('mentor', value)}}/>
        </div>;
        break;
    }

    return (
      <div className="candidate-edit-form">
        <SelectMenu
          label="Candidate's status"
          options={['Interviewee', 'Student', 'Trainee']}
          selectedOption={this.state.candidateType}
          onChange={function(status) { changeCandidateType(status)}}
        />
        <TextInput
          name="name"
          label="Name"
          value={this.props.candidateEditInfo.name}
          onChange={function(value) {changeEditInfo('name', value)}}/>
        <TextInput
          name="birthDate"
          label="Birth date"
          value={this.props.candidateEditInfo.birthDate}
          placeholder="dd.mm.yyyy"
          onChange={function(value) {changeEditInfo('birthDate', value)}}/>
        <TextInput
          name="email"
          label="E-mail"
          value={this.props.candidateEditInfo.email}
          placeholder="example@mail.com"
          onChange={function(value) {changeEditInfo('email', value)}}/>

        {specialFields}

        <div className="float-right">
          {this.props.candidateEditInfo.comments ? this.props.candidateEditInfo.comments.length + ' comment(s)' : 'no comments'}
          <CommentsEditDialog/>
        </div>
      </div>
    );
  }

  changeEditInfo(key, value) {
    this.props.changeEditInfo(key, value);
  }

  changeCandidateType(type) {
    this.setState({candidateType: type});
    this.props.changeEditInfo('status', type);
  }

  setCandidateComment(index, comment) {
    this.props.setCandidateComment(index, new Comment(' a', 'd', comment));
  }
}