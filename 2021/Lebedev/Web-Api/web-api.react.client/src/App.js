import React from 'react';
import fetcher from './fetcher';

class ToDoListPrinter extends React.Component {
  fetchSender;

  constructor(props) {
    super(props);
    this.fetchSender = new fetcher();
    this.state = { items: [] };
    this.state.items = props.toDoList;
  }

  async deleteItem(id) {
    let list = await this.fetchSender.sendDelete(id);
    this.setState({ items: list });
    return;
  }

  editItem(item) {
    document.getElementById('EditName').value = item.name;
    document.getElementById('EditId').value = item.id;
    document.getElementById('EditDelete').checked = item.deleted;
    document.getElementById('EditComplete').checked = item.completed;
    document.getElementById('EditForm').style.display = 'block';
  }

  async addItem() {
    const addNameTextbox = document.getElementById('AddName');
    let list = await this.fetchSender.sendAddItem(addNameTextbox.value.trim());
    this.setState({ items: list });
    addNameTextbox.value = '';
    return;
  }

  async patchItem() {
    const itemId = document.getElementById('EditId').value;
    const item = {
      id: parseInt(itemId, 10),
      name: document.getElementById('EditName').value.trim(),
      completed: document.getElementById('EditComplete').checked,
      deleted: document.getElementById('EditDelete').checked
    };
    let list = await this.fetchSender.sendPatchReqest(item);
    this.setState({ items: list });
    hideEdit();
    return;
  }

  render() {
    document.getElementById('SaveEditButton').onclick = () => this.patchItem();
    document.getElementById('AddItemButton').onclick = () => this.addItem();
    return this.state.items.map((item) => (
      <tr key={item.id}>
        <td key={`${item.id * 6 + 1}`}>
          {item.name}
        </td>
        <td key={`${item.id * 6 + 2}`}>
          <input type="checkbox" checked={item.completed} disabled={true} ></input>
        </td>
        <td key={`${item.id * 6 + 3}`}>
          <input type="checkbox" checked={item.deleted} disabled={true} ></input>
        </td>
        <td key={`${item.id * 6 + 4}`}>
          <input type="button" value="Edit" onClick={() => this.editItem(item)} ></input>
        </td>
        <td key={`${item.id * 6 + 5}`}>
          <input type="button" value="Delete" onClick={() => this.deleteItem(item.id)} ></input>
        </td>
      </tr>));
  }
}

function hideEdit() {
  document.getElementById('EditForm').style.display = 'none';
}

export default ToDoListPrinter;