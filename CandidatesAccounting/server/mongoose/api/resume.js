import { getCandidateById, updateCandidate } from './candidate'

export const getResume = (candidateId) => {
  return getCandidateById(candidateId)
  .then(interviewee => {
    return {
      resumeName: interviewee.resume,
      resumeFile: interviewee.resumeFile
    }
  })
}

export const addResume = (candidateId, resumeFile) => {
  return updateCandidate(candidateId, { resume: resumeFile.name, resumeFile: resumeFile.data })
}