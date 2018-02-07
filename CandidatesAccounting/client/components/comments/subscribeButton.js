import React, {Component} from 'react';
import PropTypes from 'prop-types';
import IconButton from '../common/UIComponentDecorators/iconButton';
import NotificationsIcon from 'material-ui-icons/Notifications';
import NotificationsOffIcon from 'material-ui-icons/NotificationsOff';
import NotificationsActiveIcon from 'material-ui-icons/NotificationsActive';

export default class SubscribeButton extends Component {
  constructor(props) {
    super(props);
    this.state = {active: !props.disabled && props.candidate.subscribers.includes(props.userName)};
    this.handleClick = this.handleClick.bind(this);
  }

  handleClick() {
    if (this.state.active) {
      this.props.unsubscribe(this.props.candidate.id, this.props.userName);
      this.setState({active: false});
    } else {
      this.props.subscribe(this.props.candidate.id, this.props.userName);
      this.setState({active: true});
    }
  }

  render() {
    return (
      <IconButton
        icon={
          this.props.disabled ?
            <NotificationsIcon/>
            :
            this.state.active ?
              <NotificationsActiveIcon/>
              :
              <NotificationsOffIcon/>
        }
        disabled={this.props.disabled}
        onClick={this.handleClick}
      />
    );
  }
}

SubscribeButton.propTypes = {
  candidate: PropTypes.object.isRequired,
  userName: PropTypes.string.isRequired,
  subscribe: PropTypes.func.isRequired,
  unsubscribe: PropTypes.func.isRequired,
  disabled: PropTypes.bool,
};