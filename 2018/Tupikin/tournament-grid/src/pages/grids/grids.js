import { connect } from 'react-redux';
import PropTypes from 'prop-types';
import { Component, Fragment } from 'react';
import { Redirect, withRouter } from 'react-router-dom';

class Grids extends Component {
  render() {
    const { type } = this.props;

    return (
      <Fragment>
        {
          type === 'olympic' &&
          <Redirect to='/grid/olympic'/>
        }
      </Fragment>
    );
  }
}

const mapStateToProps = state => {
  return {
    type: state.data.type
  };
};

export default withRouter(connect(mapStateToProps)(Grids));

Grids.propTypes = {
  type: PropTypes.string.isRequired,

  history: PropTypes.shape({
    push: PropTypes.func.isRequired
  }).isRequired
};
