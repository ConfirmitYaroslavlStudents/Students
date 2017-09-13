import React from 'react';
import SendIcon from 'material-ui-icons/Send';
import TextInput from '../materialUIDecorators/textInput';
import CustomIconButton from '../materialUIDecorators/customIconButton';

export default class AddCommentForm extends React.Component{
  constructor(props) {
    super(props);
    this.state = ({ commentText: ''});
    this.changeCommentText = this.changeCommentText.bind(this);
  }

  render() {
    const setCandidateEditComment = this.props.setCandidateEditComment;
    const commentIndex = this.props.candidate.comments.length;
    let commentText = this.state.commentText;

    return (
      <div style={{width: 400, margin: '0px 0px 10px 10px'}}>
        <div style={{display: 'inline-block', width: '86%'}}>
          <TextInput
            name="comment"
            autoFocus={true}
            multiline={true}
            placeholder="a quick note"
            onChange={this.changeCommentText}
          />
        </div>
        <div className="send-button" style={{display: 'inline-block', width: '14%', float: 'right'}}>
          <CustomIconButton icon={<SendIcon/>} onClick={() => setCandidateEditComment(commentIndex, commentText)}/>
        </div>
      </div>
    );
  }

  changeCommentText(text) {
    this.setState({commentText: text});
  }
}