export function fetchGet(url) {
  return fetch(url)
    .then(function(response){
      if (response.status === 200) {
        return response.json();
      } else {
        throw response.status;
      }
    });
}

export function fetchPost(url, data) {
  return fetch(url,
    {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({data: data})
    })
    .then(function(response){
      if (response.status === 200) {
        return response;
      } else {
        throw response.status;
      }
    });
}

export function fetchPut(url, data) {
  return fetch(url,
    {
      method: "PUT",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({data: data})
    })
    .then(function(response){
      if (response.status === 200) {
        return response;
      } else {
        throw response.status;
      }
    });
}

export function fetchDelete(url) {
  return fetch(url,
    {
      method: "DELETE"
    })
    .then(function(response){
      if (response.status === 200) {
        return response;
      } else {
        throw response.status;
      }
    });
}

module.exports = {fetchGet, fetchPost, fetchPut, fetchDelete};
