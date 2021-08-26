import React from 'react';

export class ToDoItemForm extends React.Component {
	
    onSubmit = async e => {
        e.preventDefault();
	
		const {id, description, status} = { id : this.props.toDoItem.id, description: this.props.toDoItem.description ,status: this.props.toDoItem.status};
		
		if(id === 0) 
			await this.props.onAddToDoItem({description, status});
		else
			await this.props.onEditToDoItem({id, description, status});   
    }
	
	render() {
		return (
			<div>
				<label>Description:
					<input onChange={this.props.onDescriptionChange} value={this.props.toDoItem.description}  />
				</label>
				<label>
					Status:
					<select onChange={this.props.onStatusChange} value={this.props.toDoItem.status}>
						<option value="NotDone">NotDone</option>
						<option value="Done">Done</option>
					</select>
				</label>
				<button onClick={this.onSubmit}>
					{this.props.command}
				</button>
			</div>
		);
	}
}