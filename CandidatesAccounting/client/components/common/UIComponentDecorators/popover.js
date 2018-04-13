import React, {Component} from 'react';
import ReactDOM from 'react-dom';
import PropTypes from 'prop-types';
import IconButton from 'material-ui/IconButton';
import Popover from 'material-ui/Popover';

export default class CustomPopover extends Component {
  constructor(props) {
    super(props);
    this.state = { open: false, button: null }
    this.button = null
  }

  handleClickButton = () => {
    this.setState({
      open: true,
      anchorEl: ReactDOM.findDOMNode(this.button)
    })
  }

  handleClose = () => {
    this.setState({
      open: false,
    })
  }

  render() {
    return (
      <div style={{display: 'inline-flex', zIndex: 100}}>
        <IconButton
          ref={node => {
            this.button = node
          }}
          color={this.props.iconColor}
          onClick={this.handleClickButton}
        >
          {this.props.icon}
        </IconButton>
        <Popover
          open={this.state.open}
          anchorReference={'anchorEl'}
          onBackdropClick={(event) => {this.handleClose()}}
          anchorEl={this.state.anchorEl}
          anchorPosition={{ top: 200, left: 400 }}
          anchorOrigin={{
            vertical: 'bottom',
            horizontal: 'center',
          }}
          transformOrigin={{
            vertical: 'top',
            horizontal: 'center',
          }}
        >
          {this.props.content}
        </Popover>
      </div>
    )
  }
}

CustomPopover.propTypes = {
  content: PropTypes.object,
  icon: PropTypes.object,
  iconColor: PropTypes.string,
}