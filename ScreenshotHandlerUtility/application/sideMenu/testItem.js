import React from 'react'
import PropTypes from 'prop-types'
import styled, { css } from 'styled-components'
import Tooltip from '../commonComponents/UIDecorators/tooltip'
import { CheckIcon, iconModifiers } from 'confirmit-icons-material'

const SideMenuTestItem = (props) => {
  const { test, onClick, highlighted } = props

  const handleClick = (e) => {
    onClick(test)
    e.stopPropagation()
  }

  return (
    <Tooltip title='Open the test view' placement='right'>
      <ItemWrapper onClick={handleClick} highlighted={highlighted}>
        <InfoWrapper bold={test.unread}>
          <Index>{test.index + 1}.</Index> {test.testName} №{test.number} {test.screenshotName ? ` (${test.screenshotName})` : ''} ({test.browserName})
        </InfoWrapper>
        {
          test.markedToUpdate && (
            <IconWrapper>
              <CheckIcon className='primary-icon' size={iconModifiers.size.size20px} />
            </IconWrapper>
          )
        }
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
  border-top: 1px solid #ddd;
  cursor: pointer;
  opacity: 0.90;
  
  &:hover {
    background-color: #f8f8f8;
  }
	
	${props => props.highlighted && css`
    border-right: 6px solid #7CB342;
    padding-right: 6px;
	`}  
`

const InfoWrapper = styled.p`
  ${props => props.bold && css`    
    font-weight: bold;
	`}	
`

const IconWrapper = styled.div`
  display: inline-flex;
  margin: auto 0 auto auto;
  padding: 0 0 0 4px;
`

const Index = styled.span`
  opacity: 0.85;
`