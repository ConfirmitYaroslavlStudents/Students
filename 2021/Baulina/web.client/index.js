const url = "https://localhost:5001/todo-list";
let todoItems = [];
let toDoItemStatus = Enum({ NotComplete: "0", Complete: "1" });

function getToDoItems() {
    fetch(url)
        .then(CheckError)
        .then(data => _displayItems(data))
        .catch(error => console.error("Unable to get items.", error));
}

function addToDoItem() {
    const addNameTextBox = document.getElementById("add-name");

    const toDoItem = {                     
        Status: toDoItemStatus.NotComplete.description,
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
    fetch(`${url}/${id}`,
            {
                method: "DELETE"
            })
        .then(() => getToDoItems())
        .catch(error => console.error("Unable to delete item.", error));
}

function displayEditForm(id) {
    const toDoItem = todoItems.find(item => item.id === id);

    document.getElementById("edit-name").value = toDoItem.description;
    document.getElementById("edit-id").value = toDoItem.id;
    if (toDoItem.status == toDoItemStatus.Complete.description)
        document.getElementById("edit-isComplete").checked = true;
    else document.getElementById("edit-isComplete").checked = false;
    document.getElementById("editForm").style.display = "block";
}

function editToDoItem() {
    const itemId = document.getElementById("edit-id").value;
    var status;
    if (document.getElementById("edit-isComplete").checked)
        status = parseInt(toDoItemStatus.Complete.description);         
    else status = parseInt(toDoItemStatus.NotComplete.description);     
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

    hideEditForm();

    return false;
}

function hideEditForm() {
    document.getElementById("editForm").style.display = "none";
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? "to-do" : "to-dos";

    document.getElementById("counter").innerText = `${itemCount} ${name}`;
}

function _displayItems(toDoList) {
    const tBody = document.getElementById("todo-items");
    tBody.innerHTML = "";

    _displayCount(toDoList.length);

    const button = document.createElement("button");

    toDoList.forEach(item => {
        let tableRow = tBody.insertRow();

        let isCompleteCheckbox = document.createElement("input");
        isCompleteCheckbox.type = "checkbox";
        isCompleteCheckbox.disabled = true;
        if (item.status == toDoItemStatus.Complete.description)
            isCompleteCheckbox.checked = true;
        else isCompleteCheckbox.checked = false;
        tableRow.insertCell(0).appendChild(isCompleteCheckbox);

        let textNode = document.createTextNode(item.description);
        tableRow.insertCell(1).appendChild(textNode);

        let editButton = button.cloneNode(false);
        editButton.innerText = "Edit";
        editButton.setAttribute("onclick", `displayEditForm(${item.id})`);
        tableRow.insertCell(2).appendChild(editButton);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = "Delete";
        deleteButton.setAttribute("onclick", `deleteToDoItem(${item.id})`);
        tableRow.insertCell(3).appendChild(deleteButton);
    });

    todoItems = toDoList;
}