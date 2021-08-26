const url = 'https://localhost:5001/api/SelectedTags/';

export const addSelectedTag = async selectedTag => {
	await fetch(url, {
				method: "POST",
				headers: { "Accept": "application/json", "Content-Type": "application/json" },
				body: JSON.stringify({
										tagId: selectedTag.tagId
									})
				});
}
						
export const getSelectedTags = async () =>{
	const response = await fetch(url, {
					method: "GET",
					headers: { "Accept": "application/json", "Content-Type": "application/json" }
					});
					
	return await response.json();
}

export const deleteSelectedTag = async id =>{
		await fetch(url + id, {
					method: "DELETE",
					headers: { "Accept": "application/json", "Content-Type": "application/json" },
					});
}

export const editSelectedTag = async selectedTag =>{
		await fetch(url + selectedTag.id, {
					method: "PUT",
					headers: { "Accept": "application/json", "Content-Type": "application/json" },
					body: JSON.stringify({
						id: selectedTag.id,
						tagId: selectedTag.tagId
					})
        });
}