import React, {Component} from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import SearchIcon from 'material-ui-icons/search';
import Input from '../common/UIComponentDecorators/input';
import IconButton from '../common/UIComponentDecorators/iconButton';

export default class SearchForm extends Component {
  constructor(props) {
    super(props);
    this.state = {isActive: false};
    this.changeRequest = this.changeRequest.bind(this);
    this.activate = this.activate.bind(this);
    this.deactivate = this.deactivate.bind(this);
    this.handleClick = this.handleClick.bind(this);
  }

  activate() {
    this.setState({isActive: true});
  }

  deactivate() {
    this.setState({isActive: false});
  }

  handleClick() {
    if (this.props.searchRequest !== '') {
      this.props.setSearchRequest(this.props.searchRequest, 0);
    } else {
      if (!this.state.isActive) {
        this.setState({isActive: true});
      }
    }
  }

  changeRequest(value) {
    this.props.setSearchRequest(value, this.props.history, 500);
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
          this.state.isActive || this.props.searchRequest !== '' ?
            <Input
              onChange={this.changeRequest}
              value={this.props.searchRequest}
              className='search'
              placeholder='search'
              onFocus={() => {this.activate()}}
              onBlur={() => {this.deactivate()}}
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
  setSearchRequest: PropTypes.func.isRequired,
};

const Form = styled.div`
  display: flex;
  align-items: center;
`;