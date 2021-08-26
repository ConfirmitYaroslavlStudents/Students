import React from 'react';

export class SelectedTagForm extends React.Component {
	
    onSubmit = async e => {
        e.preventDefault();
	
		const {id, tagId} = { id : this.props.selectedTag.id, tagId: this.props.selectedTag.tagId};
		
		if(this.props.command === "Add") 
			await this.props.onAddSelectedTag({tagId});
		else
			await this.props.onEditSelectedTag({id, tagId});   
    }
	
	render() {
		return (
			<div>
				<label>tagId:
					<input onChange={this.props.onTagIdChange} value={this.props.selectedTag.tagId}  />
				</label>
				<button onClick={this.onSubmit}>
					{this.props.command}
				</button>
			</div>
		);
	}
}