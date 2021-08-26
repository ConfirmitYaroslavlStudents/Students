import React from 'react';
import Editor from './Editor';
import Filter from './Filter';
import { sendAddItem, getToDoListUpdate } from './Fetcher';
import Adder from './Adder';
import ToDoItem from './ToDoItem';

class ToDoList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      items: props.toDoList,
      editor: {},
      filter: []
    };

    this.addItem = this.addItem.bind(this);
    this.setEditor = this.setEditor.bind(this);
  }

  async addItem(container) {
    await sendAddItem(container.value.trim());
    let list = await getToDoListUpdate();
    this.setState({ items: list });
    container.value = '';
  }

  setEditor(newEditor) {
    this.setState({
      prevState: this.state,
      editor: newEditor
    });
  }

  render() {
    return (
      <div>
        <h3>ToDoList</h3>
        <Adder onClickFunc={this.addItem} />
        <Editor connector={this.setEditor} />
        <Filter connector={this} />
        <table border="solid">
          <thead>
            <tr>
              <th>Name</th>
              <th>Tags</th>
              <th>Completed</th>
              <th>Deleted</th>
              <th></th>
              <th></th>
            </tr>
          </thead>
          <tbody>{
            this.state.items.map((item) => {
              let flag = true;
              this.state.filter.forEach((tag) => {
                if (!(item.tag.indexOf(tag) >= 0))
                  flag = false;
              });
              if (flag)
                return (
                  <ToDoItem toDoItem={item} editorNew={this.state.editor} key={`${item.id}_${item.name}`} />)
            })
          }
          </tbody>
        </table>
      </div>
    );
  }
}

export default ToDoList;