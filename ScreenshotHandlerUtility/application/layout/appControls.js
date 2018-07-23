import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../actions'
import FlatButton from '../commonComponents/UIDecorators/flatButton'

class AppControls extends Component {
  render() {
    const { commit,  markedToUpdateCount } = this.props

    return (
      <FlatButton onClick={commit} color='secondary' disabled={markedToUpdateCount <= 0}>
        Commit{markedToUpdateCount > 0 ? ` (${markedToUpdateCount})` : ''}
      </FlatButton>
    )
  }
}

AppControls.propTypes = {
  commit: PropTypes.func.isRequired,
  markedToUpdateCount: PropTypes.number.isRequired
}

export default connect((state) => ({
  markedToUpdateCount: state.markedToUpdateCount
}), actions)(AppControls)
