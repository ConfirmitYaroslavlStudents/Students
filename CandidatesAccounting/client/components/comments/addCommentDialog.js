import React from 'react';
import PropTypes from 'prop-types';
import DialogWindow from '../common/UIComponentDecorators/dialogWindow';
import CommentIcon from 'material-ui-icons/InsertComment';
import CloseIcon from 'material-ui-icons/Close';
import IconButton from '../common/UIComponentDecorators/iconButton';
import AddCommentPanel from './addCommentPanel';
import styled from 'styled-components';

export default class AddCommentDialog extends React.Component {
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
        <IconButton icon={<CommentIcon />} style={{height: 40, width: 40}} onClick={this.handleOpen}/>
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
                candidateId={this.props.candidate.id}
                onClick={this.handleClose}
                userName={this.props.userName}
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
  userName: PropTypes.string.isRequired,
};

const FormWrapper = styled.div`
  width: 500px;
`;