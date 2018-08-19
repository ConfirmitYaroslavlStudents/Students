import React from 'react'
import PropTypes from 'prop-types'
import styled from 'styled-components'

const NicknameWrapper = (props) => {
  const { nickname } = props

  if (nickname.trim() === '') {
    return null
  }

  return <Wrapper>({nickname})</Wrapper>
}

NicknameWrapper.propTypes = {
  nickname: PropTypes.string.isRequired
}

export default NicknameWrapper

const Wrapper = styled.span`
  margin-left: 4px;
  opacity: 0.9;
`