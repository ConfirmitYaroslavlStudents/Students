import { namesActions as action } from 'store/actions';
import { Component } from 'react';
import { connect } from 'react-redux';
import CountFragment from './fragments/countFragment/countFragment';
import PropTypes from 'prop-types';
import { requiredImage } from './enterConstants';
import {
  Grid,
  Image,
  Segment
} from 'semantic-ui-react';

const steps = {
  COUNT_ENTER: CountFragment,
  NAMES_ENTER: 1,
  RENDER_SELECT: 2
};

class Enter extends Component {
  constructor(props) {
    super(props);

    this.state = {
      count: this.props.count,
      modalOpened: false,
      CurrentFragment: steps.COUNT_ENTER
    };
  }

  componentWillReceiveProps(nextProps) {
    if (this.props.count !== nextProps.count) { // todo уточнить
      this.setState({ count: nextProps.count });
    }
  }

  numberIsPowerOfTwo = () => {
    const { count } = this.state;
    const number = parseInt(count, 10);

    return Math.log2(number) % 1 === 0 && number !== 1;
  }

  onNextClick = () => {
    // todo check the power of two
    if (!this.numberIsPowerOfTwo()) {
      this.setState({ modalOpened: true });
    } else {
      this.setState({ step: steps.NAMES_ENTER });
    }
  };

  render() {
    const { CurrentFragment } = this.state;

    return (
      <Grid verticalAlign='middle' centered>
        <Grid.Column>
          <Grid.Row centered>
            <Segment compact>
              <Image
                rounded
                src={requiredImage}
              />
              <CurrentFragment
                {...this.props}
              />
            </Segment>
          </Grid.Row>
        </Grid.Column>
      </Grid>
    );
  }
}

const mapStateToProps = state => {
  const { players } = state;

  return {
    names: players.names,
    count: players.names.length
  };
};

export default connect(mapStateToProps, action)(Enter);

Enter.propTypes = {
  names: PropTypes.array.isRequired,
  count: PropTypes.number.isRequired,
  addName: PropTypes.func.isRequired,
  deleteName: PropTypes.func.isRequired
};
