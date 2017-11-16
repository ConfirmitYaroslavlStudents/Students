import React from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import SearchIcon from 'material-ui-icons/search';
import Input from 'material-ui/Input';
import IconButton from './UIComponentDecorators/iconButton';

export default class SearchForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = {request: '', isOpen: false};
    this.toggleOpen = this.toggleOpen.bind(this);
    this.changeRequest = this.changeRequest.bind(this);
    this.handleClick = this.handleClick.bind(this);
  }

  handleClick() {
    if (this.state.isOpen && this.state.request.trim() !== '') {
      const currentLocation = this.props.history.location.pathname.split('/');
      switch (currentLocation[1]) {
        case "search":
          this.props.history.replace('/search/' + this.state.request);
          break;
        case "interviewees":
          this.props.history.replace('/interviewees/search/' + this.state.request);
          break;
        case "students":
          this.props.history.replace('/students/search/' + this.state.request);
          break;
        case "trainees":
          this.props.history.replace('/trainees/search/' + this.state.request);
          break;
        default:
          this.props.history.replace('/search/' + this.state.request);
      }
    } else {
      this.toggleOpen();
    }
  }

  toggleOpen() {
    if (this.state.isOpen) {
      this.setState({isOpen: false});
    } else {
      this.setState({isOpen: true});
    }
  }

  changeRequest(value) {
    this.setState({request: value});
  }

  render() {
    const self = this;
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
              classes={{root: 'search'}}
              disableUnderline
              onChange={(event) => {self.changeRequest(event.target.value)}}
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