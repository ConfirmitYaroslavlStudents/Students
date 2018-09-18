import { getCandidateById, updateCandidate } from './candidate'

export const getAvatar = (candidateId) => {
  return getCandidateById(candidateId)
  .then(candidate => {
    return {
      avatarFile: candidate ? candidate.avatar : null
    }
  })
}

export const addAvatar = (candidateId, avatarFile) => {
  return updateCandidate(candidateId, { avatar: avatarFile.data, hasAvatar: true })
}