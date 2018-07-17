import React from 'react'
import PropTypes from 'prop-types'
import Avatar from '@material-ui/core/Avatar'

const LetterAvatar = (props) => {
  const { letters, invertedColors } = props

  const colorClassName = invertedColors ? 'letter-avatar-inverted' : 'letter-avatar'

  return (
    <Avatar className={colorClassName}>
      {letters.toUpperCase()}
    </Avatar>
  )
}

LetterAvatar.propTypes = {
  letters: PropTypes.string.isRequired,
  invertedColors: PropTypes.bool
}

export default LetterAvatar
