import React from 'react';

class Adder extends React.Component {
    constructor(props) {
        super(props);
        props.connector.setAdder(this);
    }

    render() {
        return(
            <div>
                <h3>Add</h3>
                <input type="text" id="AddName" placeholder="New ToDo"></input>
                <input type="submit" id="AddItemButton" value="Add" method="POST" onClick={()=>this.state.onClick}></input>
            </div>);
    }
}

export default Adder;