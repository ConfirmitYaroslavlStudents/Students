const url = 'http://localhost:5000/todolist';

export async function getToDoListUpdate() {
    const response = await fetch(`${url}`);
    const list = await response.json();
    return list;
}

export async function getToDoItemUpdate(id) {
    const response = await fetch(`${url}/${id}`);
    const item = await response.json();
    return item;
}

export async function sendDelete(id) {
    await fetch(`${url}/${id}`, {
        headers: {
            'Content-Type': 'application/json',
            'mode': 'no-cors'
        },
        method: 'DELETE'
    });
}

export async function sendAddItem(itemName) {
    const item = {
        id: 0,
        completed: false,
        deleted: false,
        name: itemName
    };

    await fetch(`${url}`, {
        method: 'POST',
        headers: {
            'mode': 'no-cors',
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    });
}

export async function sendPatchReqest(item) {
    await fetch(`${url}`, {
        method: 'PATCH',
        headers: {
            'mode': 'no-cors',
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    });
}