import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../actions'
import FlatButton from '../commonComponents/UIDecorators/flatButton'

class AppControls extends Component {
  render() {
    const { commit,  markedToUpdateAmount } = this.props

    return (
      <FlatButton onClick={commit} color='secondary' disabled={markedToUpdateAmount <= 0}>
        Commit{markedToUpdateAmount > 0 ? ` (${markedToUpdateAmount})` : ''}
      </FlatButton>
    )
  }
}

AppControls.propTypes = {
  commit: PropTypes.func.isRequired,
  markedToUpdateAmount: PropTypes.number.isRequired
}

export default connect((state) => ({
  markedToUpdateAmount: state.markedToUpdateAmount
}), actions)(AppControls)
