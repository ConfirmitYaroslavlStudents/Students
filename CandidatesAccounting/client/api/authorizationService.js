export function login(username, password) {
  return fetch('/login',
    {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({username: username, password: password})
    })
    .then((response) => {
      if (response.status === 200) {
        return true;
      } else {
        throw response.status;
      }
    });
}

export function logout() {
  return fetch('/logout',
    {
      method: 'GET'
    })
    .then((response) => {
      if (response.status === 200) {
        return true;
      } else {
        throw response.status;
      }
    });
}