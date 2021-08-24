import React from "react";

const url = 'https://localhost:44314/todo';

class ToDoTask extends React.Component{
  constructor(props) {
    super(props);
    this.state = {
      text: this.props.text,
      status: this.props.status,
      tags: this.props.tags
    }
  }

  render(){
    return(
      <tr>
        <td align='right'>{this.props.id}</td>
        <td>
          <input type='text' onChange={this.changeTaskText} value={this.state.text}></input>
        </td>
        <td>
          <input type='checkbox' onChange={this.changeTaskStatus} checked={this.state.status}></input>
        </td>
        <td>
          <input type='text' onChange={this.changeTaskTags} value={this.state.tags}></input>
        </td>
        <td>
          <button onClick={this.editTask}>Save changes</button>
        </td>
        <td>
          <button onClick={this.deleteTask}>Delete task</button>
        </td>
      </tr>
    )
  }

  changeTaskText = e => {
    this.setState({ text: e.target.value });
  }

  changeTaskStatus = e => {
    this.setState({ status: e.target.checked });
  }

  changeTaskTags = e => {
    this.setState({tags: e.target.value});
  }

  deleteTask = async () => {
	let urlWithTaskId = this.getUrlWithTaskId();
	await this.sendDeleteRequest(urlWithTaskId);
    await this.props.update();
  }

  getUrlWithTaskId = () => {
	  return url + '/' + this.props.id;
  }

  sendDeleteRequest = async urlWithTaskId => {
	await fetch(urlWithTaskId, {
    method: 'DELETE'
    });
  }

  editTask = async () => {
    let urlWithTaskId = this.getUrlWithTaskId();
	let taskBody = this.getTaskBodyForEdit();
	await this.sendPatchRequest(urlWithTaskId, taskBody);
    await this.props.update();
  }

  getTaskBodyForEdit = () => {
    return { text: this.state.text, isDone: this.state.status, tags: this.state.tags };
  }

  sendPatchRequest = async(urlWithTaskId, taskBody) => {
	await fetch(urlWithTaskId, {
      method: 'PATCH',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(taskBody)
    });
  }
}

export default ToDoTask;