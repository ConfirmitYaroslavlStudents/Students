import React from 'react';

export class TagForm extends React.Component {
	constructor(props) {
        super(props);
        this.state = {submitValue: "Add"};
	}
	
    onSubmit = async e => {
        e.preventDefault();
	
		const {id, name} = { id : this.props.tag.id, name: this.props.tag.name};
		
		if(id === 0) 
			await this.props.onAddTag({name});
		else
			await this.props.onEditTag({id, name});   
    }
	
	render() {
		return (
			<div>
				<label>name:
					<input id="TagDescription" onChange={this.props.onNameChange} value={this.props.tag.name}  />
				</label>
				<button onClick={this.onSubmit} name="add" value={this.props.submitValue} type="submit" id="submit">
					{this.props.submitValue}
				</button>
			</div>
		);
	}
}