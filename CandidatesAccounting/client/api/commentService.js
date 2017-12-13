import {sendGraphQLMutation} from './graphqlClient';

export function addComment(candidateID, comment) {
  return sendGraphQLMutation(
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
  return sendGraphQLMutation(
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