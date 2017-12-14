import React from 'react';
import PropTypes from 'prop-types';
import {getCurrentDateTime} from '../../utilities/customMoment';
import IconButton from '../common/UIComponentDecorators/iconButton';
import AddIcon from 'material-ui-icons/AddCircleOutline';
import styled from 'styled-components';
import ReactQuill from 'react-quill';
import {Comment} from '../../databaseDocumentClasses';

export default class AddCommentPanel extends React.Component {
  constructor(props) {
    super(props);
    this.state = ({commentText: ''});
    this.handleChange = this.handleChange.bind(this);
    this.addNewComment = this.addNewComment.bind(this);
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
      this.props.addComment(this.props.candidateID, new Comment(
        this.props.userName,
        getCurrentDateTime(),
        commentText));
      this.props.onClick();
      this.setState({commentText: ''});
    }
  }

  render() {
    return (
      <AddCommentWrapper>
        <CommentTextInput>
          <ReactQuill
            value={this.state.commentText}
            onChange={this.handleChange}
            placeholder="New comment"
            tabIndex={1}
            onKeyDown={
              (event) => {
                if (event.keyCode === 13) {
                  if (!event.shiftKey) {
                    event.preventDefault();
                    this.addNewComment();
                  }
                }
              }
            }
          />
        </CommentTextInput>
        <ButtonWrapper>
          <IconButton
            icon={<AddIcon/>}
            onClick={this.addNewComment}
            style={{width: 70, height: 70}}
          />
        </ButtonWrapper>
      </AddCommentWrapper>
    );
  }
}

AddCommentPanel.propTypes = {
  addComment: PropTypes.func.isRequired,
  candidateID: PropTypes.string.isRequired,
  userName: PropTypes.string.isRequired,
  onClick: PropTypes.func,
};

const ButtonWrapper = styled.div`
  display: inline-block;
  position: absolute;
  right: 2px;
  bottom: 2px;
 `;

const AddCommentWrapper = styled.div`
  box-shadow: 0 0 15px rgba(0, 0, 0, 0.2);
  position: relative;
  width: 100%;
  clear: both;
  background: #FFF;
`;

const CommentTextInput = styled.div`
  padding-right: 74px;
`;