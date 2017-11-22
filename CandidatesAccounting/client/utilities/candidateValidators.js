export function checkCandidateValidation(candidate) {
  return (checkName(candidate.name) && checkEmail(candidate.email));
}

export function checkName(name) {
  return (name && name.trim() !== '');
}

export function checkEmail(email) {
  const validEmail = /.+@.+\..+/i;
  return (email && validEmail.test(email));
}