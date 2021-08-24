import React from 'react';

export class ToDoItemForm extends React.Component {
	constructor(props) {
        super(props);
        this.state = {submitValue: "Add"};
	}

	
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
					<input id="ToDoItemDescription" onChange={this.props.onDescriptionChange} value={this.props.toDoItem.description}  />
				</label>
				<label>
					Status:
					<select id="ToDoItemStatus" onChange={this.props.onStatusChange} value={this.props.toDoItem.status}>
						<option value="NotDone">NotDone</option>
						<option value="Done">Done</option>
					</select>
				</label>
				<button onClick={this.onSubmit} name="add" value={this.props.submitValue} type="submit" id="submit">
					{this.props.submitValue}
				</button>
			</div>
		);
	}
}