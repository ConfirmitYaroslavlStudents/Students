import React from 'react';
import {ToDoItemsList} from './ToDoItem/ToDoItemsList';
import {TagsList} from './Tag/TagsList';

function App() {
	return (
		<div>
			<ToDoItemsList />
			<TagsList />
		</div>
	);
}

export default App;


