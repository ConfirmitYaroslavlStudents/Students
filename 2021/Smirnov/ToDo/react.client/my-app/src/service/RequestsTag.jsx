const url = 'https://localhost:5001/api/Tags/';

export const addTag = async tag => {
	await fetch(url, {
				method: "POST",
				headers: { "Accept": "application/json", "Content-Type": "application/json" },
				body: JSON.stringify({
										name: tag.name
									})
				});
}
						
export const getTags = async () =>{
	const response = await fetch(url, {
					method: "GET",
					headers: { "Accept": "application/json", "Content-Type": "application/json" }
					});
					
	return await response.json();
}

export const deleteTag = async id =>{
		await fetch(url + id, {
					method: "DELETE",
					headers: { "Accept": "application/json", "Content-Type": "application/json" },
					});
}

export const editTag = async tag =>{
		await fetch(url + tag.id, {
					method: "PUT",
					headers: { "Accept": "application/json", "Content-Type": "application/json" },
					body: JSON.stringify({
						id: tag.id,
						name: tag.name
					})
        });
}