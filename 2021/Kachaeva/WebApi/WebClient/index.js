const url = 'https://localhost:44314/todo';

async function displayToDoList() {
	let toDoList = await getToDoList();
    let table = getEmptyTable();
    setCaption(toDoList);
	
    for (let i = 0; i < toDoList.length; i++) {
        let task = toDoList[i];
        let row = table.insertRow();
		
        createIdCell(row, task.id);
		createTaskTextCell(row, task.id, task.text);
		createTaskStatusCell(row, task.id, task.isDone);
		createSaveCell(row,task.id);
		createDeleteCell(row, task.id);
    }
}

async function getToDoList() {
	let response = await fetch(url);
	let toDoList = await response.json();
	return toDoList;
}

function getEmptyTable(){
	let table = document.getElementById('toDoTable');
    table.innerHTML = "";
	return table;
}

function setCaption(toDoList){
	let caption = document.getElementById('toDoListCaption');
	
    if (toDoList.length == 0) {
        caption.innerHTML = "Your to do list is empty";
    }
	else{
		caption.innerHTML = "Here is your to do list:";
	}    
}

function createIdCell(row, taskId){
	let idCell = row.insertCell();
    idCell.align = 'right';
    idCell.innerHTML = taskId;
}

function createTaskTextCell(row, taskId, taskText){
	let taskTextCell = row.insertCell();
    let taskInput = document.createElement('input');
    taskInput.id = 'text' + taskId;
    taskInput.type = 'text';
    taskInput.value = taskText;
    taskTextCell.appendChild(taskInput);
}

function createTaskStatusCell(row, taskId, taskStatus){
	let taskStatusCell = row.insertCell();
    let statusCheckbox = document.createElement('input');
    statusCheckbox.id = 'status' + taskId;
    statusCheckbox.type = 'checkbox';
    statusCheckbox.checked = taskStatus;
    taskStatusCell.appendChild(statusCheckbox);
}

function createSaveCell(row, taskId){
	let saveCell = row.insertCell();
    let saveButton = document.createElement('button');
    saveButton.innerHTML = "Save changes";
    saveButton.onclick = (function () {
		editTask(taskId);
	});
    saveCell.appendChild(saveButton);
}

function createDeleteCell(row, taskId){
	let deleteCell = row.insertCell();
    let deleteButton = document.createElement('button');
    deleteButton.innerHTML = "Delete task";
    deleteButton.onclick = (function () {
        deleteTask(taskId);
	});
    deleteCell.appendChild(deleteButton);
}

async function addTask() {
    let taskBody = getTaskBodyForAdd();
	await sendPostRequest(taskBody);
	clearAddFields();
    displayToDoList();
}

function getTaskBodyForAdd(){
	let taskText = document.getElementById('taskTextInput').value;
    let taskStatus = document.getElementById('taskStatusInput').checked;
    return { text: taskText, isDone: taskStatus };
}

async function sendPostRequest(taskBody){
	await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type' : 'application/json'
        },
        body: JSON.stringify(taskBody)
    });
}

function clearAddFields(){
	document.getElementById('taskTextInput').value = "";
    document.getElementById('taskStatusInput').checked = false;
}

async function deleteTask(taskId) {
	let urlWithTaskId = getUrlWithTaskId(taskId);
	await sendDeleteRequest(urlWithTaskId);
    displayToDoList();
}

function getUrlWithTaskId(taskId){
	return url + '/' + taskId;
}

async function sendDeleteRequest(urlWithTaskId){
	await fetch(urlWithTaskId, {
        method: 'DELETE'
    })
}

async function editTask(taskId) {
    let urlWithTaskId = getUrlWithTaskId(taskId);
	let taskBody = getTaskBodyForEdit(taskId);
	await sendPatchRequest(urlWithTaskId, taskBody);
    displayToDoList();
}

function getTaskBodyForEdit(taskId){
	let taskText = document.getElementById('text'+taskId).value;
    let taskStatus = document.getElementById('status'+taskId).checked;
    return { text: taskText, isDone: taskStatus };
}

async function sendPatchRequest(urlWithTaskId, taskBody){
	await fetch(urlWithTaskId, {
        method: 'PATCH',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(taskBody)
    })
}