import React from 'react';
import DialogWindow from '../materialUIDecorators/dialogWindow';
import AddIcon from 'material-ui-icons/Add';
import TextInput from '../materialUIDecorators/textInput';
import IconButton from 'material-ui/IconButton';

export default function AddCommentDialog(props) {
  return (
    <DialogWindow
      content={
        <div style={{margin: '0px 10px 10px 10px'}}>
          <div style={{display:'inline-block', width: '86%'}}>
            <TextInput
              name="comment"
              autoFocus={true}
              placeholder="a quick note"
            />
          </div>
          <IconButton><AddIcon /></IconButton>
        </div>
        }
      label="Add new comment"
      openButtonType="icon"
      openButtonContent={<AddIcon />}
      open={function() {
      }}
      save={function() {
      }}
      close={function() {
      }}
    />
  );
}