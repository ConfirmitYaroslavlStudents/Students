const url = 'https://localhost:5001/api/ToDoItems/';

export const addToDoItem = async toDoItem => {
	await fetch(url, {
				method: "POST",
				headers: { "Accept": "application/json", "Content-Type": "application/json" },
				body: JSON.stringify({
										description: toDoItem.description,
										status: toDoItem.status
									})
				});
}
						
export const getToDoItems = async () =>{
	const response = await fetch(url, {
					method: "GET",
					headers: { "Accept": "application/json", "Content-Type": "application/json" }
					});
					
	return response;
}

export const deleteToDoItem = async id =>{
		await fetch(url + id, {
					method: "DELETE",
					headers: { "Accept": "application/json", "Content-Type": "application/json" },
					});
}

export const editToDoItem = async toDoItem =>{
		await fetch(url + toDoItem.id, {
					method: "PATCH",
					headers: { "Accept": "application/json", "Content-Type": "application/json" },
					body: JSON.stringify({
						description: toDoItem.description,
						status: toDoItem.status
					})
        });
}