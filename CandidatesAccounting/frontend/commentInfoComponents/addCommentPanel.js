import React from 'react';
import PropTypes from 'prop-types';
import TextInput from '../materialUIDecorators/textInput';
import IconButton from '../materialUIDecorators/iconButton';
import AddIcon from 'material-ui-icons/Add';
import styled from 'styled-components';

export default function AddCommentPanel(props) {
  return (
    <AddCommentWrapper>
      <CommentTextInput>
        <TextInput
          name="comment"
          placeholder="New comment"
          value={props.value}
          autoFocus={true}
          multiline={true}
          onChange={props.onChange}
        />
      </CommentTextInput>
      <ButtonWrapper>
        <IconButton
          icon={<AddIcon />}
          onClick = {props.onClick}
        />
      </ButtonWrapper>
    </AddCommentWrapper>
  );
}

AddCommentPanel.PropTypes = {
  value: PropTypes.string.isRequired,
  onChange: PropTypes.func.isRequired,
  onClick: PropTypes.func.isRequired,
};

const ButtonWrapper = styled.div`
  display: inline-block;
  position: absolute;
  right: 10px;
  bottom: 10px;
 `;

const AddCommentWrapper = styled.div`
  box-shadow: 0 0 15px rgba(0, 0, 0, 0.2);
  position: relative;
  width: 100%;
  padding-bottom: 10px;
  clear: both;
  background: #FFF;
`;

const CommentTextInput = styled.div`
  padding-left: 20px;
  padding-right: 56px;
`;