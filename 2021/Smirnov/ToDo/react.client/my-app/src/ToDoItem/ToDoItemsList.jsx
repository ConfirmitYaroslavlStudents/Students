import React from 'react';
import {addToDoItem, getToDoItems, deleteToDoItem, editToDoItem} from '../service/RequestsToDoItem';
import {ToDoItem} from './ToDoItem';
import {ToDoItemForm} from './ToDoItemForm';

export class ToDoItemsList extends React.Component {
	constructor(props) {
        super(props);
        this.state = { toDoItems: [], id : 0, description : "", status : "", submitValue:"" };
    }
	
	onDescriptionChange = e => {
        this.setState({description: e.target.value});
    }
	onStatusChange = e => {
        this.setState({status: e.target.value});
    }

	onAddToDoItem = async toDoItem => {
		await addToDoItem(toDoItem);
		await this.loadData();
	}
	onDeleteToDoItem = async toDoItem => {
		await deleteToDoItem(toDoItem.id);	
		await this.loadData();
	}
	onGetToDoItem = async newToDoItem => {	
		this.setState({id: newToDoItem.id, description : newToDoItem.description, status: newToDoItem.status, submitValue:"Edit"});
	}	
	
	onEditToDoItem = async toDoItem => {
		await editToDoItem(toDoItem);
		await this.loadData();
	}
	
	loadData = async () => {
		const data = await getToDoItems();
        this.setState({ toDoItems: data, id : 0, description : "", status : "NotDone", submitValue:"Add" });
    }
    componentDidMount = () => {
        this.loadData();
    }
	render(){
		return (
			<div>
				<ToDoItemForm toDoItem={{id: this.state.id, description : this.state.description, status: this.state.status}} submitValue={this.state.submitValue}
				onDescriptionChange={this.onDescriptionChange} onStatusChange={this.onStatusChange} onEditToDoItem={this.onEditToDoItem} onAddToDoItem={this.onAddToDoItem} />		
				<table id ="toDoList">
					<thead>
						<tr>
							<th>
								description
							</th>
							<th>
								status
							</th>
						</tr>
					</thead>
					<tbody>
						{
							this.state.toDoItems.map((toDoItem) => {
							return 	<ToDoItem key ={toDoItem.id} toDoItem={toDoItem} onGetToDoItem={this.onGetToDoItem} onDeleteToDoItem={this.onDeleteToDoItem}/>
							})
						}	
					</tbody>
				</table>
			</div>
		);
	}
}