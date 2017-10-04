import React from 'react';
import PropTypes from 'prop-types';
import {CreateCandidate} from '../candidates/index';
import DialogWindow from '../materialUIDecorators/dialogWindow';
import AddIcon from 'material-ui-icons/Add';
import EditCandidateForm from './editCandidateForm';

export default function AddCandidateDialog(props) {
  return (
    <div className="add-btn">
      <DialogWindow
        content={
          <EditCandidateForm
            additionMode={true}
            changeTempCandidateInfo={props.changeTempCandidateInfo}
            setTempCandidateComment={props.setTempCandidateComment}
            editCandidate={props.editCandidate}
            tempCandidate={props.tempCandidate}
            setTempCandidate={props.setTempCandidate}
          />
        }
        label="Add new candidate"
        openButtonType="flat"
        openButtonContent={<div className="button-content"><AddIcon/> new candidate</div>}
        acceptButtonContent={<div className="button-content"><AddIcon/> <span style={{marginTop: 3}}>add</span></div>}
        open={function() {
          props.setTempCandidate(CreateCandidate(props.candidateType, {}));
        }}
        accept={function() {
          props.addCandidate(CreateCandidate(props.tempCandidate.status, props.tempCandidate))
        }}
        close={function() {
          props.setTempCandidate(CreateCandidate(props.candidateType, {}));
        }}
      />
    </div>);
}

AddCandidateDialog.propTypes = {
  addCandidate: PropTypes.func.isRequired,
  candidateType: PropTypes.string.isRequired,
  tempCandidate: PropTypes.object.isRequired,
  setTempCandidate: PropTypes.func.isRequired,
  changeTempCandidateInfo: PropTypes.func.isRequired,
  setTempCandidateComment: PropTypes.func.isRequired,
  editCandidate: PropTypes.func.isRequired,
};