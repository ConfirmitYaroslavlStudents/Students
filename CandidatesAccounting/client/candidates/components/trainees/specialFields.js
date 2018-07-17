import React from 'react'
import PropTypes from 'prop-types'
import TextField from '../../../commonComponents/UIComponentDecorators/textField'

const TraineeSpecialFields = (props) => {
  const handleMentorChange = (value) => {
    props.changeProperty('mentor', value)
  }

  return (
    <TextField
      onChange={handleMentorChange}
      label="Mentor's name"
      placeholder='full name'
      value={props.trainee.mentor}/>
  )
}

TraineeSpecialFields.propTypes = {
  trainee: PropTypes.object.isRequired,
  changeProperty: PropTypes.func.isRequired
}

export default TraineeSpecialFields