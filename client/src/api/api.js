const url = 'https://localhost:44336/';
function getTodos() {
  return fetch(`${url}api/todo/index`, {
    mode: 'cors',
  }).then(
    (res) => res.json(),
  );
}
export default getTodos;
