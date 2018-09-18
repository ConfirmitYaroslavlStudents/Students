const formatCandidateWithCommentsToGraphql = (candidate) => {
  candidate.id = candidate._id.toString()
  delete candidate._id
  candidate.comments.forEach(comment => {
    comment.id = comment._id.toString()
    delete comment._id
  })
  return candidate
}

export default formatCandidateWithCommentsToGraphql