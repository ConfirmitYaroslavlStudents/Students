import { Component } from 'react';
import { Redirect } from 'react-router-dom';

class DataLoader extends Component {
  render() {
    return (
      <Redirect to='/enter'/>
    );
  }
}

export default DataLoader;
