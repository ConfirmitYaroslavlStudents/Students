import { Component } from 'react';
import { connect } from 'react-redux';
import { createModel } from 'pages/grids/olympic/createModel';
import { GojsDiagram } from 'react-gojs';
import { makeModel } from 'pages/grids/olympic/makeModel';
import { modelChange } from 'pages/grids/olympic/modelChange';
import PropTypes from 'prop-types';

class Olympic extends Component {
  render() {
    const { names } = this.props;

    return (
      <GojsDiagram
        diagramId='tournament'
        model={makeModel(names)}
        createDiagram={createModel}
        className='tournament'
        onModelChange={e => modelChange(e)}
      />
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
