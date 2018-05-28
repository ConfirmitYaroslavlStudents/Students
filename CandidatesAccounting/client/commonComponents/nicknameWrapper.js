import React from 'react'
import PropTypes from 'prop-types'
import styled from 'styled-components'

export default function NicknameWrapper(props) {
  const { nickname } = props
  if (nickname.trim() === '') {
    return <span />
  } else {
    return <Wrapper>({nickname})</Wrapper>
  }
}

NicknameWrapper.propTypes = {
  nickname: PropTypes.string.isRequired
}

const Wrapper = styled.span`
  margin-left: 4px;
  opacity: 0.9;
`