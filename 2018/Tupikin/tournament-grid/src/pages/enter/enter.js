import { Component } from 'react';
import CountFragment from './fragments/countFragment/countFragment';
import { isPowerOfTwo } from 'utils';
import { logo } from './constants/constants';
import NamesFragment from './fragments/namesFragment/namesFragment';
import { Grid, Image, Segment } from 'semantic-ui-react';

class Enter extends Component {
  constructor(props) {
    super(props);

    this.steps = [
      this.countFragment,
      this.namesFragment
    ];

    this.state = {
      count: '',
      isValidField: false,
      modalIsOpen: false,
      stepId: 0
    };
  }

  toggleModal = () => {
    const { modalIsOpen } = this.state;
    this.setState({ modalIsOpen: !modalIsOpen });
  };

  changeCount = (e, { value }) => {
    this.setState({
      count: value,
      isValidField: /^\d+$/.test(value)
    });
  };

  nextStep = () => {
    const { isValidField, count, stepId } = this.state;

    if (!isValidField || !isPowerOfTwo(count)) {
      this.toggleModal();
      return;
    }

    this.setState({ stepId: stepId + 1 });
  };

  countFragment = () => {
    const { count, isValidField, modalIsOpen } = this.state;
    return (
      <CountFragment
        count={count}
        isValidField={isValidField}
        modalIsOpen={modalIsOpen}
        toggleModal={this.toggleModal}
        changeHandle={this.changeCount}
        nextStep={this.nextStep}
      />
    );
  };

  namesFragment = () => {
    const { count } = this.state;

    return (
      <NamesFragment
        count={count}
      />
    );
  };

  render() {
    const { stepId } = this.state;

    return (
      <Grid verticalAlign='middle' centered>
        <Grid.Column>
          <Segment compact>
            <Image
              rounded
              src={logo}
            />
            { this.steps[stepId]() }
          </Segment>
        </Grid.Column>
      </Grid>
    );
  }
}

export default Enter;
