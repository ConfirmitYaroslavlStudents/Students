import React from 'react';

export class Tag extends React.Component {
	constructor(props) {
        super(props);
        this.state = {tag: props.tag};
	}
	 onClickDelete = e =>{
		this.props.onDeleteTag(this.state.tag);
	}
	onClickEdit = e =>{
		this.props.onGetTag(this.state.tag);
	}
	
	render(){
		return (
			<tr>
				<td>
					{this.state.tag.name}
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