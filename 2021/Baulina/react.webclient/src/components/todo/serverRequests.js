import  handleError from './errorHandler';

const url = "https://localhost:5001/todo-list";

export async function sendGetRequest(){
    try {
        const response = await fetch(url);
        if (response.ok) {
            const data = await response.json();
            return { todoItems: data };
        } else handleError(response);
    } catch (error) {
        console.error(error);
    }
}

export async function sendPostRequest(requestBody){
    try {
        const response = await fetch(url,
            {
                method: "POST",
                headers: {
                    'Content-Type': "application/json"
                },
                body: JSON.stringify(requestBody)
            });
        if (response.ok) {
            return true;
        } else handleError(response);
    } catch (error) {
        console.error(error);
    }
}

export async function sendPatchRequest(id, requestBody){
    try {
        const response = await fetch(`${url}/${id}`,
            {
                method: "PATCH",
                headers: {
                    'Content-Type': "application/json-patch+json"
                },
                body: JSON.stringify(requestBody)
            });
        if (response.ok) {
            return true;
        } else handleError(response);
    } catch (error) {
        console.error(error);
    }
}

export async function sendDeleteRequest(id){
    try {
        const response = await fetch(`${url}/${id}`,
            {
                method: "DELETE"
            });
        if (response.ok) {
            return true;
        } else handleError(response);
    } catch (error) {
        console.error(error);
    }
}