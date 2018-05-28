import convertArrayToDictionary from './convertArrayToDictionary'

export default class Candidate {
  constructor(status, candidateProperties) {
    const properties = candidateProperties ? candidateProperties : {}
    this.status = status
    this.id = properties.id ? properties.id : 'noId'
    this.name = properties.name ? properties.name : ''
    this.nickname = properties.nickname ? properties.nickname : ''
    this.phoneNumber = properties.phoneNumber ? properties.phoneNumber : ''
    this.email = properties.email ? properties.email : ''
    this.commentAmount =
      properties.commentAmount ?
        properties.commentAmount
        :
        properties.comments ?
          properties.comments.length
          :
          0
    this.tags = properties.tags ? properties.tags.slice() : []
    this.subscribers = properties.subscribers ? convertArrayToDictionary(properties.subscribers) : {}

    switch (status) {
      case 'Interviewee':
        this.interviewDate = properties.interviewDate ? properties.interviewDate : ''
        this.resume = properties.resume ? properties.resume : ''
        break
      case 'Student':
        this.groupName = properties.groupName ? properties.groupName : ''
        this.startingDate = properties.startingDate ? properties.startingDate : ''
        this.endingDate = properties.endingDate ? properties.endingDate : ''
        break
      case 'Trainee':
        this.mentor = properties.mentor ? properties.mentor : ''
    }
  }
}

