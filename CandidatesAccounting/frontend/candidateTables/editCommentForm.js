import React from 'react';
import Avatar from 'material-ui-icons/AccountCircle';
import TextInput from '../materialUIDecorators/textInput';
import IconButton from '../materialUIDecorators/customIconButton';
import AddIcon from 'material-ui-icons/Add';
import {Comment} from '../candidates';

export default class EditCommentForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = ({newCommentText: ''});
    this.changeNewCommentText = this.changeNewCommentText.bind(this);
    this.addNewComment = this.addNewComment.bind(this);
  }
  render() {
    let comments = this.props.candidate.comments.map((comment, index) =>
      comment.author.toLowerCase() === 'вы' ?
        <div key={'comment' + index} className={'comment user-comment'}>
          <Avatar />
          <p style={{marginBottom: 0}}>{comment.author} <span className="comment-date">{comment.date}</span>
            <span> [Edit | Delete]</span></p>
          <div style={{wordWrap: 'break-word'}}>{comment.text}</div>
        </div>
        :
        <div key={'comment' + index} className={'comment'}>
          <Avatar />
          <p style={{marginBottom: 0}}>{comment.author} <span className="comment-date">{comment.date}</span></p>
          <div style={{wordWrap: 'break-word'}}>{comment.text}</div>
        </div>
    );
    return (
      <div style={{display: 'inline-block', width: 400, padding: '20px 0'}}>

        {comments}

        <div className="comment-text-input">
          <TextInput
            name="comment"
            placeholder="New comment"
            value={this.state.newCommentText}
            autoFocus={true}
            multiline={true}
            onChange={this.changeNewCommentText}
          />
        </div>
        <div className="add-comment-btn">
          <IconButton
            icon={<AddIcon />}
            onClick = {this.addNewComment}
          />
        </div>
      </div>
    );
  }

  changeNewCommentText(text) {
    this.setState({newCommentText: text})
  }

  addNewComment() {
    if (this.state.newCommentText.trim() !== '') {
      let date = new Date();
      let candidate = this.props.candidate;
      candidate.comments.push(new Comment(
        'Вы',
        date.getHours() + ':' + date.getMinutes() + ' ' + date.getDate() + '.' + (date.getMonth() + 1) + '.' + date.getFullYear(),
        this.state.newCommentText));
      this.props.editCandidate(this.props.candidate.id, candidate);
      this.setState({newCommentText: ''})
    }
  }
}