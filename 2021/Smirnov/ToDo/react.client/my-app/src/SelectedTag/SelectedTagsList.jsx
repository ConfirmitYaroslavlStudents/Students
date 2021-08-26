import React from 'react';
import {addSelectedTag, getSelectedTags, deleteSelectedTag, editSelectedTag} from '../service/RequestsSelectedTag';
import {SelectedTag} from './SelectedTag';
import {SelectedTagForm} from './SelectedTagForm';

export class SelectedTagsList extends React.Component {
	constructor(props) {
        super(props);
        this.state = { selectedTags: [],id : 0, tagId: 0, command: "" };
    }
	
	onTagIdChange = e => {
        this.setState({tagId: e.target.value});
    }

	onAddSelectedTag = async selectedTag => {
		await addSelectedTag(selectedTag);
		await this.loadData();
	}
	onDeleteSelectedTag = async selectedTag => {
		await deleteSelectedTag(selectedTag.id);	
		await this.loadData();
	}
	onGetSelectedTag = async selectedTag => {	
		this.setState({id: selectedTag.id, tagId : selectedTag.tagId, command:"Edit"});
	}	
	
	onEditSelectedTag = async selectedTag => {
		await editSelectedTag(selectedTag);
		await this.loadData();
	}
	
	loadData = async () => {
		const data = await getSelectedTags();
        this.setState({ selectedTags: data, id: 0, tagId: 0, command:"Add" });
    }
    componentDidMount = () => {
        this.loadData();
    }
	render(){
		return (
			<div>
				<label>
					SelectedTagsList
				</label>
				<SelectedTagForm selectedTag={{id: this.state.id, tagId : this.state.tagId}} command={this.state.command}
				onTagIdChange={this.onTagIdChange} onEditSelectedTag={this.onEditSelectedTag} onAddSelectedTag={this.onAddSelectedTag} />		
				<table>
					<thead>
						<tr>
							<th>
								id
							</th>
							<th>
								tagId
							</th>
						</tr>
					</thead>
					<tbody>
						{
							this.state.selectedTags.map((selectedTag) => {
							return 	<SelectedTag key ={selectedTag.id} selectedTag={selectedTag} onGetSelectedTag={this.onGetSelectedTag} onDeleteSelectedTag={this.onDeleteSelectedTag}/>
							})
						}	
					</tbody>
				</table>
			</div>
		);
	}
}