export function uploadAvatar(candidateStatus, candidateId, avatar) {
  const formData = new FormData()
  formData.append('avatar', avatar)

  return fetch('/' + candidateStatus + '/' + candidateId + '/avatar',
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