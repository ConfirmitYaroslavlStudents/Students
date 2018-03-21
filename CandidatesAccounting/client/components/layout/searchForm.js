import React, {Component} from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import SearchIcon from 'material-ui-icons/search';
import Input from '../common/UIComponentDecorators/input';
import IconButton from '../common/UIComponentDecorators/iconButton';

export default class SearchForm extends Component {
  constructor(props) {
    super(props);
    this.state = {isOpen: false, searchRequest: this.props.searchRequest};
    this.handleChange = this.handleChange.bind(this);
    this.handleOpen = this.handleOpen.bind(this);
    this.handleClose = this.handleClose.bind(this);
    this.handleClick = this.handleClick.bind(this);
    this.search = this.search.bind(this);
    this.timer = null;
  }

  handleOpen() {
    this.setState({isOpen: true, searchRequest: this.props.searchRequest});
  }

  handleClose() {
    if (this.state.searchRequest === '') {
      this.setState({isOpen: false, searchRequest: ''});
    }
  }

  handleClick() {
    if (this.state.searchRequest !== '' && this.state.isOpen) {
      this.timer = null;
      this.search(this.state.searchRequest);
    } else {
      if (this.state.isOpen) {
        this.handleClose();
      } else {
        this.handleOpen();
      }
    }
  }

  handleChange(value) {
    this.setState({searchRequest: value});
    if (this.timer) {
      clearTimeout(this.timer);
    }
    this.timer = setTimeout(() => {
      if (this.timer) {
        this.search(value);
      }
      this.timer = null;
    }, 900);
  }

  search(searchRequest) {
    this.props.loadCandidates(
      {
        applicationStatus: 'refreshing',
        searchRequest: searchRequest
      },
      this.props.history);
  }

  render() {
    return (
      <Form>
        <IconButton
          color='inherit'
          icon={<SearchIcon/>}
          onClick={this.handleClick}
          placeholder='search'
        />
        {
          this.state.isOpen || this.state.searchRequest !== '' ?
            <Input
              onChange={this.handleChange}
              value={this.state.searchRequest}
              className='search'
              placeholder='search'
              onFocus={() => {this.handleOpen()}}
              onBlur={() => {
                if (this.state.searchRequest === '') {
                  this.handleClose();
                }
              }}
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
  searchRequest: PropTypes.string.isRequired,
  history: PropTypes.object.isRequired,
  loadCandidates: PropTypes.func.isRequired,
};

const Form = styled.div`
  display: flex;
  align-items: center;
`;