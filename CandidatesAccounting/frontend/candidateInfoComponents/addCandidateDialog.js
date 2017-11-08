import React from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import {createCandidate, Comment, writeCandidate, checkCandidateValidation} from '../candidatesClasses/index';
import DialogWindow from '../materialUIDecorators/dialogWindow';
import AddPersonIcon from 'material-ui-icons/PersonAdd';
import CloseIcon from 'material-ui-icons/Close';
import CandidateInfoForm from './candidateInfoForm';
import IconButton from '../materialUIDecorators/iconButton';
import {getCurrentDateTime} from '../customMoment';

export default class AddCandidateDialog extends React.Component{
  constructor(props) {
    super(props);
    this.state=({isOpen: false});
    let newCandidate = createCandidate(props.candidateStatus, {});
    newCandidate.status = props.candidateStatus;
    this.candidate = newCandidate;
  }

  handleOpenClose(isOpen) {
    this.setState({isOpen: isOpen});
  }

  render() {
    const handleOpenClose = this.handleOpenClose.bind(this);
    let candidate = this.candidate;
    const props = this.props;
    let newCandidate = createCandidate(props.candidateStatus, {});
    newCandidate.status = props.candidateStatus;
    candidate = newCandidate;
    return (
      <AddButtonWrapper>
        <DialogWindow
          open={this.state.isOpen}
          content={
            <CandidateInfoForm
              candidate={candidate}
              tags={this.props.tags}
            />
          }
          label="Add new candidate"
          openButton={ <IconButton icon={<AddPersonIcon />} style={{height: 40, width: 40}} onClick={() => {handleOpenClose(true)}}/> }
          controls={
            <div style={{display: 'inline-block'}}>
              <IconButton color="inherit" icon={<AddPersonIcon />} onClick={() => {
                if (checkCandidateValidation(candidate)) {
                  candidate.comments.push(new Comment(props.userName, getCurrentDateTime(), 'Initial ' + writeCandidate(candidate)));
                  props.addCandidate(candidate);
                  handleOpenClose(false);
                }
              }}/>
              <IconButton color="inherit" icon={<CloseIcon />} onClick={() => {handleOpenClose(false)}}/>
            </div>
          }
        />
      </AddButtonWrapper>);
  }
}

AddCandidateDialog.propTypes = {
  addCandidate: PropTypes.func.isRequired,
  candidateStatus: PropTypes.string.isRequired,
  tags: PropTypes.object.isRequired,
  userName: PropTypes.string.isRequired,
};

const AddButtonWrapper = styled.div`
  display: inline-block;
  position: absolute;
  right: 5px;
  top: 1px;
`;

