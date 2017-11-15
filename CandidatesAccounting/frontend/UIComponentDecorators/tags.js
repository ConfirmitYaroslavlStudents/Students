import React from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import { NavLink } from 'react-router-dom';
import Tag from './tag';

export default function Tags(props) {
  return (
    <TagsWrapper>
      {props.tags.map((tag, index) => (<NavLink to={"/tag/" + encodeURIComponent(tag)} key={index}><Tag>{tag}</Tag></NavLink>))}
    </TagsWrapper>
  )
}

Tags.propTypes = {
  tags: PropTypes.array.isRequired,
};

const TagsWrapper = styled.div`
  display: inline-block;
  min-width: 300px;
`;