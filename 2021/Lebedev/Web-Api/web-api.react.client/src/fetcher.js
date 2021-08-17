const url = 'http://localhost:5000/todolist';

async function getToDoListUpdate() {
    const response = await fetch(`${url}`);
    const list = await response.json();
    return list;
}

class fetcher {
    async sendDelete(id) {
        await fetch(`${url}/${id}`, {
            headers: {
                'Content-Type': 'application/json',
                'mode': 'no-cors'
            },
            method: 'DELETE'
        })
        return await getToDoListUpdate();
    }

    async sendAddItem(itemName) {
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
        })
        return await getToDoListUpdate();
    }

    async sendPatchReqest(item) {
        await fetch(`${url}`, {
            method: 'PATCH',
            headers: {
                'mode': 'no-cors',
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(item)
        });
        return await getToDoListUpdate();
    }
}
export default fetcher;