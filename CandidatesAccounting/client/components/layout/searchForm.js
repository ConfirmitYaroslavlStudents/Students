import React from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import SearchIcon from 'material-ui-icons/search';
import Input from '../common/UIComponentDecorators/input';
import IconButton from '../common/UIComponentDecorators/iconButton';

function debounce(func, wait, immediate) {
  let timeout;
  return function() {
    const context = this;
    const args = arguments;
    let later = () => {
      timeout = null;
      if (!immediate) func.apply(context, args);
    };
    let callNow = immediate && !timeout;
    clearTimeout(timeout);
    timeout = setTimeout(later, wait);
    if (callNow) {
      func.apply(context, args);
    }
  };
}

export default class SearchForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = {request: '', isOpen: false};
    this.changeRequest = this.changeRequest.bind(this);
    this.open = this.open.bind(this);
    this.close = this.close.bind(this);
    this.handleClick = this.handleClick.bind(this);
    this.search = this.search.bind(this);
    this.debounceSearch = debounce(this.debounceSearch.bind(this), 500);
  }

  search() {
    const currentLocation = this.props.history.location.pathname.split('/');
    switch (currentLocation[1]) {
      case "interviewees":
        this.props.history.replace('/interviewees?q=' + this.state.request);
        break;
      case "students":
        this.props.history.replace('/students?q=' + this.state.request);
        break;
      case "trainees":
        this.props.history.replace('/trainees?q=' + this.state.request);
        break;
      default:
        this.props.history.replace('/?q=' + this.state.request);
    }
  }

  debounceSearch() {
    this.search();
  }

  open() {
    this.setState({isOpen: true});
  }

  close() {
    this.props.history.replace(this.props.history.location.pathname);
    this.setState({isOpen: false});
  }

  handleClick() {
    if (this.state.isOpen) {
      this.search();
    } else {
      this.open();
    }
  }

  changeRequest(value) {
    this.setState({request: value});
    this.debounceSearch();
  }

  componentWillMount() {
    if (!this.state.isOpen) {
      this.props.history.replace(this.props.history.location.pathname);
    }
  }

  render() {
    return (
      <Form>
        <IconButton
          color="contrast"
          icon={<SearchIcon/>}
          onClick={this.handleClick}
          placeholder="search"
          defaultValue={this.state.request}
        />
        {
          this.state.isOpen ?
            <Input
              onChange={this.changeRequest}
              className="search"
              placeholder="search"
              onBlur={() => {if (this.state.request === '') this.close()}}
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
};

const Form = styled.div`
  display: flex;
  align-items: center;
`;