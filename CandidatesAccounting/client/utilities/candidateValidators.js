export const checkCandidateValidation = (candidate) => {
  return (isNotEmpty(candidate.name))
}

export const isNotEmpty = (name) => {
  return (name && name.trim() !== '')
}

export const isEmail = (email) => {
  const validEmail = /.+@.+\..+/i
  return (email && validEmail.test(email))
}