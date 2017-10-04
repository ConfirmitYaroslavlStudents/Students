import React from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import {CreateCandidate} from '../candidates/index';
import DialogWindow from '../materialUIDecorators/dialogWindow';
import AddIcon from 'material-ui-icons/Add';
import EditCandidateInfoForm from './editCandidateInfoForm';

export default class AddCandidateDialog extends React.Component{
  constructor(props) {
    super(props);
    let newCandidate = CreateCandidate(props.candidateStatus, {});
    newCandidate.status = props.candidateStatus;
    this.candidate = newCandidate;
  }

  render() {
    let candidate = this.candidate;
    const props = this.props;
    let newCandidate = CreateCandidate(props.candidateStatus, {});
    newCandidate.status = props.candidateStatus;
    candidate = newCandidate;
    return (
      <AddButtonWrapper>
        <DialogWindow
          content={
            <EditCandidateInfoForm
              additionMode={true}
              candidate={candidate}
            />
          }
          label="Add new candidate"
          openButtonType="flat"
          openButtonContent={<div className="button-content"><AddIcon/> new candidate</div>}
          acceptButtonContent={<div className="button-content"><AddIcon/> <span style={{marginTop: 3}}>add</span></div>}
          accept={function () {
            props.addCandidate(CreateCandidate(candidate.status, candidate));
            return true;
          }}
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
  right: 1px;
  top: 1px;
`;

