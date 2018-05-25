import React from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../actions'
import styled from 'styled-components'

function TagList(props) {
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
  border: 1px solid #c9e6f2;
  border-radius: 2px;
  color: #08c;
  vertical-align: middle;
  margin: 0px 1px 3px 1px;
  &:hover {
    color: #2196F3;
  }
`