export default function createCandidateUpdateMessage(differences) {
  let message = 'Candidate has been updated:'
  for (const property in differences)
  {
    const previousState = differences[property].previousState
    const newState = differences[property].newState
    message += '</br>'
    if (!previousState) {
      message += '+ ' + getPropertyFullName(property) + ': ' +  newState
      continue
    }
    if (!newState) {
      message += '- ' + getPropertyFullName(property) + ': ' +  previousState
      continue
    }
    message += getPropertyFullName(property) + ': ' +  differences[property].previousState + ' -> ' + differences[property].newState
  }
  return message
}

function getPropertyFullName(property) {
  switch (property) {
    case 'status':
      return 'status'
    case 'name':
      return 'name'
    case 'phoneNumber':
      return 'phone number'
    case 'email':
      return 'email'
    case 'interviewDate':
      return 'interview date'
    case 'resume':
      return 'resume'
    case 'groupName':
      return 'group name'
    case 'startingDate':
      return 'learning start'
    case 'endingDate':
      return 'learning end'
    case 'mentor':
      return 'mentor'
    default:
      return property
  }
}