export default function checkCandidateValidation(candidate) {
  return (checkName(candidate.name) && checkEmail(candidate.email));
}

function checkName(name) {
  return (name && name.trim() !== '');
}

function checkEmail(email) {
  const validEmail = /.+@.+\..+/i;
  return (email && validEmail.test(email));
}