export default function createCandidateUpdateMessage(differences) {
  let message = '<p>' + 'Candidate has been updated:' + '</p>'
  for (const property in differences)
  {
    const previousState = differences[property].previousState
    const newState = differences[property].newState
    if (!previousState) {
      message += '<p>' + '+ ' + getPropertyFullName(property) + ': ' +  newState + '</p>'
      continue
    }
    if (!newState) {
      message += '<p>' + '- ' + getPropertyFullName(property) + ': ' +  previousState + '</p>'
      continue
    }
    message += '<p>' + getPropertyFullName(property) + ': ' +  differences[property].previousState + ' -> ' + differences[property].newState + '</p>'
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