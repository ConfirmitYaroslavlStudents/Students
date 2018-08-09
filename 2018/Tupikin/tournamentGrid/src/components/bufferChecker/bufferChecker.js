import { Component } from 'react';
import { Redirect, Switch } from 'react-router-dom';

class BufferChecker extends Component {
  render() {
    return (
      <Switch>
        <Redirect to='/enter'/>
      </Switch>
    );
  }
}

export default BufferChecker;
