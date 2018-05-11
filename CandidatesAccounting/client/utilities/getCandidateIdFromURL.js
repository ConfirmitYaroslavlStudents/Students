export default function getCandidateIdFromURL(url) {
  const splitedURL = url.split('?')

  const path = splitedURL[0]
  const splitedPath = path.split('/')
  let candidateId = null
  if (splitedPath[3] && splitedPath[3] === 'comments') {
    candidateId = splitedPath[2]
  }

  return candidateId
}