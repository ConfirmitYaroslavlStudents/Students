import { connect } from 'react-redux';
import PropTypes from 'prop-types';
import { Component, Fragment } from 'react';
import 'treantjs/Treant.css';
import 'treantjs/Treant';
import 'treantjs/examples/tennis-draw/example7.css';

class Olympic extends Component {
  render() {
    return (
      <Fragment/>
    );
  }
}

const mapStateToProps = state => ({
  names: state.data.names
});

export default connect(mapStateToProps)(Olympic);

Olympic.propTypes = {
  names: PropTypes.arrayOf(PropTypes.string.isRequired).isRequired
};
