export function checkCandidateValidation(candidate) {
  return (isNotEmpty(candidate.name))
}

export function isNotEmpty(name) {
  return (name && name.trim() !== '')
}

export function isEmail(email) {
  const validEmail = /.+@.+\..+/i;
  return (email && validEmail.test(email))
}