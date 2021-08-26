import React from 'react';
import {ToDoItem} from '../ToDoItem/ToDoItem';
import {getSelectedToDoItemsLogicalOperatorOr, getSelectedToDoItemsLogicalOperatorAnd} from '../service/RequestsSelectedToDoItems';

export class SelectedToDoItemsList extends React.Component {
	constructor(props) {
        super(props);
        this.state = { toDoItems: []};
    }
	
	onGetToDoItem = async newToDoItem => {
	}	
	
		onDeleteToDoItem = async toDoItem => {
	}
	onOrClick = async e => {
		const data = await getSelectedToDoItemsLogicalOperatorOr();
		this.setState({ toDoItems: data});
	}
	onAndClick = async e => {
		const data = await getSelectedToDoItemsLogicalOperatorAnd();
		this.setState({ toDoItems: data});
	}
	loadData = async () => {
    }
    componentDidMount = () => {
    }
	render(){
		return (
			<div>
				<label>
					ToDoItemList
				</label>
				<div>
					<button onClick={this.onOrClick}>
						Or
					</button>
					<button onClick={this.onAndClick}>
						And
					</button>
				</div>
				<table>
					<thead>
						<tr>
							<th>
								id
							</th>
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