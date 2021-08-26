import React from 'react';

export class TagForm extends React.Component {
    onSubmit = async e => {
        e.preventDefault();
	
		const {id, name} = { id : this.props.tag.id, name: this.props.tag.name};
		
		if(this.props.command === "Add") 
			await this.props.onAddTag({name});
		else
			await this.props.onEditTag({id, name});   
    }
	
	render() {
		return (
			<div>
				<label>name:
					<input onChange={this.props.onNameChange} value={this.props.tag.name}  />
				</label>
				<button onClick={this.onSubmit}>
					{this.props.command}
				</button>
			</div>
		);
	}
}