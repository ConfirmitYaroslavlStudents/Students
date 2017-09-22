import React from 'react';
import PropTypes from 'prop-types';
import Button from 'material-ui/Button';

export default class RaisedButton extends React.Component {
  render() {
    return (
      <div style={{"display": "inline"}}>
        <Button
          raised
          color={this.props.color}
          onClick={this.props.onClick}
          disabled={this.props.disabled}
        >
          {this.props.icon}
          {this.props.text}
        </Button>
      </div>
    );
  }
}

RaisedButton.propTypes = {
  onClick: PropTypes.func.isRequired,
  disabled: PropTypes.bool,
  icon: PropTypes.object,
  text: PropTypes.object,
  color: PropTypes.string,
};