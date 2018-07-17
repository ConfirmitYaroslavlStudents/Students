import React from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../actions'
import styled from 'styled-components'

const TagList = (props) => {
  const handleTagClick = tag => () => {
    props.searchByTag({ tag })
  }

  return (
    <TagWrapper>
      {props.candidateTags.map((tag, index) =>
        <Tag key={index} onClick={handleTagClick(tag)}>
          {tag}
        </Tag>
      )}
    </TagWrapper>
  )
}

TagList.propTypes = {
  candidateTags: PropTypes.array.isRequired,
  searchByTag: PropTypes.func.isRequired
}

export default connect(() => ({}), actions)(TagList)

const TagWrapper = styled.div`
  display: inline-block;
  padding-top: 3px;
  min-width: 300px;
`

const Tag = styled.div`
  cursor: pointer;
  display: inline-block;
  padding: 0px 4px;
  background: #f2f9fc;
  border: 1px solid #BBDEFB;
  border-radius: 2px;
  color: #1976D2;
  vertical-align: middle;
  margin: 0px 2px 3px 2px;
  &:hover {
    color: #1E88E5;
    border-color: #64B5F6;
  }
`