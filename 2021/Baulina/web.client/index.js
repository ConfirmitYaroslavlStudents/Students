const url = "https://localhost:5001/todo-list";
let todoItems = [];
let toDoItemStatus = { NotComplete: 0, Complete: 1 };

async function getToDoItems() {
    try {
        const response = await fetch(url);
        if (response.ok) {
            todoItems = await response.json();
        } else handleError(response);
    } catch (err) {
        console.error("Unable to get items.", error);
    }
}

async function updateItemsDisplay() {
    await getToDoItems();
    displayItems();
}

async function addToDoItem() {
    const addNameTextBox = document.getElementById("add-name"); 

    const toDoItem = {
        status: toDoItemStatus.NotComplete,
        description: addNameTextBox.value.trim()
    };

    try {
        const response = await fetch(url,
            {
                method: "POST",
                headers: {
                    'Content-Type': "application/json"
                },
                body: JSON.stringify(toDoItem)
            });
        if (response.ok) {
            addNameTextBox.value = ""; 
            updateItemsDisplay();
        } else handleError(response);
    } catch (err) {
        console.error("Unable to get items.", error);
    }
}

async function deleteToDoItem(id) {
    try {
        const response = await fetch(`${url}/${id}`,
            {
                method: "DELETE"
            });
        if (response.ok) {
            updateItemsDisplay();
        } else handleError(response);
    } catch (err) {
        console.error("Unable to get items.", error);
    }
}

function displayEditForm(id) {
    const toDoItem = todoItems.find(item => item.id === id);

    document.getElementById("edit-name").value = toDoItem.description;
    document.getElementById("edit-id").value = toDoItem.id;
    document.getElementById("edit-isComplete").checked = toDoItem.status === toDoItemStatus.Complete;
    document.getElementById("editForm").style.display = "block";
}

async function editToDoItem() {
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

    try {
        const response = await fetch(`${url}/${itemId}`,
            {
                method: "PATCH",
                headers: {
                    'Content-Type': "application/json-patch+json"
                },
                body: JSON.stringify(requestBody)
            });
        if (response.ok) {
            hideEditForm();
            updateItemsDisplay();
        } else handleError(response);
    } catch (err) {
        console.error("Unable to get items.", error);
    }
}

function hideEditForm() {
    document.getElementById("editForm").style.display = "none";
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? "to-do" : "to-dos";
    document.getElementById("counter").innerText = `${itemCount} ${name}`;
}

function displayItems() {
    const tBody = document.getElementById("todo-items");
    tBody.innerHTML = "";

    _displayCount(todoItems.length);

    const button = document.createElement("button");

    todoItems.forEach(item => {
        let tableRow = tBody.insertRow();

        let isCompleteCheckbox = document.createElement("input");
        isCompleteCheckbox.type = "checkbox";
        isCompleteCheckbox.disabled = true;
        isCompleteCheckbox.checked = item.status === toDoItemStatus.Complete;
        tableRow.insertCell(0).appendChild(isCompleteCheckbox);

        let textNode = document.createTextNode(item.description);
        tableRow.insertCell(1).appendChild(textNode);

        let editButton = button.cloneNode(false);
        editButton.innerText = "Edit";
        editButton.onclick = function() { displayEditForm(item.id) };
        tableRow.insertCell(2).appendChild(editButton);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = "Delete";
        deleteButton.onclick = function() { deleteToDoItem(item.id) };
        tableRow.insertCell(3).appendChild(deleteButton);
    });
}