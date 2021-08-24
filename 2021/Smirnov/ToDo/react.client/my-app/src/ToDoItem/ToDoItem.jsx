import React from 'react';

export class ToDoItem extends React.Component {
	constructor(props) {
        super(props);
        this.state = {toDoItem: props.toDoItem};
	}
	 onClickDelete = e =>{
		this.props.onDeleteToDoItem(this.state.toDoItem);
	}
	onClickEdit = e =>{
		this.props.onGetToDoItem(this.state.toDoItem);
	}
	
	render(){
		return (
			<tr>
				<td>
					{this.state.toDoItem.description}
				</td>
				<td>
					{this.state.toDoItem.status}
				</td>
				<td>
					<button onClick={this.onClickEdit}>
						change
					</button>
				</td> 
				<td>
					<button onClick={this.onClickDelete}>
						delete
					</button>
				</td> 
			</tr>
		);
	}
}