import React from 'react'
import PropTypes from 'prop-types'
import styled, { css } from 'styled-components'
import Tooltip from '../commonComponents/UIDecorators/tooltip'
import TestIcon from './testIcon'

const SideMenuTestItem = (props) => {
  const { test, onClick, highlighted } = props

  const handleClick = (e) => {
    onClick(test)
    e.stopPropagation()
  }

  return (
    <Tooltip title='Open the test view'>
      <ItemWrapper onClick={handleClick}>
        <InfoWrapper bold={test.unread} highlighted={highlighted}>
          {test.index + 1}. {test.testName} №{test.number} {test.screenshotName ? ` (${test.screenshotName})` : ''}
        </InfoWrapper>
        <IconWrapper>
          <TestIcon markedToUpdate={test.markedToUpdate} />
        </IconWrapper>
      </ItemWrapper>
    </Tooltip>
  )
}

SideMenuTestItem.propTypes = {
  test: PropTypes.object.isRequired,
  onClick: PropTypes.func.isRequired,
  highlighted: PropTypes.bool
}

export default SideMenuTestItem

const ItemWrapper = styled.div`
  display: flex;
  align-items: center;
  padding: 12px;
  border-top: 1px solid #ccc;
  cursor: pointer;
  opacity: 0.95;
  
  &:hover {
    background-color: #f8f8f8;
  }  
`

const InfoWrapper = styled.p`
  ${props => props.bold && css`
    font-weight: bold;
	`}
	
	${props => props.highlighted && css`
    color: #039BE5;
	`}
	
`

const IconWrapper = styled.div`
  display: inline-flex;
  margin: auto 0 auto auto;
  padding: 0 0 0 4px;
`