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
    try {
        const response = await fetch(url,
            {
                method: "POST",
                headers: {
                    'Content-Type': "application/json"
                },
                body: JSON.stringify(_getToDoItemToAdd())
            });
        if (response.ok) {
            _clearAddForm();
            updateItemsDisplay();
        } else handleError(response);
    } catch (err) {
        console.error("Unable to add the item.", error);
    }
}

function _getToDoItemToAdd() {
    var status;
    if (document.getElementById("add-isComplete").checked)
        status = toDoItemStatus.Complete;
    else status = toDoItemStatus.NotComplete;

    return {
        status: status,
        description: document.getElementById("add-description").value.trim()
    };
}

function _clearAddForm() {
    document.getElementById("add-description").value = "";
    document.getElementById("add-isComplete").checked = false;
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
        console.error("Unable to delete the item.", error);
    }
}

function displayEditForm(id) {
    const toDoItem = todoItems.find(item => item.id === id);

    document.getElementById("edit-description").value = toDoItem.description;
    document.getElementById("edit-id").value = toDoItem.id;
    document.getElementById("edit-isComplete").checked = toDoItem.status === toDoItemStatus.Complete;
    document.getElementById("editForm").style.display = "block";
}

async function editToDoItem() {
    try {
        const response = await fetch(`${url}/${_getIdOfItemToBeEdited()}`,
            {
                method: "PATCH",
                headers: {
                    'Content-Type': "application/json-patch+json"
                },
                body: JSON.stringify(_getPatchRequestBody())
            });
        if (response.ok) {
            hideEditForm();
            updateItemsDisplay();
        } else handleError(response);
    } catch (err) {
        console.error("Unable to edit the item.", error);
    }
}

function _getIdOfItemToBeEdited() {                        
    return document.getElementById("edit-id").value;
}

function _getPatchRequestBody() {
    var status;
    if (document.getElementById("edit-isComplete").checked)
        status = toDoItemStatus.Complete;
    else status = toDoItemStatus.NotComplete;
    return [
        { op: "replace", path: "/Description", value: document.getElementById("edit-description").value.trim() },
        { op: "replace", path: "/Status", value: status }
    ];
}

function hideEditForm() {
    document.getElementById("editForm").style.display = "none";
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? "to-do" : "to-dos";
    document.getElementById("counter").innerText = `${itemCount} ${name}`;
}

function _clearTable() {
    const tBody = document.getElementById("todo-items");
    tBody.innerHTML = "";
}

function displayItems() {
    _clearTable();
    _displayCount(todoItems.length);

    const tBody = document.getElementById("todo-items");

    todoItems.forEach(item => {
        _generateTableRow(tBody.insertRow(), item);
    });
}

function _generateTableRow(tableRow, item) {
    tableRow.insertCell(0).appendChild(_createIsCompleteCheckbox(item));
    
    tableRow.insertCell(1).appendChild(document.createTextNode(item.description));

    tableRow.insertCell(2).appendChild(_createEditButton(item));

    tableRow.insertCell(3).appendChild(_createDeleteButton(item));
}

function _createIsCompleteCheckbox(item) {
    const isCompleteCheckbox = document.createElement("input");
    isCompleteCheckbox.type = "checkbox";
    isCompleteCheckbox.disabled = true;
    isCompleteCheckbox.checked = item.status === toDoItemStatus.Complete;
    return isCompleteCheckbox;
}

function _createEditButton(item) {
    const editButton = document.createElement("button");
    editButton.innerText = "Edit";
    editButton.onclick = function () { displayEditForm(item.id) };
    return editButton;
}

function _createDeleteButton(item) {
    const deleteButton = document.createElement("button");
    deleteButton.innerText = "Delete";
    deleteButton.onclick = function () { deleteToDoItem(item.id) };
    return deleteButton;
}