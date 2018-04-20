import React from 'react'
import PropTypes from 'prop-types'
import { TagWrapper, Tag } from '../common/styledComponents'

export default function TagList(props) {
  const handleTagClick = tag => () => {
    props.setSearchRequest(tag)
    props.search(props.history)
  }

  return (
    <TagWrapper>
      {props.tags.map((tag, index) =>
        <Tag key={index} onClick={handleTagClick(tag)}>
          {tag}
        </Tag>
      )}
    </TagWrapper>
  )
}

TagList.propTypes = {
  tags: PropTypes.array.isRequired,
  history: PropTypes.object.isRequired,
  setSearchRequest: PropTypes.func.isRequired,
  search: PropTypes.func.isRequired,
}