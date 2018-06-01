import React from 'react'
import PropTypes from 'prop-types'
import Avatar from '@material-ui/core/Avatar'

export default function LetterAvatar(props) {
  const { letters, invertedColors } = props

  return (
    <Avatar className={invertedColors ? 'letter-avatar-inverted' : 'letter-avatar'}>
      {letters.toUpperCase()}
    </Avatar>
  )
}

LetterAvatar.propTypes = {
  letters: PropTypes.string.isRequired,
  invertedColors: PropTypes.bool
}