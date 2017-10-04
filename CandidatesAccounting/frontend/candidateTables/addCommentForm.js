import React from 'react';
import PropTypes from 'prop-types';
import TextInput from '../materialUIDecorators/textInput';
import { Comment } from '../candidates/index';
import styled from 'styled-components';
import moment from 'moment';

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
          value=""
        />
      </FormWrapper>
    );
  }

  changeCommentText(text) {
    this.props.setTempCandidateComment(this.props.commentIndex, new Comment(
      'Вы',
      moment().format('H:MM:SS DD MMMM YYYY'),
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