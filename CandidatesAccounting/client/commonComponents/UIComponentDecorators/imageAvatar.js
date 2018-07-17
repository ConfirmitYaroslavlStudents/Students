import React, { Component } from 'react'
import PropTypes from 'prop-types'
import Avatar from '@material-ui/core/Avatar'
import AccountIcon from '@material-ui/icons/AccountCircle'

class ImageAvatar extends Component {
  constructor(props) {
    super(props)
    this.state = { imageUploaded: true }
  }

  handleError = () => {
    this.setState({ imageUploaded: false })
  }

  render() {
    const {source, alternative} = this.props

    if (this.state.imageUploaded) {
      return <Avatar src={source} alt={alternative.toUpperCase()} imgProps={{onError: this.handleError}}/>
    } else {
      return <AccountIcon style={{ height: 42, width: 42, color: '#3F51B5'}}/>
    }
  }
}

ImageAvatar.propTypes = {
  source: PropTypes.string.isRequired,
  alternative: PropTypes.string.isRequired
}

export default ImageAvatar