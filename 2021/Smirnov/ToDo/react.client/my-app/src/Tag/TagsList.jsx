import React from 'react';
import {addTag, getTags, deleteTag, editTag} from '../service/RequestsTag';
import {Tag} from './Tag';
import {TagForm} from './TagForm';

export class TagsList extends React.Component {
	constructor(props) {
        super(props);
        this.state = { tags: [],id : 0, name: "", submitValue: "" };
    }
	
	onNameChange = e => {
        this.setState({name: e.target.value});
    }

	onAddTag = async tag => {
		await addTag(tag);
		await this.loadData();
	}
	onDeleteTag = async tag => {
		await deleteTag(tag.id);	
		await this.loadData();
	}
	onGetTag = async tag => {	
		this.setState({id: tag.id, name : tag.name, submitValue:"Edit"});
	}	
	
	onEditTag = async tag => {
		await editTag(tag);
		await this.loadData();
	}
	
	loadData = async () => {
		const data = await getTags();
        this.setState({ tags: data, id: 0, name: "", submitValue:"Add" });
    }
    componentDidMount = () => {
        this.loadData();
    }
	render(){
		return (
			<div>
				<TagForm tag={{id: this.state.id, name : this.state.name}} submitValue={this.state.submitValue}
				onNameChange={this.onNameChange} onEditTag={this.onEditTag} onAddTag={this.onAddTag} />		
				<table id ="toDoList">
					<thead>
						<tr>
							<th>
								name
							</th>
						</tr>
					</thead>
					<tbody>
						{
							this.state.tags.map((tag) => {
							return 	<Tag key ={tag.id} tag={tag} onGetTag={this.onGetTag} onDeleteTag={this.onDeleteTag}/>
							})
						}	
					</tbody>
				</table>
			</div>
		);
	}
}