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
            placeholder="New comment"
            onChange={this.changeCommentText}
          />
        </div>
      </div>
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