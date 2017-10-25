import React from 'react';
import PropTypes from 'prop-types';
import moment from 'moment';
import IconButton from '../materialUIDecorators/iconButton';
import AddIcon from 'material-ui-icons/AddCircleOutline';
import styled from 'styled-components';
import ReactQuill from 'react-quill';
import { Comment } from '../candidatesClasses';

export default class AddCommentPanel extends React.Component {
  constructor(props) {
    super(props);
    this.state = ({commentText: ''});
  }

  handleChange(text) {
    this.setState({commentText: text});
  }

  addNewComment() {
    let commentText = this.state.commentText;
    if (commentText.replace(/<[^>]+>/g,'').trim() !== '') {
      if (commentText.slice(-11) === '<p><br></p>') {
        commentText = commentText.substr(0, commentText.length - 11);
      }
      this.props.addComment(this.props.candidateId, new Comment(
        'Вы',
        moment().format('H:MM:SS DD MMMM YYYY'),
        commentText));
      this.props.onClick();
      this.setState({commentText: ''});
    }
  }

  render() {
    const addNewComment = this.addNewComment.bind(this);
    return (
      <AddCommentWrapper>
        <CommentTextInput>
          <ReactQuill
            value={this.state.commentText}
            onChange={this.handleChange.bind(this)}
            placeholder="New comment"
            tabIndex={1}
            onKeyDown={
              function (event) {
                if (event.keyCode === 13) {
                  if (!event.shiftKey) {
                    event.preventDefault();
                    addNewComment();
                  }
                }
              }
            }
          >
            <CommentTextEdit/>
          </ReactQuill>

        </CommentTextInput>
        <ButtonWrapper>
          <IconButton
            icon={<AddIcon/>}
            onClick={this.addNewComment.bind(this)}
          />
        </ButtonWrapper>
      </AddCommentWrapper>
    );
  }
}

AddCommentPanel.PropTypes = {
  addComment: PropTypes.func.isRequired,
  candidateId: PropTypes.number.isRequired,
  onClick: PropTypes.func,
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

const CommentTextEdit = styled.div`
  tabindex: 1;
  min-height: 120px;
  font-size: 96%;
`;