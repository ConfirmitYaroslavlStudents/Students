import React from 'react';
import { sendDelete, getToDoItemUpdate } from './FetchSender';
import ToDoItemTag from './ToDoItemTag';

class ToDoItem extends React.Component {
  editor;

  constructor(props) {
    super(props);
    this.state = {
      item: props.toDoItem
    };
  }

  editItem() {
    this.editor.newTag = "";
    this.editor.setState({
      prevState: this.editor.state,
      item: this
    });
  }

  async deleteItem() {
    await sendDelete(this.state.item.id);
    let itemUpdated = await getToDoItemUpdate(this.state.item.id)
    this.setState({
      item: itemUpdated
    });
    return;
  }

  render() {
    this.editor = this.props.editorNew;
    const item = this.state.item;

    return (
      <tr key={item.id}>
        <td key={item.name + item.id}>
          {item.name}
        </td>
        <td key={`${item.tag}`}>
          <ToDoItemTag listOfTags={item.tag} editor={this.editor} />
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