import React from 'react';
import {addTagToDoItem, getTagToDoItems, deleteTagToDoItem, editTagToDoItem} from '../service/RequestsTagToDoItem';
import {TagToDoItem} from './TagToDoItem';
import {TagToDoItemForm} from './TagToDoItemForm';

export class TagToDoItemsList extends React.Component {
	constructor(props) {
        super(props);
        this.state = { tagToDoItems: [], id : 0, toDoItemId : "", tagId : "", command:"" };
    }
	
	onToDoItemIdChange = e => {
        this.setState({toDoItemId: e.target.value});
    }
	onTagIdChange = e => {
        this.setState({tagId: e.target.value});
    }

	onAddTagToDoItem = async tagToDoItem => {
		await addTagToDoItem(tagToDoItem);
		await this.loadData();
	}
	onDeleteTagToDoItem = async tagToDoItem => {
		await deleteTagToDoItem(tagToDoItem.id);	
		await this.loadData();
	}
	onGetTagToDoItem = async tagToDoItem => {	
		this.setState({id: tagToDoItem.id, toDoItemId : tagToDoItem.toDoItemId, tagId: tagToDoItem.tagId, command:"Edit"});
	}	
	
	onEditTagToDoItem = async tagToDoItem => {
		await editTagToDoItem(tagToDoItem);
		await this.loadData();
	}
	
	loadData = async () => {
		const data = await getTagToDoItems();
        this.setState({ tagToDoItems: data, id : 0, toDoItemId : 0, tagId : 0, command:"Add" });
    }
    componentDidMount = () => {
        this.loadData();
    }
	render(){
		return (
			<div>
				<label>
					TagToDoItemList
				</label>
				<TagToDoItemForm tagToDoItem={{id: this.state.id, toDoItemId : this.state.toDoItemId, tagId: this.state.tagId}} command={this.state.command}
				onToDoItemIdChange={this.onToDoItemIdChange} onTagIdChange={this.onTagIdChange} onEditTagToDoItem={this.onEditTagToDoItem} onAddTagToDoItem={this.onAddTagToDoItem} />		
				<table id ="TagToDoItemList">
					<thead>
						<tr>
							<th>
								id
							</th>
							<th>
								toDoItemId
							</th>
							<th>
								TagId
							</th>
						</tr>
					</thead>
					<tbody>
						{
							this.state.tagToDoItems.map((tagToDoItem) => {
							return 	<TagToDoItem key ={tagToDoItem.id} tagToDoItem={tagToDoItem} onGetTagToDoItem={this.onGetTagToDoItem} onDeleteTagToDoItem={this.onDeleteTagToDoItem}/>
							})
						}	
					</tbody>
				</table>
			</div>
		);
	}
}