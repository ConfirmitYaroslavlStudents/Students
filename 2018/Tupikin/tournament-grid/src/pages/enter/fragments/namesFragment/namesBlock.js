import { colors } from '../../constants/constants';
import { connect } from 'react-redux';
import { namesActions } from 'store/actions';
import PropTypes from 'prop-types';
import { Container, Divider, Icon, Label, Message } from 'semantic-ui-react';
import { Fragment, PureComponent } from 'react';

const randomInt = (min, max) => {
  return Math.floor(Math.random() * (max - min)) + min;
};

class NamesBlock extends PureComponent {
  message = () => (
    <div className='center'>
      <Message
        compact
        info
      >
        Enter name in input field
      </Message>
    </div>
  );

  randomColor = () => {
    return colors[randomInt(0, colors.length)];
  };

  onClick = (name) => {
    this.props.deleteName(name);
  };

  namesMapper = (name) => {
    return (
      <Label
        key={name}
        color={this.randomColor()}
      >
        {name}
        <Icon
          name='delete'
          onClick={() => this.onClick(name)}
        />
      </Label>
    );
  };

  render() {
    const { names } = this.props;

    return (
      <Fragment>
        <Divider horizontal>Players names</Divider>
        {
          names.length === 0 &&
          this.message()
        }
        <Container className={'center'}>
          {
            names.length > 0 &&
          names.map(this.namesMapper)
          }
        </Container>
      </Fragment>
    );
  }
}

const mapStateToProps = state => {
  return {
    names: state.data.names
  };
};

const mapDispatchToProps = dispatch => {
  return {
    deleteName: (name) => dispatch(namesActions.deleteName(name))
  };
};

NamesBlock.propTypes = {
  names: PropTypes.array.isRequired,

  deleteName: PropTypes.func.isRequired
};

export default connect(mapStateToProps, mapDispatchToProps)(NamesBlock);
