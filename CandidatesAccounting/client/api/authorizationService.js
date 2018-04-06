export function getUsername() {
  return fetch('/login',
    {
      method: 'GET',
      credentials: 'include',
    })
  .then((response) => {
    if (response.status === 200) {
      return response.json()
      .then((data) => {
        return data.username
      });
    } else {
      throw response.status
    }
  })
}

export function login(username, password) {
  return fetch('/login',
    {
      method: 'POST',
      credentials: 'include',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({username: username, password: password})
    })
    .then((response) => {
      if (response.status === 200) {
        return response.json()
          .then((data) => {
            return data.username
          });
      } else {
        throw response.status
      }
    })
}

export function logout() {
  return fetch('/logout',
    {
      method: 'GET',
      credentials: 'include',
    })
    .then((response) => {
      if (response.status === 200) {
        return true
      } else {
        throw response.status
      }
    })
}