import React from 'react';
import fetcher from './fetcher';

class ToDoItem extends React.Component {

  fetchSender;

  constructor(props) {
    super(props);
    this.fetchSender = new fetcher();
    this.state = {
      item: props.toDoItem,
      editor: props.editor
    };
  }

  editItem() {
    this.state.editor.setState({
      prevState: this.state.editor.state,
      item: this 
    });
  }

  async deleteItem() {
    await this.fetchSender.sendDelete(this.state.item.id);
    let itemUpdated = await this.fetchSender.getToDoItemUpdate(this.state.item.id)
    this.setState({
      prevState: this.state.editor.state,
      item: itemUpdated
    });
    return;
  }

  render() {
    return (
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

export default ToDoItem;