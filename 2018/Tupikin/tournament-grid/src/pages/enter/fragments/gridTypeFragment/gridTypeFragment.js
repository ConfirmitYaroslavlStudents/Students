import { connect } from 'react-redux';
import { gridActions } from 'store/actions';
import PropTypes from 'prop-types';
import { withRouter } from 'react-router-dom';
import { Button, Container, Divider, Dropdown } from 'semantic-ui-react';
import { Component, Fragment } from 'react';

class GridTypeFragment extends Component {
  constructor(props) {
    super(props);

    this.state = {
      grid: undefined
    };

    this.gridTypes = [
      {
        text: 'Classic olympic grid',
        value: 'olympic'
      }
    ];
  }

  handleChange = (e, { value }) => {
    this.setState({ grid: value });
  };

  nextStep = () => {
    const { grid } = this.state;
    this.props.addGridType(grid);
    this.props.history.push('/grids');
  };

  render() {
    const { grid } = this.state;

    return (
      <Fragment>
        <Divider horizontal>Please select grid type</Divider>
        <Dropdown
          placeholder='Select grid type'
          fluid
          selection
          options={this.gridTypes}
          onChange={this.handleChange}
          value={grid}
        />
        <Divider hidden/>
        <Container className={'center'}>
          {
            grid &&
            <Button
              content='Next step'
              color={'orange'}
              icon='right arrow'
              labelPosition='right'
              onClick={this.nextStep}
            />
          }
        </Container>
      </Fragment>
    );
  }
}

const mapDispatchToProps = dispatch => {
  return {
    addGridType: (type) => dispatch(gridActions.addGridType(type))
  };
};

export default withRouter(connect(undefined, mapDispatchToProps)(GridTypeFragment));

GridTypeFragment.propTypes = {
  history: PropTypes.shape({
    push: PropTypes.func.isRequired
  }).isRequired,

  addGridType: PropTypes.func.isRequired
};

