const url = 'http://localhost:5000/todolist';

class fetcher {

    async getToDoListUpdate() {
        const response = await fetch(`${url}`);
        const list = await response.json();
        return list;
    }
    
    async getToDoItemUpdate(id) {
        const response = await fetch(`${url}/${id}`);
        const item = await response.json();
        return item;
    }

    async sendDelete(id) {
        await fetch(`${url}/${id}`, {
            headers: {
                'Content-Type': 'application/json',
                'mode': 'no-cors'
            },
            method: 'DELETE'
        });
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
        });
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
    }
}

export default fetcher;