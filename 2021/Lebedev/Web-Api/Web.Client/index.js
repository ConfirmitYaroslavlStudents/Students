const url = 'http://localhost:5000/todolist';
let toDoList = [];

async function apiRequestSender(fetchArgs, urlEnd = '')
{
  const response = await fetch(url+urlEnd, fetchArgs)
  if(fetchArgs === undefined || fetchArgs.method === 'GET'){
    const json = await response.json();
    return json;
  }
}

async function addItem() {
  const addNameTextbox = document.getElementById('AddName');

  const item = {
    id: 0,
    completed: false,
    deleted: false,
    name: addNameTextbox.value.trim()
  };

  await apiRequestSender({
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(item)
  });
  await displayToDoList();
  addNameTextbox.value = '';
}

async function deleteItem(id) {
  await apiRequestSender({
    method: 'DELETE'
  },
  `/${id}`);
  await displayToDoList();
}

function showEdit(id) {
  const item = toDoList.find(item => item.id === id);
  
  document.getElementById('EditName').value = item.name;
  document.getElementById('EditId').value = item.id;
  document.getElementById('EditDelete').checked = item.deleted,
  document.getElementById('EditComplete').checked = item.completed;
  document.getElementById('EditForm').style.display = 'block';
}

async function patchItem() {
  const itemId = document.getElementById('EditId').value;
  const item = {
    id: parseInt(itemId, 10),
    name: document.getElementById('EditName').value.trim(),
    completed: document.getElementById('EditComplete').checked,
    deleted: document.getElementById('EditDelete').checked
  };
  await sendPatchReqest(item);
  hideEdit();
}

async function sendPatchReqest(item) {
  await apiRequestSender({
    method: 'PATCH',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(item)
  })
  await displayToDoList();
}

function hideEdit() {
  document.getElementById('EditForm').style.display = 'none';
}

async function displayToDoList() {
  list = await apiRequestSender();
  const tableBody = document.getElementById('ToDoTable');
  tableBody.innerHTML = '';
  toDoList = list;

  list.forEach(item => {
    let tr = tableBody.insertRow();

    let completedCheckbox = document.createElement('input');
    completedCheckbox.type = 'checkbox';
    completedCheckbox.disabled = true;
    completedCheckbox.checked = item.completed;
    tr.insertCell().appendChild(completedCheckbox);
    
    let deletedCheckbox = document.createElement('input');
    deletedCheckbox.type = 'checkbox';
    deletedCheckbox.disabled = true;
    deletedCheckbox.checked = item.deleted;
    tr.insertCell().appendChild(deletedCheckbox);

    let textNode = document.createTextNode(item.name);
    tr.insertCell().appendChild(textNode);

    let editButton = document.createElement('button');
    editButton.innerText = 'Edit';
    editButton.setAttribute('onclick', `showEdit(${item.id})`);
    tr.insertCell().appendChild(editButton);

    let deleteButton = document.createElement('button');
    deleteButton.innerText = 'Delete';
    deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);
    tr.insertCell().appendChild(deleteButton);
  });
}