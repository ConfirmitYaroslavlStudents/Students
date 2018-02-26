export function downloadResume(fileName) {
  return fetch('/resume/' + fileName,
    {
      method: 'GET',
      credentials: 'include',
      headers: {
        'ContentDisposition': 'attachment; filename=' + fileName // TODO: need better header
      }
    })
    .then((response) => {
      if (response.status === 200) {
        console.log(response.body);
        return response.body;
      } else {
        throw response.status;
      }
    });
}