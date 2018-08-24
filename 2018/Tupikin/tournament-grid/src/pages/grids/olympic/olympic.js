import { connect } from 'react-redux';
import PropTypes from 'prop-types';
import { addWinnerInTree, generateTree } from 'utils';
import { Component, Fragment } from 'react';
import { Divider, Form, Icon } from 'semantic-ui-react';
import 'treantjs/Treant.css';
import 'treantjs/Treant';
import 'treantjs/examples/tennis-draw/example7.css';

class Olympic extends Component {
  constructor(props) {
    super(props);

    this.state = {
      tree: generateTree(this.props.names.length !== 0 ? this.props.names : ['test_1', 'test_2', 'test_3', 'test_4']),
      winner: ''
    };
  }

  handleChange = (e, { value }) => {
    this.setState({
      winner: value
    });
  };

  addWinner = () => {
    const { winner, tree } = this.state;
    this.setState({
      tree: addWinnerInTree(tree, winner)
    });
  };

  draw = () => {
    const { tree } = this.state;
    setTimeout(() => window.Treant(tree), 250);
  };

  render() {
    return (
      <Fragment>
        <div className={'chart'} id='chart'/>
        {this.draw() && <div/>}
        <div className={'center limited'}>
          <Divider horizontal>Please enter any winner name</Divider>
          <Form.Input
            placeholder='Please enter name of winner in any game'
            onChange={this.handleChange}
            value={this.state.winner}
            icon={
              <Icon
                onClick={this.addWinner}
                size='large'
                color='orange'
                name='add circle'
                link
              />
            }
          />
          <Divider/>
        </div>
      </Fragment>
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

Olympic.defaultProps = {
  names: ['test_1', 'test_2']
};

