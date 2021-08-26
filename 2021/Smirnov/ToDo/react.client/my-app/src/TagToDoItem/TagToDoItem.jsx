import React from 'react';

export class TagToDoItem extends React.Component {
	constructor(props) {
        super(props);
        this.state = {tagToDoItem: this.props.tagToDoItem};
	}
	 onClickDelete = e =>{
		this.props.onDeleteTagToDoItem(this.state.tagToDoItem);
	}
	onClickEdit = e =>{
		this.props.onGetTagToDoItem(this.state.tagToDoItem);
	}
	
	render(){
		return (
			<tr>
			<td>
					{this.state.tagToDoItem.id}
				</td>
				<td>
					{this.state.tagToDoItem.toDoItemId}
				</td>
				<td>
					{this.state.tagToDoItem.tagId}
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