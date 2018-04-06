import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { BigButtonStyle, SmallestIconStyle, SmallIconStyle } from '../common/styleObjects'
import IconButton from '../common/UIComponentDecorators/iconButton'
import AttachIcon from 'material-ui-icons/AttachFile'
import AddIcon from 'material-ui-icons/AddCircleOutline'
import ReactQuill from 'react-quill'
import Comment from '../../utilities/comment'
import SubscribeButton from './subscribeButton'
import FileUploader from '../common/fileUploader'
import {
  CenteredDiv,
  AddCommentPanelWrapper,
  QuillWrapper,
  AddCommentPanelButtonsWrapper,
  SubscribeButtonWrapper,
  AddCommentButtonWrapper
} from '../common/styledComponents'

export default class AddCommentPanel extends Component {
  constructor(props) {
    super(props);
    this.state = ({ commentText: '', commentAttachment: null })
  }

  handleChange = (text) => {
    this.setState({ commentText: text })
  }

  handleAttachFile = (file) => {
    this.setState({ commentAttachment: file })
  }

  createCustomQuillToolbar = () => {
    return (
      <CenteredDiv id='toolbar'>
        <select defaultValue='' className='ql-size'>
          <option value='small' />
          <option value='' />
          <option value='large' />
          <option value='huge' />
        </select>
        <button className='ql-bold' />
        <button className='ql-italic' />
        <button className='ql-underline' />
        <button className='ql-strike' />
        <button className='ql-list' value='ordered' />
        <button className='ql-list' value='bullet' />
        <button className='ql-indent' value='-1' />
        <button className='ql-indent' value='+1' />
        <button className='ql-link' />
        <FileUploader
          uploadFile={this.handleAttachFile}
          icon={<AttachIcon style={SmallestIconStyle} />}
          attachment={this.state.commentAttachment}
          disabled={this.props.disabled} />
      </CenteredDiv>
    )
  }

  handleEnterPress = (event) => {
    if (event.keyCode === 13) {
      if (!event.shiftKey) {
        event.preventDefault()
        this.addNewComment()
      }
    }
  }

  addNewComment = () => {
    let commentText = this.state.commentText
    if (commentText.replace(/<[^>]+>/g,'').trim() !== '' || this.state.commentAttachment) {
      if (commentText.slice(-11) === '<p><br></p>') {
        commentText = commentText.substr(0, commentText.length - 11)
      }
      this.props.addComment(this.props.candidate.id, new Comment(
        this.props.username,
        commentText,
        this.state.commentAttachment ? this.state.commentAttachment.name : ''
        ), this.state.commentAttachment)
      this.props.onClick()
      this.setState({ commentText: '', commentAttachment: null })
    }
  }

  render() {
    return (
      <AddCommentPanelWrapper>
        <QuillWrapper>
          {this.createCustomQuillToolbar()}
          <ReactQuill
            value={this.state.commentText}
            onChange={this.handleChange}
            placeholder='New comment'
            tabIndex={1}
            modules={AddCommentPanel.modules}
            onKeyDown={this.handleEnterPress}
          />
        </QuillWrapper>
        <AddCommentPanelButtonsWrapper>
          <SubscribeButtonWrapper>
            <SubscribeButton
              active={!this.props.disabled && !!this.props.candidate.subscribers[this.props.username]}
              candidate={this.props.candidate}
              username={this.props.username}
              subscribe={this.props.subscribe}
              unsubscribe={this.props.unsubscribe}
              disabled={this.props.disabled}
            />
          </SubscribeButtonWrapper>
          <AddCommentButtonWrapper>
            <IconButton
              icon={<AddIcon style={SmallIconStyle}/>}
              disabled={this.props.disabled}
              onClick={this.addNewComment}
              style={BigButtonStyle}
            />
          </AddCommentButtonWrapper>
        </AddCommentPanelButtonsWrapper>
      </AddCommentPanelWrapper>
    )
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
}

AddCommentPanel.modules = {
  toolbar: {
    container: '#toolbar',
  }
}