import React from 'react';
import styled from 'styled-components';

export default function Tag(props) {
  return(
    <TagWrapper>
      {props.children}
    </TagWrapper>
  );
}

const TagWrapper = styled.div`
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

