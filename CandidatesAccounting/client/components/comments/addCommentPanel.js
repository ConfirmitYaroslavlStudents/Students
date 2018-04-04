import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { getCurrentDateTime } from '../../utilities/customMoment';
import IconButton from '../common/UIComponentDecorators/iconButton';
import AttachIcon from 'material-ui-icons/AttachFile';
import AddIcon from 'material-ui-icons/AddCircleOutline';
import styled from 'styled-components';
import ReactQuill from 'react-quill';
import createComment from '../../utilities/createComment';
import SubscribeButton from './subscribeButton';
import FileUploader from '../common/fileUploader';

export default class AddCommentPanel extends Component {
  constructor(props) {
    super(props);
    this.state = ({commentText: '', commentAttachment: null});
    this.handleChange = this.handleChange.bind(this);
    this.handleAttachFile = this.handleAttachFile.bind(this);
    this.addNewComment = this.addNewComment.bind(this);
    this.createCustomQuillToolbar = this.createCustomQuillToolbar.bind(this)
  }

  handleChange(text) {
    this.setState({commentText: text});
  }

  handleAttachFile(file) {
    this.setState({commentAttachment: file});
  }

  createCustomQuillToolbar() {
    return (
      <div id="toolbar" style={{display: 'flex', alignItems: 'center'}}>
        <select defaultValue={''} className="ql-size">
          <option value='small' />
          <option value='' />
          <option value='large' />
          <option value='huge' />
        </select>
        <button className="ql-bold" />
        <button className="ql-italic" />
        <button className="ql-underline" />
        <button className="ql-strike" />
        <button className="ql-list" value="ordered" />
        <button className="ql-list" value="bullet" />
        <button className="ql-indent" value="-1" />
        <button className="ql-indent" value="+1" />
        <button className="ql-link" />
        <FileUploader
          uploadFile={this.handleAttachFile}
          icon={<AttachIcon style={{width: 20, height: 20}}/>}
          attachment={this.state.commentAttachment}
          disabled={this.props.disabled}/>
      </div>
    )
  }

  addNewComment() {
    let commentText = this.state.commentText;
    if (commentText.replace(/<[^>]+>/g,'').trim() !== '' || this.state.commentAttachment) {
      if (commentText.slice(-11) === '<p><br></p>') {
        commentText = commentText.substr(0, commentText.length - 11);
      }
      this.props.addComment(this.props.candidate.id, createComment(
        this.props.username,
        getCurrentDateTime(),
        commentText,
        this.state.commentAttachment ? this.state.commentAttachment.name : ''
        ), this.state.commentAttachment);
      this.props.onClick();
      this.setState({commentText: '', commentAttachment: null});
    }
  }

  render() {
    return (
      <AddCommentWrapper>
        <CommentTextInput>
          {this.createCustomQuillToolbar()}
          <ReactQuill
            value={this.state.commentText}
            onChange={this.handleChange}
            placeholder="New comment"
            tabIndex={1}
            modules={AddCommentPanel.modules}
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
          <NotificationsWrapper>
            <SubscribeButton
              active={!this.props.disabled && !!this.props.candidate.subscribers[this.props.username]}
              candidate={this.props.candidate}
              username={this.props.username}
              subscribe={this.props.subscribe}
              unsubscribe={this.props.unsubscribe}
              disabled={this.props.disabled}
            />
          </NotificationsWrapper>
          <AddButtonWrapper>
            <IconButton
              icon={<AddIcon style={{width: 28, height: 28}}/>}
              iconStyle="big-icon"
              disabled={this.props.disabled}
              onClick={this.addNewComment}
              style={{width: 60, height: 60}}
            />
          </AddButtonWrapper>
        </ButtonWrapper>
      </AddCommentWrapper>
    );
  }
}

AddCommentPanel.propTypes = {
  addComment: PropTypes.func.isRequired,
  candidate: PropTypes.object.isRequired,
  username: PropTypes.string.isRequired,
  subscribe: PropTypes.func.isRequired,
  unsubscribe: PropTypes.func.isRequired,
  onClick: PropTypes.func,
  disabled: PropTypes.bool,
};

AddCommentPanel.modules = {
  toolbar: {
    container: "#toolbar",
  }
}

const AddCommentWrapper = styled.div`
  box-shadow: 0 0 15px rgba(0, 0, 0, 0.2);
  position: relative;
  width: 100%;
  clear: both;
  background: #FFF;
`;

const ButtonWrapper = styled.div`
  display: inline-flex;
  flex-direction: column;
  align-items: center;
  position: absolute;
  height: 100%;
  right: 2px;
  bottom: 2px;
 `;

const NotificationsWrapper = styled.div`
  display: inline-block;
  position: relative;
  top: 11px;
  right: 11px;
`;

const AddButtonWrapper = styled.div`
  display: inline-block;
  position: absolute;
  bottom: 0px;
  right: 5px;
`;

const CommentTextInput = styled.div`
  padding-right: 74px;
`;