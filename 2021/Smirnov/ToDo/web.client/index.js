const url = 'https://localhost:5001/api/ToDoItems/'

async function GetToDoItems() {
    const response = await fetch(url, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const toDoItems = await response.json();
        let rows = document.querySelector("tbody");
        toDoItems.forEach(toDoItem => {
            rows.append(Row(toDoItem));
        });
    }
}

async function GetToDoItem(id) {
    const response = await fetch(url + id, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const toDoItem = await response.json();
        const form = document.forms["toDoItemsForm"];
        form.elements["id"].value = toDoItem.Id;
        form.elements["description"].value = toDoItem.Description;
        form.elements["status"].value = toDoItem.Status;
    }
}

async function CreateToDoItem(description, status) {

    const response = await fetch(url, {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            description: description,
            status: status
        })
    });
    if (response.ok === true) {
        const toDoItem = await response.json();
        document.querySelector("tbody").append(Row(toDoItem));
    }
    Reset();
}

async function EditToDoItem(id, description, status) {
    const response = await fetch(url + id,
        {
            method: "PATCH",
            headers: { "Accept": "application/json", "Content-Type": "application/json" },
            body: JSON.stringify({
                description: description,
                status: status
            })
        });
    if (response.ok === true) {
        const toDoItem = await response.json();
        document.getElementById(id).replaceWith(Row(toDoItem));
    }
    Reset();
}

async function DeleteToDoItem(id) {
    const response = await fetch(url + id, {
        method: "DELETE",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        document.getElementById(id).remove();
    }
}

function Reset() {
    const form = document.forms["toDoItemsForm"];
    form.reset();
    form.elements["id"].value = 0;
    document.forms["toDoItemsForm"].elements["add"].value = "Add";
}

function Row(toDoItem) {

    const tr = document.createElement("tr");
    tr.setAttribute("id", toDoItem.Id);

    const idTd = document.createElement("td");
    idTd.append(toDoItem.Id);
    tr.append(idTd);

    const descriptionTd = document.createElement("td");
    descriptionTd.append(toDoItem.Description);
    tr.append(descriptionTd);

    const statusTd = document.createElement("td");
    statusTd.append(toDoItem.Status);
    tr.append(statusTd);

    const linksTd = document.createElement("td");

    const editLink = document.createElement("a");
    editLink.setAttribute("data-id", toDoItem.Id);
    editLink.setAttribute("style", "cursor:pointer;padding:15px;");

    const editImg = document.createElement('img');
    editImg.src = "https://localhost:5001/edit.svg";
    editImg.alt = "Edit";
    editLink.appendChild(editImg);

    editLink.addEventListener("click", e => {
        e.preventDefault();
        document.forms["toDoItemsForm"].elements["add"].value = "Change";
        GetToDoItem(toDoItem.Id);
    });
    linksTd.append(editLink);

    const removeLink = document.createElement("a");
    removeLink.setAttribute("data-id", toDoItem.id);
    removeLink.setAttribute("style", "cursor:pointer;padding:15px;");

    const deleteimg = document.createElement('img');
    deleteimg.src = "https://localhost:5001/Delete.svg";
    deleteimg.alt = "Delete";
    removeLink.appendChild(deleteimg);

    removeLink.addEventListener("click", e => {
        //e.preventDefault();
        DeleteToDoItem(toDoItem.Id);
    });
    linksTd.append(removeLink);

    tr.appendChild(linksTd);
    return tr;
}

document.forms["toDoItemsForm"].addEventListener("submit", e => {
    e.preventDefault();
    const form = document.forms["toDoItemsForm"];
    const id = form.elements["id"].value;
    const description = form.elements["description"].value;
    const status = form.elements["status"].value;
    if (id == 0)
        CreateToDoItem(description, status);
    else
        EditToDoItem(id, description, status);
});