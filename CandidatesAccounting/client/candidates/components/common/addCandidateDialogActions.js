import React from 'react'
import PropTypes from 'prop-types'
import AddPersonIcon from '@material-ui/icons/PersonAdd'
import CloseIcon from '@material-ui/icons/Close'
import IconButton from '../../../commonComponents/UIComponentDecorators/iconButton'

const AddCandidateDialogActions = (props) => {
  return (
    <React.Fragment>
      <IconButton icon={<AddPersonIcon />} onClick={props.onAcceptClick} color='inherit' />
      <IconButton icon={<CloseIcon />} onClick={props.onCancelClick} color='inherit' />
    </React.Fragment>
  )
}

AddCandidateDialogActions.propTypes = {
  onAcceptClick: PropTypes.func.isRequired,
  onCancelClick: PropTypes.func.isRequired
}

export default AddCandidateDialogActions