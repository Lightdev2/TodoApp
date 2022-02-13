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
    credentials: 'same-origin',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(user),
  })
    .then(
      (res) => res.json(),
    );
}

export function getProjects(token) {
  return fetch(`${url}api/projects/all`, {
    mode: 'cors',
    method: 'POST',
    headers: {
      Authorization: `Bearer ${token}`,
    },
  })
    .then(
      (res) => res.json(),
    );
}

export function deleteProjectAsync(token, id) {
  return fetch(`${url}api/projects/`, {
    mode: 'cors',
    method: 'DELETE',
    headers: {
      Authorization: `Bearer ${token}`,
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      id,
    }),
  })
    .then(
      (res) => res.json(),
    );
}

export function createProjectAsync(token, project) {
  return fetch(`${url}api/projects/`, {
    mode: 'cors',
    method: 'Post',
    headers: {
      Authorization: `Bearer ${token}`,
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(project),
  })
    .then(
      (res) => res.json(),
    );
}

export function createTaskAsync(token, todo) {
  return fetch(`${url}api/todos/`, {
    mode: 'cors',
    method: 'Post',
    headers: {
      Authorization: `Bearer ${token}`,
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(todo),
  })
    .then(
      (res) => res.json(),
    );
}

export function getTodosInProjectAsync(token, todo) {
  return fetch(`${url}api/todos/all`, {
    mode: 'cors',
    method: 'Post',
    headers: {
      Authorization: `Bearer ${token}`,
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(todo),
  })
    .then(
      (res) => res.json(),
    );
}

export function updateTodoAsync(token, todo) {
  return fetch(`${url}api/todos/edit`, {
    mode: 'cors',
    method: 'Post',
    headers: {
      Authorization: `Bearer ${token}`,
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(todo),
  })
    .then(
      (res) => res.json(),
    );
}

export default {
  signUpAsync,
  getTodos,
  signInAsync,
  createProjectAsync,
  getTodosInProjectAsync,
  updateTodoAsync,
};
