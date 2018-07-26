import React from 'react'
import PropTypes from 'prop-types'
import DoneIcon from '@material-ui/icons/Done'
import CloseIcon from '@material-ui/icons/Close'

const TestIcon = (props) => {
  return (
    props.markedToUpdate ?
      <DoneIcon style={{color: '#29B6F6'}} />
      :
      null // <CloseIcon style={{color: '#ef5350'}}/>
  )
}

TestIcon.propTypes = {
  markedToUpdate: PropTypes.bool
}

export default TestIcon