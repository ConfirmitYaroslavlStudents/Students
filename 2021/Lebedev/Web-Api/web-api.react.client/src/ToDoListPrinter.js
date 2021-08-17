import React from 'react';
import fetcher from './fetcher';
import ToDoItemPrinter from './ToDoItemPrinter';

class ToDoListPrinter extends React.Component {

  fetchSender;

  constructor(props) {
    super(props);
    this.fetchSender = new fetcher();
    this.state = { items: [] };
    this.state.items = props.toDoList;
  }

  async addItem() {
    const addNameTextbox = document.getElementById('AddName');
    let list = await this.fetchSender.sendAddItem(addNameTextbox.value.trim());
    this.setState({ items: list });
    addNameTextbox.value = '';
    return;
  }

  render() {
    document.getElementById('AddItemButton').onclick = () => this.addItem();
    return this.state.items.map((item) => (
      <ToDoItemPrinter toDoItem={item}>
      </ToDoItemPrinter>));
  }
}

export default ToDoListPrinter;