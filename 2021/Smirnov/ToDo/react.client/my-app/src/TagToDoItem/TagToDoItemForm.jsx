import React from 'react';

export class TagToDoItemForm extends React.Component {
	
    onSubmit = async e => {
        e.preventDefault();
	
		const {id, toDoItemId, tagId} = { id : this.props.tagToDoItem.id, toDoItemId: this.props.tagToDoItem.toDoItemId , tagId: this.props.tagToDoItem.tagId};
		
		if(this.props.command === "Add") 
			await this.props.onAddTagToDoItem({toDoItemId, tagId});
		else
			await this.props.onEditTagToDoItem({id, toDoItemId, tagId});   
    }
	
	render() {
		return (
			<div>
				<label>ToDoItemId:
					<input onChange={this.props.onToDoItemIdChange} value={this.props.tagToDoItem.toDoItemId}  />
				</label>
				<label>
					TagId:
					<input onChange={this.props.onTagIdChange} value={this.props.tagToDoItem.tagId} />
				</label>
				<button onClick={this.onSubmit}>
					{this.props.command}
				</button>
			</div>
		);
	}
}