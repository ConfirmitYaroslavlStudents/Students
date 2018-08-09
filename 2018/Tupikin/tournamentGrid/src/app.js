import BufferChecker from 'components/bufferChecker/bufferChecker';
import { connect } from 'react-redux';
import Enter from 'components/enter/enter';
import PropTypes from 'prop-types';
import { Component, Fragment } from 'react';
import { Dimmer, Loader } from 'semantic-ui-react';
import {
  Route,
  HashRouter as Router,
  Switch
} from 'react-router-dom';

class App extends Component {
  render() {
    const { isLoading } = this.props;

    return (
      <Fragment>
        <Router>
          <Fragment>
            <Dimmer active={isLoading} inverted>
              <Loader>Loading</Loader>
            </Dimmer>
            <Switch>
              <Route exact path='/' component={BufferChecker}/>
              <Route path='/enter' component={Enter}/>
              <Route path='/grid' component={null}/>
            </Switch>
          </Fragment>
        </Router>
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
  isLoading: PropTypes.bool
};

export default connect(mapStateToProps)(App);
