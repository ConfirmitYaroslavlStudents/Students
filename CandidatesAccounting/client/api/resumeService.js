export function uploadResume(intervieweeId, resume) {
  const formData = new FormData()
  formData.append('resume', resume)

  return fetch('/interviewees/' + intervieweeId + '/resume',
    {
      method: 'POST',
      credentials: 'include',
      body: formData
    })
    .then(response => {
      if (response.status === 200) {
        return true
      } else {
        throw response.status
      }
    })
}