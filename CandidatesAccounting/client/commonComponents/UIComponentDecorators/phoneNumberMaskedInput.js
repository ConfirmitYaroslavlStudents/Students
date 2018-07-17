import React from 'react'
import PropTypes from 'prop-types'
import MaskedInput from 'react-text-mask'

const PhoneNumberMaskedInput = (props) => {
  const { inputRef, ...other } = props

  return (
    <MaskedInput
      {...other}
      ref={inputRef}
      mask={['+', '7', ' ', /\d/, /\d/, /\d/, ' ', /\d/, /\d/, /\d/, '-', /\d/, /\d/, '-', /\d/, /\d/]}
      placeholderChar={'\u2000'}
      showMask
    />
  )
}

PhoneNumberMaskedInput.propTypes = {
  inputRef: PropTypes.func.isRequired
}

export default PhoneNumberMaskedInput