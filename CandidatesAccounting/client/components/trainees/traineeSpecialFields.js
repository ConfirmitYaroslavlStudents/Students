import React from 'react';
import PropTypes from 'prop-types';
import TextField from '../common/UIComponentDecorators/textField';

export default function TraineeSpecialFields(props) {
  return (
    <div>
      <TextField
        onChange={(value) => {props.changeInfo('mentor', value)}}
        label="Mentor's name"
        placeholder="full name"
        value={props.trainee.mentor}
        multiline
        fullWidth/>
    </div>);
}

TraineeSpecialFields.propTypes = {
  trainee: PropTypes.object.isRequired,
  changeInfo: PropTypes.func.isRequired,
};