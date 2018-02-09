import sendGraphQLQuery from './graphqlClient';

export function addComment(candidateID, comment) {
  return sendGraphQLQuery(
    `mutation addComment($candidateID: ID!, $comment: CommentInput!) {
      addComment(
        candidateID: $candidateID,
        comment: $comment
      )
    }`,
    {
      candidateID: candidateID,
      comment: comment
    }
  )
  .then((data) => {
    if (!data.addComment) {
      throw 'Server error';
    }
    return data.addComment;
  });
}

export function deleteComment(candidateID, commentID) {
  return sendGraphQLQuery(
    `mutation deleteComment($candidateID: ID!, $commentID: ID!) {
      deleteComment(
        candidateID: $candidateID,
        commentID: $commentID
      )
    }`,
    {
      candidateID: candidateID,
      commentID: commentID
    }
  )
  .then((data) => {
    if (!data.deleteComment) {
      throw 'Server error';
    }
  });
}