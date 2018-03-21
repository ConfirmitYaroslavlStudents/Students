import React from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';

export default function TagList(props) {
  return (
    <Wrapper>
      {props.tags.map((tag, index) => (<Tag key={index} onClick={() => {
        props.loadCandidates(
          {
            applicationStatus: 'refreshing',
            searchRequest: tag
          },
          props.history
        );
      }}>{tag}</Tag>))}
    </Wrapper>
  )
}

TagList.propTypes = {
  tags: PropTypes.array.isRequired,
  loadCandidates: PropTypes.func.isRequired,
  history: PropTypes.object.isRequired,
};

const Wrapper = styled.div`
  display: inline-block;
  padding-top: 3px;
  min-width: 300px;
`;

const Tag = styled.div`
  cursor: pointer;
  display: inline-block;
  padding: 0px 4px;
  background: #f2f9fc;
  border: 1px solid #c9e6f2;
  border-radius: 2px;
  color: #08c;
  vertical-align: middle;
  margin-left: 3px;
  margin-bottom: 3px;
  &:hover {
    color: #2196F3;
  }
`;