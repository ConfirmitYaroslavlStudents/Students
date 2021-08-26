const url = 'https://localhost:5001/api/TagToDoItems/';

export const addTagToDoItem = async tagToDoItem => {
	await fetch(url, {
				method: "POST",
				headers: { "Accept": "application/json", "Content-Type": "application/json" },
				body: JSON.stringify({
										toDoItemId: tagToDoItem.toDoItemId,
										tagId: tagToDoItem.tagId
									})
				});
}
						
export const getTagToDoItems = async () =>{
	const response = await fetch(url, {
					method: "GET",
					headers: { "Accept": "application/json", "Content-Type": "application/json" }
					});
					
	return await response.json();
}

export const deleteTagToDoItem = async id =>{
		await fetch(url + id, {
					method: "DELETE",
					headers: { "Accept": "application/json", "Content-Type": "application/json" },
					});
}

export const editTagToDoItem = async tagToDoItem =>{
		await fetch(url + tagToDoItem.id, {
					method: "PUT",
					headers: { "Accept": "application/json", "Content-Type": "application/json" },
					body: JSON.stringify({
						id : tagToDoItem.id,
						toDoItemId: tagToDoItem.toDoItemId,
						tagId: tagToDoItem.tagId
					})
        });
}