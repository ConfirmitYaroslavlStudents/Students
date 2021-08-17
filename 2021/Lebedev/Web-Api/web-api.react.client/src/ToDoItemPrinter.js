import React from 'react';
import fetcher from './fetcher';

class ToDoItemPrinter extends React.Component {
  
    fetchSender;
  
    constructor(props) {
      super(props);
      this.fetchSender = new fetcher();
      this.state = { item: {} };
      this.state.item = props.toDoItem;
    }
    
    editItem() {
        document.getElementById('EditName').value = this.state.item.name;
        document.getElementById('EditDelete').checked = this.state.item.deleted;
        document.getElementById('EditComplete').checked = this.state.item.completed;
        document.getElementById('EditForm').style.display = 'block';
        document.getElementById('SaveEditButton').onclick = () => this.patchItem();
    }

    async deleteItem() {
        let list = await this.fetchSender.sendDelete(this.state.item.id);
        this.setState({ item: list[this.state.item.id] });
        return;
    }

    async patchItem() {
        const item = {
          id: parseInt(this.state.item.id, 10),
          name: document.getElementById('EditName').value.trim(),
          completed: document.getElementById('EditComplete').checked,
          deleted: document.getElementById('EditDelete').checked
        };
        let list = await this.fetchSender.sendPatchReqest(item);
        this.setState({ item: list[this.state.item.id] });
        hideEdit();
        return;
    }
    
    render() {
        return(
        <tr key={this.state.item.id}>
        <td key={`${this.state.item.id * 6 + 1}`}>
          {this.state.item.name}
        </td>
        <td key={`${this.state.item.id * 6 + 2}`}>
          <input type="checkbox" checked={this.state.item.completed} disabled={true} ></input>
        </td>
        <td key={`${this.state.item.id * 6 + 3}`}>
          <input type="checkbox" checked={this.state.item.deleted} disabled={true} ></input>
        </td>
        <td key={`${this.state.item.id * 6 + 4}`}>
          <input type="button" value="Edit" onClick={() => this.editItem()} ></input>
        </td>
        <td key={`${this.state.item.id * 6 + 5}`}>
          <input type="button" value="Delete" onClick={() => this.deleteItem()} ></input>
        </td>
      </tr>);
    }
}

function hideEdit() {
  document.getElementById('EditForm').style.display = 'none';
}

export default ToDoItemPrinter;