import React from 'react';
import fetcher from './fetcher';
import ToDoItemTag from './ToDoItemTag';

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
    const item = this.state.item;
    return (
      <tr key={item.id}>
        <td key={item.name+item.id}>
          {item.name}
        </td>
        <td key={`${item.tag}`}>
          <ToDoItemTag listOfTags={item.tag} editor={this.state.editor}/>
        </td>
        <td key={`Completed${item.id}`}>
          <input type="checkbox" checked={this.state.item.completed} disabled={true} ></input>
        </td>
        <td key={`Deleted${item.id}`}>
          <input type="checkbox" checked={this.state.item.deleted} disabled={true} ></input>
        </td>
        <td key={`Edit${item.id}`}>
          <input type="button" value="Edit" onClick={() => this.editItem()} ></input>
        </td>
        <td key={`Delete${item.id}`}>
          <input type="button" value="Delete" onClick={() => this.deleteItem()} ></input>
        </td>
      </tr>);
  }
}

export default ToDoItem;