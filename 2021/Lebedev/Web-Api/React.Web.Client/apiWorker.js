const url = 'http://localhost:5000/todolist';

async function getToDoListUpdate() {
  const response = await fetch(url);
  const list = await response.json();
  return list;
}

async function sendDelete(id) {
  await fetch(`${url}/${id}`, {
    method: 'DELETE'
  })
  return await getToDoListUpdate();
}

async function sendAddItem(itemName, container) {
  const item = {
    id: 0,
    completed: false,
    deleted: false,
    name: itemName
  };
  
  await fetch(url,{
    method: 'POST',
    headers: {
    'Accept': 'application/json',
    'Content-Type': 'application/json'
    },
    body: JSON.stringify(item)
    })
  return await getToDoListUpdate();
}

async function sendPatchReqest(item, container) {
  await fetch(`${url}`, {
    method: 'PATCH',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(item)
  });
  return await getToDoListUpdate();
}

function hideEdit() {
  document.getElementById('EditForm').style.display = 'none';
}

async function start() {
  const domContainer = document.getElementById('ToDOListContainer');
  const response = await fetch(url)
  const list = await response.json();
  ReactDOM.render(create(ToDoListPrinter,{toDoList: list}), domContainer);
}