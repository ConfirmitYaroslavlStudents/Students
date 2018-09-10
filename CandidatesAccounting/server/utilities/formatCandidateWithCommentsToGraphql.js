const formatCandidateWithCommentsToGraphql = (candidate) => {
  candidate.id = candidate._id
  delete candidate._id
  candidate.comments.forEach(comment => {
    comment.id = comment._id
    delete comment._id
  })
  return candidate
}

export default formatCandidateWithCommentsToGraphql