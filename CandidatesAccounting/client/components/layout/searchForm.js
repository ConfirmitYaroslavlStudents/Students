import React, {Component} from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import SearchIcon from 'material-ui-icons/search';
import Input from '../common/UIComponentDecorators/input';
import IconButton from '../common/UIComponentDecorators/iconButton';

export default class SearchForm extends Component {
  constructor(props) {
    super(props);
    this.state = {isOpen: false};
    this.handleChange = this.handleChange.bind(this);
    this.handleOpen = this.handleOpen.bind(this);
    this.handleClose = this.handleClose.bind(this);
    this.handleClick = this.handleClick.bind(this);
    this.timer = null;
  }

  handleOpen() {
    this.setState({isOpen: true});
  }

  handleClose() {
    this.setState({isOpen: false});
  }

  handleClick() {
    if (this.props.searchRequest !== '') {
      this.props.setSearchRequest(this.props.searchRequest);
    } else {
      if (!this.state.isOpen) {
        this.setState({isOpen: true});
      }
    }
  }

  handleChange(value) {
    this.props.setSearchRequest(value);
    if (this.timer) {
      clearTimeout(this.timer);
    }
    this.timer = setTimeout(() => {
      this.props.changeURL(this.props.history);
      this.props.loadCandidates();
      this.timer = null;
    }, 900);
  }

  render() {
    return (
      <Form>
        <IconButton
          color='contrast'
          icon={<SearchIcon/>}
          onClick={this.handleClick}
          placeholder='search'
        />
        {
          this.state.isOpen || this.props.searchRequest !== '' ?
            <Input
              onChange={this.handleChange}
              value={this.props.searchRequest}
              className='search'
              placeholder='search'
              onFocus={() => {this.handleOpen()}}
              onBlur={() => {this.handleClose()}}
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
  changeURL: PropTypes.func.isRequired,
  history: PropTypes.object.isRequired,
  setSearchRequest: PropTypes.func.isRequired,
  loadCandidates: PropTypes.func.isRequired,
};

const Form = styled.div`
  display: flex;
  align-items: center;
`;