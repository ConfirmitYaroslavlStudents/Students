const url = 'https://localhost:5001/api/ToDoItems/';

export async function addToDoItem(toDoItem){
	await fetch(url, {
				method: "POST",
				headers: { "Accept": "application/json", "Content-Type": "application/json" },
				body: JSON.stringify({
										description: toDoItem.description,
										status: toDoItem.status
									})
				});
}
						
export async function getToDoItems(){
	const response = await fetch(url, {
					method: "GET",
					headers: { "Accept": "application/json", "Content-Type": "application/json" }
					});
					
	return response;
}

export async function deleteToDoItem(id){
		await fetch(url + id, {
					method: "DELETE",
					headers: { "Accept": "application/json", "Content-Type": "application/json" },
					});
}

export async function editToDoItem(toDoItem){
		await fetch(url + toDoItem.id, {
					method: "PATCH",
					headers: { "Accept": "application/json", "Content-Type": "application/json" },
					body: JSON.stringify({
						description: toDoItem.description,
						status: toDoItem.status
					})
        });
}