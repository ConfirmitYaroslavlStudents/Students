import React from 'react';
import Button from 'material-ui/Button';

export default class RaisedButton extends React.Component {
  render() {
    return (
      <div style={{"display": "inline"}}>
        <Button raised color={this.props.color} className={this.props.class} onClick={this.props.onClick} disabled={this.props.disabled}>
          {this.props.icon}
          {this.props.text}
        </Button>
      </div>
    );
  }
}