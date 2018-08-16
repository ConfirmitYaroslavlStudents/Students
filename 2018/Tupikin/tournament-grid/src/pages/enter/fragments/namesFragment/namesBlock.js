import { connect } from 'react-redux';
import { namesActions } from 'store/actions';
import PropTypes from 'prop-types';
import { Divider, Icon, Label, Message } from 'semantic-ui-react';
import { Fragment, PureComponent } from 'react';

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

  onClick = (name) => {
    this.props.deleteName(name);
  };

  namesMapper = (name) => {
    return (
      <Label key={name}>
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
        {
          names.length > 0 &&
            names.map(this.namesMapper)
        }
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
