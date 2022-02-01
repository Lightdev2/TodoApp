const url = 'https://localhost:44336/';

function getTodos() {
  return fetch(`${url}api/todo/index`, {
    mode: 'cors',
  })
    .then(
      (res) => res.json(),
    );
}

export function signUpAsync(user) {
  return fetch(`${url}api/auth/register`, {
    mode: 'cors',
    method: 'Post',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(user),
  })
    .then(
      (res) => res.json(),
    );
}

export function signInAsync(user) {
  return fetch(`${url}api/auth/login`, {
    mode: 'cors',
    method: 'Post',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(user),
  })
    .then(
      (res) => res.json(),
    );
}

export default {
  signUpAsync,
  getTodos,
  signInAsync,
};
