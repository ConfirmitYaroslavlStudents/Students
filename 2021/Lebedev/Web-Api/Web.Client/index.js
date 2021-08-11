const url = 'http://localhost:5000/todolist';
let toDoList = [];

function GetToDoList() 
{
  fetch(url)
    .then(response => response.json())
    .then(list => DisplayToDoList(list));
}

function AddItem() 
{
  const addNameTextbox = document.getElementById('AddName');

  const item = 
  {
    id: 0,
    completed: false,
    deleted: false,
    name: addNameTextbox.value.trim()
  };

  fetch(url, 
    {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(item)
  })
    .then(response => response.json())
    .then(() => 
    {
      GetToDoList();
      addNameTextbox.value = '';
    });
}

function DeleteItem(id) 
{
  fetch(`${url}/${id}`, 
  {
    method: 'DELETE'
  })
  .then(() => GetToDoList());
}

function ShowEdit(id) 
{
  const item = toDoList.find(item => item.id === id);
  
  document.getElementById('EditName').value = item.name;
  document.getElementById('EditId').value = item.id;
  document.getElementById('EditDelete').checked = item.deleted,
  document.getElementById('EditComplete').checked = item.completed;
  document.getElementById('EditForm').style.display = 'block';
}

function PatchItem() 
{
  const itemId = document.getElementById('EditId').value;
  const item = 
  {
    id: parseInt(itemId, 10),
    name: document.getElementById('EditName').value.trim(),
    completed: document.getElementById('EditComplete').checked,
    deleted: document.getElementById('EditDelete').checked
  };

  fetch(`${url}`, 
  {
    method: 'PATCH',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(item)
  })
  .then(() => GetToDoList());

  HideEdit();
}

function HideEdit() 
{
  document.getElementById('EditForm').style.display = 'none';
}

function DisplayToDoList(list) 
{
  const tableBody = document.getElementById('ToDoTable');
  tableBody.innerHTML = '';
  toDoList = list;

  list.forEach(item => 
  {
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
    editButton.setAttribute('onclick', `ShowEdit(${item.id})`);
    tr.insertCell().appendChild(editButton);

    let deleteButton = document.createElement('button');
    deleteButton.innerText = 'Delete';
    deleteButton.setAttribute('onclick', `DeleteItem(${item.id})`);
    tr.insertCell().appendChild(deleteButton);
  });
}