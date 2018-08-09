import PropTypes from 'prop-types';
import { Component, Fragment } from 'react';
import {
  Divider,
  Icon,
  Input,
  Modal
} from 'semantic-ui-react';

class CountFragment extends Component {
  constructor(props) {
    super(props);

    this.state = {
      count: this.props.count,
      isValid: false,
      hasBeenUsed: false
    };
  }

  isPowerOfTwo = () => {
    const { count } = this.state;
    const number = parseInt(count, 10);

    return Math.log2(number) % 1 === 0 && number !== 1;
  };


  handleChange = (e, { value }) => {
    this.setState({
      count: value,
      isValid: /^\d+$/.test(value),
      hasBeenUsed: true,
      modal: false
    });
  };

  toggleModal = () => {
    const { modal } = this.state;
    this.setState({ modal: !modal });
  };

  completeStep = () => {
    if (this.isPowerOfTwo()) {
      // todo action for adding
    } else {
      this.toggleModal();
    }
  };

  render() {
    const { isValid, count, hasBeenUsed, modal } = this.state;

    return (
      <Fragment>
        <Modal
          size='mini'
          header='Attention'
          open={modal}
          content='The number of players must be a multiple of two.'
          onClose={this.toggleModal}
          actions={[{
            key: 'okay',
            content: 'Okay',
            positive: true,
            onClick: this.toggleModal
          }]}
        />

        <Divider horizontal>Please enter count of players</Divider>
        <Input
          placeholder='Count'
          onChange={this.handleChange}
          error={!isValid && hasBeenUsed}
          value={count || ''}
          icon={
            <Icon
              onClick={this.completeStep}
              circular
              inverted
              disabled={!isValid}
              name='right arrow'
              link={isValid}
            />
          }
        />
      </Fragment>
    );
  }
}

CountFragment.propTypes = {
  count: PropTypes.number
};

export default CountFragment;
