import React from 'react';
import PropTypes from 'prop-types';
import DialogWindow from '../materialUIDecorators/dialogWindow';
import CommentIcon from 'material-ui-icons/InsertComment';
import CloseIcon from 'material-ui-icons/Close';
import IconButton from '../materialUIDecorators/iconButton';
import AddCommentPanel from './addCommentPanel';
import styled from 'styled-components';

export default class AddCommentDialog extends React.Component {
  constructor(props) {
    super(props);
    this.state = ({ isOpen: false });
  }

  changeCommentText(text) {
    this.setState({commentText: text});
  }

  handleOpenClose(isOpen) {
    this.setState({isOpen: isOpen});
    this.changeCommentText('');
  }

  render() {
    const handleOpenClose = this.handleOpenClose.bind(this);
    return (
      <DialogWindow
        open={this.state.isOpen}
        content={
          <FormWrapper>
            <AddCommentPanel
              addComment={this.props.addComment}
              candidateId={this.props.candidate.id}
              onClick={() => { handleOpenClose(false) }}
              userName={this.props.userName}
            />
          </FormWrapper>}
        label="Add new comment"
        openButton={
          <IconButton icon={<CommentIcon />} style={{height: 40, width: 40}} onClick={() => {handleOpenClose(true)}}/>
        }
        controls={
          <div style={{display: 'inline-block'}}>
            <IconButton color="inherit" icon={<CloseIcon />} onClick={() => {handleOpenClose(false)}}/>
          </div>
        }
      />
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