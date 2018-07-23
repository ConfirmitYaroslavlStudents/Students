import React, { Component } from 'react'
import PropTypes from 'prop-types'
import FlatButton from '../commonComponents/UIDecorators/flatButton'

class TestControls extends Component {
  handleUpdate = () => {
    this.props.onUpdate()
  }

  handleCancel = () => {
    this.props.onCancel()
  }

  render() {
    const { test } = this.props

    return (
      test.markedToUpdate ?
        <FlatButton onClick={this.handleCancel} color='secondary'>
          Cancel
        </FlatButton>
        :
        <FlatButton onClick={this.handleUpdate} color='secondary'>
          Update
        </FlatButton>
    )
  }
}

TestControls.propTypes = {
  test: PropTypes.object.isRequired,
  onUpdate: PropTypes.func.isRequired,
  onCancel: PropTypes.func.isRequired
}

export default TestControls
