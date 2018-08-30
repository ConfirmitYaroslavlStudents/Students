import { connect } from 'react-redux';
import { namesActions } from 'store/actions';
import NamesBlock from './namesBlock';
import PropTypes from 'prop-types';
import { Button, Container, Divider, Icon, Input } from 'semantic-ui-react';
import { Component, Fragment } from 'react';

class NamesFragment extends Component {
  constructor(props) {
    super(props);

    this.state = {
      currentName: '',
      isValidField: true
    };
  }

  handleChange = (e, { name, value }) => {
    const { names } = this.props;

    this.setState({
      [name]: value,
      isValidField: Boolean(!names.includes(value) && value)
    });
  };

  addName = () => {
    const { currentName } = this.state;
    this.props.addName(currentName);
    this.setState({ currentName: '' });
  };

  render() {
    const { names, count, nextStep } = this.props;
    const { currentName, isValidField } = this.state;

    return (
      <Fragment>
        <Divider horizontal>Please enter players names</Divider>
        {
          names.length < parseInt(count, 10) &&
          <Input
            name='currentName'
            placeholder='Player name'
            onChange={this.handleChange}
            error={!isValidField}
            value={currentName || ''}
            icon={
              <Icon
                onClick={this.addName}
                circular
                inverted
                disabled={!isValidField}
                name='add'
                color={'orange'}
                link={Boolean(isValidField && currentName)}
              />
            }
          />
        }
        <Container className={'center'}>
          {
            names.length === parseInt(count, 10) &&
            <Button
              content='Next step'
              color={'orange'}
              icon='right arrow'
              labelPosition='right'
              onClick={nextStep}
            />
          }
        </Container>
        <NamesBlock/>
      </Fragment>
    );
  }
}

const mapStateToProps = state => {
  const { names } = state.data;
  return {
    names
  };
};

const mapDispatchToProps = dispatch => {
  return {
    addName: (name) => dispatch(namesActions.addName(name))
  };
};

NamesFragment.propTypes = {
  count: PropTypes.string.isRequired,
  names: PropTypes.arrayOf(PropTypes.string.isRequired).isRequired,

  addName: PropTypes.func.isRequired,
  nextStep: PropTypes.func.isRequired
};

export default connect(mapStateToProps, mapDispatchToProps)(NamesFragment);
