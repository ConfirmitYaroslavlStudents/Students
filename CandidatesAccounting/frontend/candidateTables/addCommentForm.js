import React from 'react';
import TextInput from '../materialUIDecorators/textInput';
import { Comment } from '../candidates/index';

export default class AddCommentForm extends React.Component{
  constructor(props) {
    super(props);
    this.changeCommentText = this.changeCommentText.bind(this);
  }

  render() {

    return (
      <div style={{width: 400, margin: '0px 10px 10px 10px'}}>
        <div style={{display: 'inline-block', width: '100%'}}>
          <TextInput
            name="comment"
            autoFocus={true}
            multiline={true}
            placeholder="a quick note"
            onChange={this.changeCommentText}
          />
        </div>
      </div>
    );
  }

  changeCommentText(text) {
    this.props.setCandidateEditComment(this.props.commentIndex, new Comment('Вы', '01.01.1900', text));
  }
}