import { Enum } from "./enum.js"

const url = "http://localhost:5000/todo-list";
let todoItems = [];
let toDoItemStatus = Enum({ NotComplete: "0", Complete: "1" });

function getToDoItems() {
    fetch(url)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error("Unable to get items.", error));
}

function addToDoItem() {
    const addNameTextBox = document.getElementById("add-name");

    const toDoItem = {                     
        Status: toDoItemStatus.NotComplete,
        Description: addNameTextBox.value.trim()
    };

    fetch(url,
            {
                method: "POST",
                headers: {
                    'Accept': "application/json",
                    'Content-Type': "application/json"
                },
                body: JSON.stringify(toDoItem)
            })
        .then(response => response.json())
        .then(() => {
            getToDoItems();
            addNameTextBox.value = "";
        })
        .catch(error => console.error("Unable to add item.", error));
}

function deleteToDoItem(id) {
    fetch(`${url}/${id}`, {
        method: "DELETE"
    })
        .then(() => getToDoItems())
        .catch(error => console.error("Unable to delete item.", error));
}

function displayEditForm(id) {
    const toDoItem = todoItems.find(item => item.id === id);

    document.getElementById("edit-name").value = toDoItem.name;
    document.getElementById("edit-id").value = toDoItem.id;
    if (toDoItem.Status === toDoItemStatus.Complete)
        document.getElementById("edit-isComplete").checked = true;
    else document.getElementById("edit-isComplete").checked = false;
    document.getElementById("editForm").style.display = "block";
}

function editToDoItem() {
    const itemId = document.getElementById("edit-id").value;
    var status;
    if (document.getElementById("edit-isComplete").checked)
        status = toDoItemStatus.Complete;
    else status = toDoItemStatus.NotComplete;
    const requestBody =
    [
        { op: "replace", path: "/Description", value: document.getElementById("edit-name").value.trim() },
        { op: "replace", path: "/Status", value: status }
    ];

    fetch(`${url}/${itemId}`,
            {
                method: "PATCH",
                headers: {
                    'Accept': "application/json",
                    'Content-Type': "application/json-patch+json"
                },
                body: JSON.stringify(requestBody)
            })
        .then(() => getToDoItems())
        .catch(error => console.error("Unable to update item.", error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById("editForm").style.display = "none";
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? "to-do" : "to-dos";

    document.getElementById("counter").innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
    const tBody = document.getElementById("todo-items");
    tBody.innerHTML = "";

    _displayCount(data.length);

    const button = document.createElement("button");

    data.forEach(item => {
        let isCompleteCheckbox = document.createElement("input");
        isCompleteCheckbox.type = "checkbox";
        isCompleteCheckbox.disabled = true;
        if (item.Status === toDoItemStatus.Complete)
            isCompleteCheckbox.checked = true;
        else isCompleteCheckbox.checked = false;

        let editButton = button.cloneNode(false);                   
        editButton.innerText = "Edit";
        editButton.setAttribute("onclick", `displayEditForm(${item.Id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = "Delete";
        deleteButton.setAttribute("onclick", `deleteToDoItem(${item.Id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(isCompleteCheckbox);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.Description);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    todoItems = data;
}