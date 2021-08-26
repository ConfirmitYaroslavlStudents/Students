import React from 'react';
import {ToDoItemsList} from './ToDoItem/ToDoItemsList';
import {TagsList} from './Tag/TagsList';
import {TagToDoItemsList} from './TagToDoItem/TagToDoItemsList';
import {SelectedTagsList} from './SelectedTag/SelectedTagsList';
import {SelectedToDoItemsList} from './SelectedToDoItems/SelectedToDoItems';

function App() {
	return (
		<div>
			<ToDoItemsList />
			<TagsList />
			<TagToDoItemsList />
			<SelectedTagsList />
			<SelectedToDoItemsList />
		</div>
	);
}

export default App;


