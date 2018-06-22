import React from 'react'
import PropTypes from 'prop-types'
import formatUserName from '../../utilities/formatUserName'
import styled from 'styled-components'

export default function UsernameWrapper(props) {
  return (
    <AppbarUsernameWrapper>
      {formatUserName(props.content)}
    </AppbarUsernameWrapper>
  )
}

UsernameWrapper.propTypes = {
  content: PropTypes.string.isRequired
}

const AppbarUsernameWrapper = styled.span`
  margin: 0 5px;
`