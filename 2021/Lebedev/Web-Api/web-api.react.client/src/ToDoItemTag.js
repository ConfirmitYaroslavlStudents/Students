import React from 'react';

class ToDoItemTag extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      ToDoItemTag: props.listOfTags,
      editor: props.editor
    };
    if (this.state.ToDoItemTag === undefined)
      this.state.ToDoItemTag = [];
  }

  render() {
    return (
      <table>
        <tbody>
          {
            this.state.ToDoItemTag.map((tag) => (
              <tr>
                <td>
                  {tag}
                </td>
              </tr>
            ))
          }
        </tbody>
      </table>);
  }
}

export default ToDoItemTag;