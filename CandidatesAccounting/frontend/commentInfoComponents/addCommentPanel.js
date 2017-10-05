import React from 'react';
import PropTypes from 'prop-types';
import IconButton from '../materialUIDecorators/iconButton';
import AddIcon from 'material-ui-icons/AddCircleOutline';
import styled from 'styled-components';
import ReactQuill from 'react-quill';

export default function AddCommentPanel(props) {
  return (
    <AddCommentWrapper>
      <CommentTextInput>
        <ReactQuill
          value={props.value}
          onChange={props.onChange}
          placeholder="New comment"
          tabIndex={1}
          onKeyDown={
            function(event) {
              if(event.keyCode === 13) {
                if(!event.shiftKey) {
                  event.preventDefault();
                  props.onClick(event);
                }
              }
            }
          }
        >
          <div autoFocus={true}  tabIndex={1}> </div>
        </ReactQuill>

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
  right: 5px;
  bottom: 5px;
 `;

const AddCommentWrapper = styled.div`
  box-shadow: 0 0 15px rgba(0, 0, 0, 0.2);
  position: relative;
  width: 100%;
  clear: both;
  background: #FFF;
`;

const CommentTextInput = styled.div`
  padding-right: 58px;
`;