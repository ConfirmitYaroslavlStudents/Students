import { getCandidateById, updateCandidate } from './candidate'

export const getResume = (candidateId) => {
  return getCandidateById(candidateId)
  .then(interviewee => {
    return {
      resumeName: interviewee ? interviewee.resume : null,
      resumeFile: interviewee ? interviewee.resumeFile : null
    }
  })
}

export const addResume = (candidateId, resumeFile) => {
  return updateCandidate(candidateId, { resume: resumeFile.name, resumeFile: resumeFile.data })
}