import { connect } from 'react-redux';
import PropTypes from 'prop-types';
import Router from './router/router';
import { Component, Fragment } from 'react';
import { Dimmer, Loader } from 'semantic-ui-react';

class App extends Component {
  render() {
    const { isLoading } = this.props;

    return (
      <Fragment>
        <Dimmer active={isLoading} inverted>
          <Loader>Loading</Loader>
        </Dimmer>
        <Router/>
      </Fragment>
    );
  }
}

function mapStateToProps(state) {
  const { isLoading } = state.app;
  return {
    isLoading
  };
}

App.propTypes = {
  isLoading: PropTypes.bool.isRequired
};

export default connect(mapStateToProps)(App);
