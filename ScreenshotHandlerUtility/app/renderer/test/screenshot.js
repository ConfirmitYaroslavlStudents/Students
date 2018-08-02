import React from 'react'
import PropTypes from 'prop-types'
import styled, { css } from 'styled-components'
import OpenableImage from '../common/openableImage'

const Screenshot = (props) => {
  const { source, borderColor } = props

  return (
    <Wrapper>
      <FrameWrapper borderColor={borderColor}>
        <OpenableImage source={source} alternativeText={'no screenshot'} width={'100%'} />
      </FrameWrapper>
    </Wrapper>
  )
}

Screenshot.propTypes = {
  source: PropTypes.string.isRequired,
  borderColor: PropTypes.string
}

export default Screenshot

const Wrapper = styled.div`
  margin: 0 8px;
`

const FrameWrapper = styled.div`
  display: inline-block;
  border: 3px solid #BDBDBD;
  border-radius: 3px;
  
  ${props => props.borderColor && css`
    border-color: ${props.borderColor};
	`}
`