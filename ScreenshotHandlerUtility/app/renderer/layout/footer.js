import React from 'react'
import PropTypes from 'prop-types'
import styled, { css } from 'styled-components'

const Footer = (props) => {
  const { leftColumn, centerColumn, rightColumn } = props

  return (
    <FooterWrapper>
      <Column justify='flex-start'>
        {leftColumn}
      </Column>
      <Column justify='center'>
        {centerColumn}
      </Column>
      <Column justify='flex-end'>
        {rightColumn}
      </Column>
    </FooterWrapper>
  )
}

Footer.propTypes = {
  leftColumn: PropTypes.any,
  centerColumn: PropTypes.any,
  rightColumn: PropTypes.any
}

export default Footer

const FooterWrapper = styled.div`
  display: flex;
  align-items: center;
  background-color: #7CB342;
  color: #ffffff;
  width: 100%;
  position: absolute;
  left: 0;
  bottom: 0;
  box-sizing: border-box;
  font-weight: bold; 
  padding: 12px 24px;
`

const Column = styled.div`
  display: inline-flex;
  width: 33.3333%;
  align-items: center;
  
  ${props => props.justify && css`    
    justify-content: ${props.justify};
	`}	
`