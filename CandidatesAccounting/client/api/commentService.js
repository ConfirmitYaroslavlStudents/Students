import sendGraphQLQuery from './graphqlClient'

export function addComment(candidateId, comment) {
  return sendGraphQLQuery(
    `mutation addComment($candidateId: ID!, $comment: CommentInput!) {
      addComment(
        candidateId: $candidateId,
        comment: $comment
      )
    }`,
    { candidateId, comment }
  )
  .then(data => {
    if (!data.addComment) {
      throw 'Server error'
    }
    return data.addComment
  })
}

export function deleteComment(candidateId, commentId) {
  return sendGraphQLQuery(
    `mutation deleteComment($candidateId: ID!, $commentId: ID!) {
      deleteComment(
        candidateId: $candidateId,
        commentId: $commentId
      )
    }`,
    { candidateId, commentId }
  )
  .then(data => {
    if (!data.deleteComment) {
      throw 'Server error'
    }
  })
}

export function addCommentAttachment(candidateId, commentId, attachment) {
  let formData = new FormData()
  formData.append('attachment', attachment)

  return fetch(
    '/interviewees/' + candidateId + '/comments/' + commentId + '/attachment',
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

export function getCommentAttachmentArrayBuffer(candidateId, commentId) {
  return fetch(
    '/interviewees/' + candidateId + '/comments/' + commentId + '/attachment',
    {
      method: 'GET',
      credentials: 'include'
    })
  .then(response => {
    if (response.status === 200) {
      const reader = response.body.getReader()
      return reader.read().then(({ value }) => value.buffer)
    } else {
      throw response.status
    }
  })
}