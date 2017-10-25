import React from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import {CreateCandidate} from '../candidatesClasses/index';
import DialogWindow from '../materialUIDecorators/dialogWindow';
import AddPersonIcon from 'material-ui-icons/PersonAdd';
import CloseIcon from 'material-ui-icons/Close';
import CandidateInfoForm from './candidateInfoForm';
import IconButton from '../materialUIDecorators/iconButton';

export default class AddCandidateDialog extends React.Component{
  constructor(props) {
    super(props);
    this.state=({isOpen: false});
    let newCandidate = CreateCandidate(props.candidateStatus, {});
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
    let newCandidate = CreateCandidate(props.candidateStatus, {});
    newCandidate.status = props.candidateStatus;
    candidate = newCandidate;
    return (
      <AddButtonWrapper>
        <DialogWindow
          open={this.state.isOpen}
          content={
            <CandidateInfoForm candidate={candidate} />
          }
          label="Add new candidate"
          openButton={ <IconButton icon={<AddPersonIcon />} onClick={() => {handleOpenClose(true)}}/> }
          controls={
            <div style={{display: 'inline-block'}}>
              <IconButton color="inherit" icon={<AddPersonIcon />} onClick={() => {
                props.addCandidate(candidate);
                handleOpenClose(false);
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
};

const AddButtonWrapper = styled.div`
  display: inline-block;
  position: absolute;
  right: 5px;
  top: 1px;
`;

