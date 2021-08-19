import React from 'react';
import {addToDoItem, getToDoItems, deleteToDoItem, editToDoItem} from './Requests';

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
        this.state = {id: 0, description: "", status:"NotDone", submitValue: "Add"};
 
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
	fillForm(toDoItem) {
		this.setState({id: toDoItem.id, description: toDoItem.description, status: toDoItem.status, submitValue: "Edit"});
	}
	
    async onSubmit(e) {
        e.preventDefault();
		
		var idToDo = this.state.id;
        var descriptionToDo = this.state.description;
        var statusToDo = this.state.status;
		
		if(idToDo > 0) 
			await this.props.onEditToDoItem({id: idToDo, description: descriptionToDo, status: statusToDo});
		else
			await this.props.onAddToDoItem({ description: descriptionToDo, status: statusToDo});
		
        this.setState({id: 0, description: "", status:"", submitValue: "Add"});
    }
	
	render() {
		return (
			<form onSubmit={this.onSubmit}>
				<label>Description:
					<input id="ToDoItemDescription" name="description" onChange={this.onDescriptionChange} value={this.state.description}  />
				</label>
				<label>
					Status:
					<select id="ToDoItemStatus" name="status" onChange={this.onStatusChange} value={this.state.status}>
						<option value="NotDone">NotDone</option>
						<option value="Done">Done</option>
					</select>
				</label>
				<input name="add" value={this.state.submitValue} type="submit" id="submit"/>
			</form>
		);
	}
}

class ToDoItem extends React.Component {
	constructor(props) {
        super(props);
        this.state = {toDoItem: props.toDoItem};
		
		this.onClickDelete = this.onClickDelete.bind(this);
		this.onClickEdit = this.onClickEdit.bind(this);
	}
	 onClickDelete(e){
		this.props.onDelete(this.state.toDoItem);
	}
	onClickEdit(e){
		this.props.onEdit(this.state.toDoItem);
	}
	
	render(){
		return (
			<tr key={this.state.toDoItem.id}>
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

class ToDoItemsList extends React.Component {
	constructor(props) {
        super(props);
        this.state = { toDoItems: [] };
		this.toDoItemForm = React.createRef();
		
		this.onAddToDoItem = this.onAddToDoItem.bind(this);
		this.onEditToDoItem = this.onEditToDoItem.bind(this);
		this.onDeleteToDoItem = this.onDeleteToDoItem.bind(this);
		this.onGetToDoItem = this.onGetToDoItem.bind(this);
    }
	
	async onAddToDoItem(toDoItem) {
		await addToDoItem(toDoItem);
		await this.loadData();
	}
	async onDeleteToDoItem(toDoItem) {
		await deleteToDoItem(toDoItem.id);	
		await this.loadData();
	}
	onGetToDoItem(toDoItem) {	
		this.toDoItemForm.current.fillForm(toDoItem);
	}	
	
	async onEditToDoItem(toDoItem) {
		await editToDoItem(toDoItem);
		await this.loadData();
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
		var get = this.onGetToDoItem;
		return (
			<div>
				<ToDoItemForm ref={this.toDoItemForm} onEditToDoItem={this.onEditToDoItem} onAddToDoItem={this.onAddToDoItem} />		
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
							return 	<ToDoItem key ={toDoItem.id} toDoItem={toDoItem} onEdit={get} onDelete={delet}/>
							})
						}	
					</tbody>
				</table>
			</div>
		);
	}
}


