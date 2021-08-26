import React from 'react';
import {Tag} from '../Tag/Tag';
import {getSelectedTagsForToDoItem} from '../service/RequestsSelectedTagsForToDoItem';
import {deleteTag, editTag} from '../service/RequestsTag';

export class ToDoItem extends React.Component {
	constructor(props) {
        super(props);
        this.state = {toDoItem: this.props.toDoItem, tags:[]};
	}
	 onClickDelete = e =>{
		this.props.onDeleteToDoItem(this.state.toDoItem);
	}
	onClickEdit = e =>{
		this.props.onGetToDoItem(this.state.toDoItem);
	}
	
	loadData = async () => {
		const data = await getSelectedTagsForToDoItem(this.state.toDoItem.id);
        this.setState({ tags: data});
    }
		onDeleteTag = async tag => {
		await deleteTag(tag.id);	
		await this.loadData();
	}
		onEditTag = async tag => {
		await editTag(tag);
		await this.loadData();
	}
    componentDidMount = () => {
        this.loadData();
    }	
	render(){
		return (
			<tr>
			<td>
					{this.state.toDoItem.id}
				</td>
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
				<td>
				<table>
					<thead>
						<tr>
							<th>
								id
							</th>
							<th>
								name
							</th>
						</tr>
					</thead>
					<tbody>
					{
						this.state.tags.map((tag) => {
						return 	<Tag key ={tag.id} tag={tag} onGetTag={this.onEditTag} onDeleteTag={this.onDeleteTag}/>
						})
					}	
					</tbody>
				</table>	
				</td> 
			</tr>
		);
	}
}