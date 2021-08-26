const url = 'https://localhost:5001/api/ToDoItems/';

export const addToDoItem = async toDoItem => {
	try{
	await fetch(url, {
				method: "POST",
				headers: { "Accept": "application/json", "Content-Type": "application/json" },
				body: JSON.stringify({
										description: toDoItem.description,
										status: toDoItem.status
									})
				});
	}
	catch(err){
		alert(err);
	}
}
						
export const getToDoItems = async () =>{
	try{
		const response = await fetch(url, {
					method: "GET",
					headers: { "Accept": "application/json", "Content-Type": "application/json" }
					});
					
		return await response.json();
	}
	catch(err){
		alert(err);
		return [];
	}
}

export const deleteToDoItem = async id =>{
	try{
		await fetch(url + id, {
					method: "DELETE",
					headers: { "Accept": "application/json", "Content-Type": "application/json" },
					});
	}
	catch(err){
		alert(err);
	}
}

export const editToDoItem = async toDoItem =>{
	try{
		await fetch(url + toDoItem.id, {
					method: "PATCH",
					headers: { "Accept": "application/json", "Content-Type": "application/json" },
					body: JSON.stringify({
						description: toDoItem.description,
						status: toDoItem.status
					})
        });
	}
	catch(err){
		alert(err);
	}
}