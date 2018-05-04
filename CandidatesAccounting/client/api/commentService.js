import sendGraphQLQuery from './graphqlClient'

export function addComment(candidateID, comment) {
  return sendGraphQLQuery(
    `mutation addComment($candidateID: ID!, $comment: CommentInput!) {
      addComment(
        candidateID: $candidateID,
        comment: $comment
      )
    }`,
    { candidateID, comment }
  )
  .then(data => {
    if (!data.addComment) {
      throw 'Server error'
    }
    return data.addComment
  })
}

export function deleteComment(candidateID, commentID) {
  return sendGraphQLQuery(
    `mutation deleteComment($candidateID: ID!, $commentID: ID!) {
      deleteComment(
        candidateID: $candidateID,
        commentID: $commentID
      )
    }`,
    { candidateID, commentID }
  )
  .then(data => {
    if (!data.deleteComment) {
      throw 'Server error'
    }
  })
}

export function addCommentAttachment(candidateID, commentID, attachment) {
  let formData = new FormData()
  formData.append('attachment', attachment)

  return fetch(
    '/interviewees/' + candidateID + '/commentsActions/' + commentID + '/attachment',
    {
      method: 'POST',
      credentials: 'include',
      body: formData
    })
    .then(response => {
      if (response.status === 200) {
        return true
      } else {
        throw response.status
      }
    })
}