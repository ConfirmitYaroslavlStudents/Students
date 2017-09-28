import React from 'react';
import PropTypes from 'prop-types';
import TextInput from '../materialUIDecorators/textInput';
import { Comment } from '../candidates/index';
import styled from 'styled-components';

export default class AddCommentForm extends React.Component{
  constructor(props) {
    super(props);
    this.changeCommentText = this.changeCommentText.bind(this);
  }

  render() {
    return (
      <FormWrapper>
        <TextInput
          name="comment"
          autoFocus={true}
          multiline={true}
          placeholder="New comment"
          onChange={this.changeCommentText}
        />
      </FormWrapper>
    );
  }

  changeCommentText(text) {
    let date = new Date();
    this.props.setTempCandidateComment(this.props.commentIndex, new Comment(
      'Вы',
      date.getHours() + ':' + date.getMinutes() + ' ' + date.getDate() + '.' + (date.getMonth() + 1) + '.' + date.getFullYear(),
      text));
  }
}

AddCommentForm.propTypes = {
  commentIndex: PropTypes.number.isRequired,
  setTempCandidateComment: PropTypes.func.isRequired,
};

const FormWrapper = styled.div`
  width: 500px;
  margin: 0px 10px 10px;
`;