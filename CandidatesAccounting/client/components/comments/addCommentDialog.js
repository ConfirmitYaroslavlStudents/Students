import React, {Component} from 'react';
import PropTypes from 'prop-types';
import DialogWindow from '../common/UIComponentDecorators/dialogWindow';
import CommentIcon from 'material-ui-icons/InsertComment';
import CloseIcon from 'material-ui-icons/Close';
import IconButton from '../common/UIComponentDecorators/iconButton';
import AddCommentPanel from './addCommentPanel';
import styled from 'styled-components';

export default class AddCommentDialog extends Component {
  constructor(props) {
    super(props);
    this.state = ({ isOpen: false });
    this.handleOpen = this.handleOpen.bind(this);
    this.handleClose = this.handleClose.bind(this);
  }

  changeCommentText(text) {
    this.setState({commentText: text});
  }

  handleOpen() {
    this.setState({isOpen: true});
  }

  handleClose() {
    this.setState({isOpen: false});
  }

  render() {
    return (
      <div style={{display: 'inline-block'}}>
        <IconButton icon={<CommentIcon />} style={{height: 40, width: 40}} disabled={this.props.disabled} onClick={this.handleOpen}/>
        <DialogWindow
          title="Add new comment"
          isOpen={this.state.isOpen}
          onRequestClose={this.handleClose}
          controls={
            <IconButton color="inherit" icon={<CloseIcon />} onClick={this.handleClose}/>
          }>
            <FormWrapper>
              <AddCommentPanel
                addComment={this.props.addComment}
                candidate={this.props.candidate}
                onClick={this.handleClose}
                username={this.props.username}
                subscribe={this.props.subscribe}
                unsubscribe={this.props.unsubscribe}
                disabled={this.props.disabled}
              />
            </FormWrapper>
        </DialogWindow>
      </div>
    );
  }
}

AddCommentDialog.propTypes = {
  candidate: PropTypes.object.isRequired,
  addComment: PropTypes.func.isRequired,
  username: PropTypes.string.isRequired,
  subscribe: PropTypes.func.isRequired,
  unsubscribe: PropTypes.func.isRequired,
  disabled: PropTypes.bool,
};

const FormWrapper = styled.div`
  width: 500px;
`;