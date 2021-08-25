import React from 'react';

class ToDoItemTag extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      item: props.listOfTags,
      editor: props.editor
    };
  }

  render() {
    return (
      <table>
        {
          this.state.ToDoItemTag.map((tag)=>
          (<tr>
            <td>
              {tag.name}
            </td>
          </tr>))
        }
      </table>);
  }
}

export default ToDoItemTag;