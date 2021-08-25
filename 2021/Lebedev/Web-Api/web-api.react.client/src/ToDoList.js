import React from 'react';
import fetcher from './fetcher';
import ToDoItem from './ToDoItem';

class ToDoList extends React.Component {

  fetchSender;

  constructor(props) {
    super(props);
    this.fetchSender = new fetcher();
    this.state = {
      items: props.toDoList,
      adder: props.connector.getAdder(),
      editor: props.connector.getEditor()
    };
    this.state.adder.setState({ onClickFunc: () => this.addItem() });
  }

  async addItem() {
    const addNameTextbox = document.getElementById('AddName');
    await this.fetchSender.sendAddItem(addNameTextbox.value.trim());
    let list = await this.fetchSender.getToDoListUpdate();
    this.setState({ items: list });
    addNameTextbox.value = '';
    return;
  }

  render() {
    return this.state.items.map((item) => (
      <ToDoItem toDoItem={item} editor={this.state.editor} />));
  }
}

export default ToDoList;