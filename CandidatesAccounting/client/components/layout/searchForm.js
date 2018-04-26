import React, { Component } from 'react'
import { connect } from 'react-redux'
import * as actions from '../../actions/actions'
import SearchIcon from 'material-ui-icons/search'
import Input from '../common/UIComponentDecorators/input'
import IconButton from '../common/UIComponentDecorators/iconButton'

class SearchForm extends Component {
  constructor(props) {
    super(props)
    this.state = { isOpen: props.searchRequest !== '' }
    this.timer = null
  }

  handleOpen = () => {
    this.setState({ isOpen: true })
  }

  handleClose = () => {
    this.setState({ isOpen: false })
  }

  handleClick = () => {
    if (this.props.searchRequest !== '' && this.state.isOpen) {
      this.timer = null
      this.search(this.props.searchRequest)
    } else {
      if (this.state.isOpen) {
        this.handleClose()
      } else {
        this.handleOpen()
      }
    }
  }

  handleChange = (searchRequest) => {
    this.props.setSearchRequest({ searchRequest })
    if (this.timer) {
      clearTimeout(this.timer)
    }
    this.timer = setTimeout(() => {
      if (this.timer) {
        this.search(searchRequest)
      }
      this.timer = null
    }, 900)
  }

  search = searchRequest => {
    this.props.search({ searchRequest })
  }

  render() {
    const { searchRequest } = this.props

    const searchInput =
      this.state.isOpen || searchRequest !== '' ?
        <Input
          id='search-input'
          onChange={this.handleChange}
          value={searchRequest}
          className='search'
          placeholder='search'
          onFocus={this.handleOpen}
          onBlur={() => {
            if (searchRequest === '') {
              this.handleClose()
            }
          }}
          disableUnderline
          autoFocus
        />
        : ''

    return (
      <div className='flex centered'>
        <IconButton
          color='inherit'
          icon={<SearchIcon/>}
          onClick={this.handleClick}
          placeholder='search'
        />
        { searchInput }
      </div>
    )
  }
}

export default connect(state => {
  return {
    searchRequest: state.searchRequest
  }
}, actions)(SearchForm)