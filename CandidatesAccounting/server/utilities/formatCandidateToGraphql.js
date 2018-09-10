const formatCandidateToGraphql = (candidate) => {
  candidate.id = candidate._id
  delete candidate._id
  candidate.commentAmount = candidate.comments.length
  delete candidate.comments
  return candidate
}

export default formatCandidateToGraphql