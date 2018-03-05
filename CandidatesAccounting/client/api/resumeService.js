export function uploadResume(intervieweeID, resume) {
  let formData = new FormData();
  formData.append('resume', resume);

  return fetch('/interviewees/' + intervieweeID + '/resume',
    {
      method: 'POST',
      credentials: 'include',
      body: formData
    })
    .then((response) => {
      if (response.status === 200) {
        return true;
      } else {
        throw response.status;
      }
    });
}