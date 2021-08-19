import React from 'react';
import {addToDoItem, getToDoItems, deleteToDoItem} from './Requests';

function App() {
	return (
	<div>
		<ToDoItemsList />
	</div>
	);
}

export default App;

class ToDoItemForm extends React.Component {
	constructor(props) {
        super(props);
        this.state = {description: "", status:""};
 
        this.onSubmit = this.onSubmit.bind(this);
        this.onDescriptionChange = this.onDescriptionChange.bind(this);
        this.onStatusChange = this.onStatusChange.bind(this);
	}
	onDescriptionChange(e) {
        this.setState({description: e.target.value});
    }
    onStatusChange(e) {
        this.setState({status: e.target.value});
    }
    onSubmit(e) {
        e.preventDefault();
        var descriptionToDo = this.state.description;
        var statusToDo = this.state.status;
		
        this.props.onToDoItemsSubmit({ description: descriptionToDo, status: statusToDo});
        this.setState({description: "", status:""});
    }
	render() {
		return (
			<form onSubmit={this.onSubmit}>
				<label>Description:
					<input id="ToDoItemDescription" name="description" onChange={this.onDescriptionChange}  />
				</label>
				<label>
					Status:
					<select id="ToDoItemStatus" name="status" onChange={this.onStatusChange}>
						<option value="NotDone">Not Done</option>
						<option value="Done">Done</option>
					</select>
				</label>
				<input name="add" value="Add" type="submit" id="submit"/>
			</form>
		);
	}
}

class ToDoItem extends React.Component {
	constructor(props){
        super(props);
        this.state = {toDoItem: props.toDoItem};
		this.onClickDelete = this.onClickDelete.bind(this);
	}
	 onClickDelete(e){
		this.props.onDelete(this.state.toDoItem);
	}
	render(){
		return (
				<tr key={this.state.toDoItem.Id}>
					<td>{this.state.toDoItem.description}</td>
					<td>{this.state.toDoItem.status}</td>
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

class ToDoItemsList extends React.Component {
	constructor(props){
        super(props);
        this.state = { toDoItems: [] };
		
		this.onAddToDoItem = this.onAddToDoItem.bind(this);
		this.onDeleteToDoItem = this.onDeleteToDoItem.bind(this);
    }
	
	async onAddToDoItem(toDoItem){
		await addToDoItem(toDoItem);
		this.loadData();
	}
	async onDeleteToDoItem(toDoItem){
		await deleteToDoItem(toDoItem.id);	
		this.loadData();
	}	
	
	async loadData() {
		
		const response = await getToDoItems();
		const data = await response.json();
        this.setState({ toDoItems: data });
    }
    componentDidMount() {
        this.loadData();
    }
	render(){
		var delet = this.onDeleteToDoItem;
		return (
		<div>
		<ToDoItemForm onToDoItemsSubmit={this.onAddToDoItem} />		
		<table id ="toDoList">
			<thead><tr><th>description</th><th>status</th><th></th></tr></thead>
			<tbody>
				{
					this.state.toDoItems.map((toDoItem) => {
					return 	<ToDoItem key ={toDoItem.id} toDoItem={toDoItem} onDelete={delet}/>
					})
				}	
			</tbody>
		</table>
		</div>
		);
	}
}
