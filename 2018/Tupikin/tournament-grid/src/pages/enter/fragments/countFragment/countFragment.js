import PropTypes from 'prop-types';
import { Divider, Icon, Input, Modal } from 'semantic-ui-react';
import { Fragment, PureComponent } from 'react';

class CountFragment extends PureComponent {
  render() {
    const {
      count,
      isValidField,
      modalIsOpen,

      toggleModal,
      changeHandle,
      nextStep
    } = this.props;

    return (
      <Fragment>
        <Modal
          size='mini'
          header='Attention'
          open={modalIsOpen}
          content='The number of players must be a multiple of two.'
          onClose={toggleModal}
          actions={[{
            key: 'okay',
            content: 'Okay',
            positive: true,
            onClick: toggleModal
          }]}
        />

        <Divider horizontal>Please enter count of players</Divider>
        <Input
          placeholder='Count'
          onChange={changeHandle}
          error={!isValidField}
          value={count || ''}
          icon={
            <Icon
              onClick={nextStep}
              circular
              inverted
              color={'orange'}
              disabled={!isValidField}
              name='right arrow'
              link={isValidField}
            />
          }
        />
      </Fragment>
    );
  }
}

CountFragment.propTypes = {
  count: PropTypes.string.isRequired,
  isValidField: PropTypes.bool.isRequired,
  modalIsOpen: PropTypes.bool.isRequired,

  toggleModal: PropTypes.func.isRequired,
  changeHandle: PropTypes.func.isRequired,
  nextStep: PropTypes.func.isRequired
};

export default CountFragment;
