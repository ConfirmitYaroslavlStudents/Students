export default function createCandidate(status, fields) {
  let candidate = {
    status: 'Candidate',
    id: fields.id ? fields.id : '',
    name: fields.name ? fields.name : '',
    birthDate: fields.birthDate ? fields.birthDate : '',
    email: fields.email ? fields.email : '',
    comments: fields.comments ? commentArrayToCommentDictionary(fields.comments) : {},
    tags: fields.tags ? fields.tags.slice() : [],
    subscribers: fields.subscribers ? subscriberArrayToSubscriberDictionary(fields.subscribers) : {},
  };

  switch (status) {
    case 'Interviewee':
      candidate.status = 'Interviewee';
      candidate.interviewDate = fields.interviewDate ? fields.interviewDate : '';
      candidate.resume = fields.resume ? fields.resume : '';
      break;
    case 'Student':
      candidate.status = 'Student';
      candidate.groupName = fields.groupName ? fields.groupName : '';
      candidate.startingDate = fields.startingDate ? fields.startingDate : '';
      candidate.endingDate = fields.endingDate ? fields.endingDate : '';
      break;
    case 'Trainee':
      candidate.status = 'Trainee';
      candidate.mentor = fields.mentor ? fields.mentor : '';
      break;
  }
  return candidate;
}

function commentArrayToCommentDictionary(commentArray) {
  if (commentArray instanceof Array) {
    let commentDictionary = {};
    commentArray.forEach(comment => {
      commentDictionary[comment.id] = comment;
    });
    return commentDictionary;
  } else {
    return commentArray;
  }
}

function subscriberArrayToSubscriberDictionary(subscriberArray) {
  if (subscriberArray instanceof Array) {
    let subscriberDictionary = {};
    subscriberArray.forEach(email => {
      subscriberDictionary[email] = email;
    });
    return subscriberDictionary;
  } else {
    return subscriberArray;
  }
}