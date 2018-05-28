import React, { Component } from 'react'
import PropTypes from 'prop-types'
import Avatar from '@material-ui/core/Avatar'
import AccountIcon from '@material-ui/icons/AccountCircle'

export default class ImageAvatars extends Component {
  constructor(props) {
    super(props)

    this.state = { imageUploaded: true }
  }

  handleError = () => {
    this.setState({ imageUploaded: false })
  }

  render() {
    const {src, alt} = this.props
    if (this.state.imageUploaded) {
      return <Avatar src={src} imgProps={{onError: this.handleError}}/>
    } else {
      return <AccountIcon/>
    }
  }
}

ImageAvatars.propTypes = {
  src: PropTypes.string.isRequired,
  alt: PropTypes.string.isRequired
}