import React from 'react';

export class SelectedTag extends React.Component {
	constructor(props) {
        super(props);
        this.state = {selectedTag: this.props.selectedTag};
	}
	 onClickDelete = e =>{
		this.props.onDeleteSelectedTag(this.state.selectedTag);
	}
	onClickEdit = e =>{
		this.props.onGetSelectedTag(this.state.selectedTag);
	}
	
	render(){
		return (
			<tr>
				<td>
					{this.state.selectedTag.id}
				</td>
				<td>
					{this.state.selectedTag.tagId}
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