import React from 'react'
import PropTypes from 'prop-types'
import TextField from '../../../common/UIComponentDecorators/textField'

export default function TraineeSpecialFields(props) {
  const handleMentorChange = value => {
    props.changeProperty('mentor', value)
  }

  return (
    <div>
      <TextField
        onChange={handleMentorChange}
        label="Mentor's name"
        placeholder='full name'
        value={props.trainee.mentor}/>
    </div>
  )
}

TraineeSpecialFields.propTypes = {
  trainee: PropTypes.object.isRequired,
  changeProperty: PropTypes.func.isRequired,
}