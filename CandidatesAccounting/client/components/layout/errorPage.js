import React from 'react'
import PropTypes from 'prop-types';
import styled from 'styled-components';

export default function ErrorPage(props) {
  return (
    <Wrapper>
      <CodeWrapper>{props.errorCode}</CodeWrapper>
      <MessageWrapper>{props.errorMessage}</MessageWrapper>
    </Wrapper>
  );
}

ErrorPage.propTypes = {
  errorCode: PropTypes.number,
  errorMessage: PropTypes.string,
};

const Wrapper = styled.div`
  width: 100%;
  min-height: 100vmin;
  background: #EEE;
  position: absolute;
  top: 0;
  padding-top: 160px;
  box-sizing: border-box;
  text-align: center;
`;

const CodeWrapper = styled.div`
  display: inline-block;
  font-size: 220%;
  color: #666;
  margin-right: 10px;
`;

const MessageWrapper = styled.div`
  display: inline-block;
  font-size: 150%;
  color: #888;
`;