const url = 'https://localhost:5001/api/SelectedTagsForToDoItem/';

export const getSelectedTagsForToDoItem = async (id) => {
	const response = await fetch(url + id, {
					method: "GET",
					headers: { "Accept": "application/json", "Content-Type": "application/json" }
					});
					
	return await response.json();
}