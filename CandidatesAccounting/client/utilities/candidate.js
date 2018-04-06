export default class Candidate {
  constructor(status, candidateProperties) {
    const properties = candidateProperties ? candidateProperties : {}
    this.status = status
    this.id = properties.id ? properties.id : ''
    this.name = properties.name ? properties.name : ''
    this.birthDate = properties.birthDate ? properties.birthDate : ''
    this.email = properties.email ? properties.email : ''
    this.comments = properties.comments ? commentArrayToCommentDictionary(properties.comments) : {}
    this.tags = properties.tags ? properties.tags.slice() : []
    this.subscribers = properties.subscribers ? subscriberArrayToSubscriberDictionary(properties.subscribers) : {}

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

function commentArrayToCommentDictionary(commentArray) {
  if (commentArray instanceof Array) {
    const commentDictionary = {}
    commentArray.forEach(comment => {
      commentDictionary[comment.id] = comment
    });
    return commentDictionary
  } else {
    return commentArray
  }
}

function subscriberArrayToSubscriberDictionary(subscriberArray) {
  if (subscriberArray instanceof Array) {
    let subscriberDictionary = {}
    subscriberArray.forEach(email => {
      subscriberDictionary[email] = email
    })
    return subscriberDictionary
  } else {
    return subscriberArray
  }
}