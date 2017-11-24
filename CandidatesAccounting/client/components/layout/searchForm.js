import React from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import SearchIcon from 'material-ui-icons/search';
import Input from '../common/UIComponentDecorators/input';
import IconButton from '../common/UIComponentDecorators/iconButton';

export default class SearchForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = {isOpen: false};
    this.changeRequest = this.changeRequest.bind(this);
    this.open = this.open.bind(this);
    this.close = this.close.bind(this);
    this.handleClick = this.handleClick.bind(this);
    this.search = this.search.bind(this);
  }

  search() {
    this.props.search(this.props.searchRequest, this.props.history);
  }

  open() {
    this.setState({isOpen: true});
  }

  close() {
    this.setState({isOpen: false});
  }

  handleClick() {
    if (this.state.isOpen) {
      if (this.props.searchRequest === '') {
        this.close();
      } else {
        this.search();
      }
    } else {
      this.open();
    }
  }

  changeRequest(value) {
    this.props.setSearchRequest(value, this.props.history);
  }

  render() {
    return (
      <Form>
        <IconButton
          color="contrast"
          icon={<SearchIcon/>}
          onClick={this.handleClick}
          placeholder="search"
        />
        {
          this.state.isOpen ?
            <Input
              onChange={this.changeRequest}
              className="search"
              placeholder="search"
              defaultValue={this.props.searchRequest}
              onBlur={() => {if (this.props.searchRequest === '') this.close()}}
              disableUnderline
              autoFocus
            />
            : ''
        }
      </Form>
    );
  }
}

SearchForm.propTypes = {
  history: PropTypes.object.isRequired,
  searchRequest: PropTypes.string.isRequired,
  setSearchRequest: PropTypes.func.isRequired,
  search: PropTypes.func.isRequired,
};

const Form = styled.div`
  display: flex;
  align-items: center;
`;