import  handleError from './errorHandler';
import configData from "../../config.json";

const url = configData.SERVER_URL;

export async function getAllItems(){
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

export async function addTodoItem(newItem){
    const response = await fetch(url,
        {
                method: "POST",
                headers: {
                    'Content-Type': "application/json"
                },
                body: JSON.stringify(newItem)
        });
    return handleResponse(response);
}

export async function editTodoItem(id, adjustments){
    const response = await fetch(`${url}/${id}`,
        {
                method: "PATCH",
                headers: {
                    'Content-Type': "application/json-patch+json"
                },
                body: JSON.stringify(adjustments)
        });
    return handleResponse(response);
}

export async function deleteTodoItem(id){
    const response = await fetch(`${url}/${id}`,
            {
                method: "DELETE"
            });
    return handleResponse(response);
}

function handleResponse(response){
    try {
        if (response.ok) {
            return true;
        } else handleError(response);
    } catch (error) {
        console.error(error);
    }
} 