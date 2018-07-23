import React from 'react'
import PropTypes from 'prop-types'
import styled from 'styled-components'
import DoneIcon from '@material-ui/icons/Done'

const NoFallenTestsPage = (props) => {
  return (
    <Wrapper>
      <MessageWrapper>
        <p>All tests ({props.totalTestAmount}) have passed successfully!</p>
        <DoneIcon style={{ color: '#29B6F6', width: 100, height: 100 }}/>
      </MessageWrapper>
    </Wrapper>
  )
}

NoFallenTestsPage.propTypes = {
  totalTestAmount: PropTypes.number.isRequired
}

export default NoFallenTestsPage

const Wrapper = styled.div`
  display: flex;
  align-items: center;
  width: 100%;
  height: 100%;
  padding-top: 300px;
`

const MessageWrapper = styled.div`
  display: inline-block;
  margin: auto;
  color: #29B6F6;
  font-size: 190%;
  text-align: center;
`