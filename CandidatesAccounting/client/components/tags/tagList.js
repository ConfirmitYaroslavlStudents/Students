import React from 'react'
import PropTypes from 'prop-types'
import { TagWrapper, Tag } from '../common/styledComponents'

export default function TagList(props) {
  const handleTagClick = tag => () => {
    props.loadCandidates(
      {
        applicationStatus: 'refreshing',
        searchRequest: tag,
        history,
      }
    )
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
  loadCandidates: PropTypes.func.isRequired,
  history: PropTypes.object.isRequired,
}