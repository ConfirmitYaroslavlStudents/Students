import React from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import {NavLink} from 'react-router-dom';
import Tag from './tag';

export default function TagList(props) {
  return (
    <TagsWrapper>
      {props.tags.map((tag, index) => (<NavLink to={props.currentLocation + "/tag/" + encodeURIComponent(tag)} key={index}><Tag>{tag}</Tag></NavLink>))}
    </TagsWrapper>
  )
}

TagList.propTypes = {
  tags: PropTypes.array.isRequired,
  currentLocation: PropTypes.string.isRequired,
};

const TagsWrapper = styled.div`
  display: inline-block;
  min-width: 300px;
`;