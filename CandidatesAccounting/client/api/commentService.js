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
  });
}

export function deleteComment(candidateID, comment) {
  return sendGraphQLQuery(
    `mutation deleteComment($candidateID: ID!, $comment: CommentInput!) {
      deleteComment(
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
    if (!data.deleteComment) {
      throw 'Server error';
    }
  });
}