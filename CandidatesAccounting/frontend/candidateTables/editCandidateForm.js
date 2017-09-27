import React from 'react';
import PropTypes from 'prop-types';
import TextInput from '../materialUIDecorators/textInput';
import SimpleSelect from '../materialUIDecorators/simpleSelect';
import EditCommentForm from './editCommentForm';
import EditIcon from 'material-ui-icons/ViewList';
import DialogWindow from '../materialUIDecorators/dialogWindow';
import Badge from '../materialUIDecorators/badge';
import styled from 'styled-components';

export default class EditCandidateForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = ({candidateType: props.tempCandidate.status});
    this.changeInfo = this.changeInfo.bind(this);
    this.changeCandidateType = this.changeCandidateType.bind(this);
  }

  render() {
    const changeInfo = this.changeInfo;
    const changeCandidateType = this.changeCandidateType;
    let specialFields;
    switch (this.state.candidateType) {
      case 'Interviewee':
        specialFields = <div>
          <TextInput
            name="interviewDate"
            label="Interview date"
            value={this.props.tempCandidate.interviewDate}
            placeholder="dd.mm.yyyy hh:mm"
            onChange={function(value) {changeInfo('interviewDate', value)}}/>
          <TextInput
            name="interviewRoom"
            label="Interview room"
            value={this.props.tempCandidate.interviewRoom}
            placeholder="interview placement"
            onChange={function(value) {changeInfo('interviewRoom', value)}}/>
        </div>;
        break;

      case 'Student':
        specialFields = <div>
          <TextInput
            name="groupName"
            label="Group name"
            value={this.props.tempCandidate.groupName}
            onChange={function(value) {changeInfo('groupName', value)}}/>
        </div>;
        break;

      case 'Trainee':
        specialFields = <div>
          <TextInput
            name="mentor"
            label="Mentor's name"
            value={this.props.tempCandidate.mentor}
            onChange={function(value) {changeInfo('mentor', value)}}/>
        </div>;
        break;
    }

    return (
      <FormWrapper>

        <SimpleSelect
          label="Candidate's status"
          options={['Interviewee', 'Student', 'Trainee']}
          selected={this.state.candidateType}
          onChange={changeCandidateType}
        />
        <TextInput
          name="name"
          label="Name"
          value={this.props.tempCandidate.name}
          onChange={function(value) {changeInfo('name', value)}}
          autoFocus={true}/>
        <TextInput
          name="birthDate"
          label="Birth date"
          value={this.props.tempCandidate.birthDate}
          placeholder="dd.mm.yyyy"
          onChange={function(value) {changeInfo('birthDate', value)}}/>
        <TextInput
          name="email"
          label="E-mail"
          value={this.props.tempCandidate.email}
          placeholder="example@mail.com"
          onChange={function(value) {changeInfo('email', value)}}/>

        {specialFields}

        {this.props.additionMode ?
          ''
          :
          <div className="float-right">
            <Badge badgeContent={this.props.tempCandidate.comments.length} badgeStyle="comment-badge">
              <DialogWindow
                content={
                  <EditCommentForm
                    candidate={this.props.tempCandidate}
                    setTempCandidateComment={this.props.setTempCandidateComment}
                    editCandidate={this.props.editCandidate}
                  />}
                label="Comments"
                openButtonType="icon"
                openButtonContent={<EditIcon/>}
                />
            </Badge>
          </div>
        }
      </FormWrapper>
    );
  }

  changeInfo(key, value) {
    this.props.changeTempCandidateInfo(key, value);
  }

  changeCandidateType(type) {
    this.setState({ candidateType: type });
    this.props.changeTempCandidateInfo('status', type);
  }
}

EditCandidateForm.propTypes = {
  tempCandidate: PropTypes.object.isRequired,
  setTempCandidateComment: PropTypes.func.isRequired,
  changeTempCandidateInfo: PropTypes.func.isRequired,
  editCandidate: PropTypes.func.isRequired,
  additionMode: PropTypes.bool,
};

const FormWrapper = styled.div`
  padding: 15px;
`;