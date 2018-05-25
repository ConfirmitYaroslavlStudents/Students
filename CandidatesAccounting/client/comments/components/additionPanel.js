import 'react-quill/dist/quill.snow.css'
import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { BigButtonStyle, SmallIconStyle, QuillToolbarButtonStyle } from '../../common/styleObjects'
import IconButton from '../../common/UIComponentDecorators/iconButton'
import AttachIcon from '@material-ui/icons/AttachFile'
import AddIcon from '@material-ui/icons/AddCircleOutline'
import ReactQuill from 'react-quill'
import Comment from '../../utilities/comment'
import SubscribeButton from './subscribeButton'
import FileUploader from '../../common/fileUploader'
import styled from 'styled-components'

export default class AddCommentPanel extends Component {
  constructor(props) {
    super(props);
    this.state = ({ commentText: '', commentAttachment: null })
  }

  handleChange = (commentText) => {
    this.setState({commentText})
  }

  handleAttachFile = (commentAttachment) => {
    this.setState({ commentAttachment })
  }

  handleEnterPress = (event) => {
    if (event.keyCode === 13) {
      if (!event.shiftKey) {
        event.preventDefault()
        this.handleCommentAdd()
      }
    }
  }

  handleCommentAdd = () => {
    const {addComment, username, onCommentAdd, candidate, disabled } = this.props

    if (disabled) {
      return
    }

    let commentText = this.state.commentText
    if (commentText.replace(/<[^>]+>/g,'').trim() !== '' || this.state.commentAttachment) {
      if (commentText.slice(-11) === '<p><br></p>') {
        commentText = commentText.substr(0, commentText.length - 11)
      }
      addComment({
        candidateId: candidate.id,
        comment: new Comment(
          username,
          commentText,
          this.state.commentAttachment ? this.state.commentAttachment.name : ''
        ),
        commentAttachment: this.state.commentAttachment
      })
      onCommentAdd()
      this.setState({ commentText: '', commentAttachment: null })
    }
  }

  customQuillToolbar = () => {
    return (
      <div id='toolbar' className='flex centered'>
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
        <button className='ql-link'/>
        <FileUploader
          uploadFile={this.handleAttachFile}
          icon={<AttachIcon style={QuillToolbarButtonStyle}/>}
          attachment={this.state.commentAttachment}
          disabled={this.props.disabled} />
      </div>
    )
  }

  render() {
    const { disabled, candidate, username, subscribe, unsubscribe } = this.props

    return (
      <AddCommentPanelWrapper>
        <QuillWrapper>
          { this.customQuillToolbar() }
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
              active={!disabled && !!candidate.subscribers[username]}
              subscribe={() => { subscribe({ candidateId: candidate.id })}}
              unsubscribe={() => { unsubscribe({ candidateId: candidate.id })}}
              disabled={disabled}
            />
          </SubscribeButtonWrapper>
          <AddCommentButtonWrapper>
            <IconButton
              icon={<AddIcon style={SmallIconStyle}/>}
              disabled={disabled}
              onClick={this.handleCommentAdd}
              style={BigButtonStyle}
            />
          </AddCommentButtonWrapper>
        </AddCommentPanelButtonsWrapper>
      </AddCommentPanelWrapper>
    )
  }
}

AddCommentPanel.propTypes = {
  username: PropTypes.string.isRequired,
  candidate: PropTypes.object.isRequired,
  addComment: PropTypes.func.isRequired,
  subscribe: PropTypes.func.isRequired,
  unsubscribe: PropTypes.func.isRequired,
  onCommentAdd: PropTypes.func,
  disabled: PropTypes.bool,
}

AddCommentPanel.modules = {
  toolbar: {
    container: '#toolbar',
  }
}

const AddCommentPanelButtonsWrapper = styled.div`
  display: inline-flex;
  flex-direction: column;
  align-items: center;
  position: absolute;
  height: 100%;
  right: 2px;
  bottom: 2px;
 `

const AddCommentPanelWrapper = styled.div`
  box-shadow: 0 0 15px rgba(0, 0, 0, 0.2);
  position: relative;
  width: 100%;
  clear: both;
  background: #FFF;
`

const AddCommentButtonWrapper = styled.div`
  display: inline-block;
  position: absolute;
  bottom: 0px;
  right: 5px;
`

const QuillWrapper = styled.div`
  padding-right: 74px;
`

const SubscribeButtonWrapper = styled.div`
  display: inline-block;
  position: relative;
  top: 11px;
  right: 11px;
`