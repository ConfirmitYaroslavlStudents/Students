import React from 'react';

class Adder extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            nameContainer: {}
        }

        this.addNameChange = this.addNameChange.bind(this);
    }

    addNameChange(e) {
        this.setState({ nameContainer: e.target });
    }

    render() {
        return (
            <div>
                <h3>Add</h3>
                <input type="text" id="AddName" onChange={this.addNameChange} placeholder="New ToDo"></input>
                <input type="button" id="AddItemButton" value="Add" method="POST" onClick={() => this.props.onClickFunc(this.state.nameContainer)}></input>
            </div>);
    }
}

export default Adder;