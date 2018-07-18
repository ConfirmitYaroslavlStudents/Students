import React, { Component } from 'react'
import PropTypes from 'prop-types'
import Snackbar from '@material-ui/core/Snackbar'
import Fade from '@material-ui/core/Fade'

class ErrorSnackbar extends Component {
  constructor(props) {
    super(props)
    this.state = { open: false }
  }

  handleRequestClose = () => {
    this.props.onClose()
    this.setState({ open: false })
  }

  render() {
    const open = this.props.message !== '' ? true :  this.state.open

    return (
      <Snackbar
        action='Error'
        open={open}
        onClose={this.handleRequestClose}
        TransitionComponent={Fade}
        message={this.props.message}
        autoHideDuration={this.props.autoHideDuration ? this.props.autoHideDuration : 10000}
        anchorOrigin={{ vertical: 'bottom', horizontal: 'left' }}
      />
    )
  }
}

ErrorSnackbar.propTypes = {
  message: PropTypes.string.isRequired,
  onClose: PropTypes.func.isRequired,
  autoHideDuration: PropTypes.number
}

export default ErrorSnackbar