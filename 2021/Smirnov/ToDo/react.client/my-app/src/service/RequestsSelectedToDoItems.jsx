export const getSelectedToDoItemsLogicalOperatorOr = async () => {
	const response = await fetch('https://localhost:5001/api/SelectedToDoItemsLogicalOperatorOr/', {
					method: "GET",
					headers: { "Accept": "application/json", "Content-Type": "application/json" }
					});
					
	return await response.json();
}
export const getSelectedToDoItemsLogicalOperatorAnd = async () => {
	const response = await fetch('https://localhost:5001/api/SelectedToDoItemsLogicalOperatorAnd/', {
					method: "GET",
					headers: { "Accept": "application/json", "Content-Type": "application/json" }
					});
					
	return await response.json();
}