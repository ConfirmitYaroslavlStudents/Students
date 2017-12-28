export function sendGraphQLQuery(query) {
  return fetch('/graphql',
    {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Authorization': 'ntlm'
      },
      body: JSON.stringify({query: query})
    })
    .then((response) => {
      if (response.status === 200) {
        return response.json()
          .then((response) => {
            return response.data;
          });
      } else {
        throw response.status;
      }
    });
}

export function sendGraphQLMutation(query, variables) {
  return fetch('/graphql',
    {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Authorization': 'ntlm'
      },
      body: JSON.stringify({
        query: query,
        variables: variables
      })
    })
    .then((response) => {
      if (response.status === 200) {
        return response.json()
          .then((response) => {
            return response.data;
          });
      } else {
        throw response.status;
      }
    });
}