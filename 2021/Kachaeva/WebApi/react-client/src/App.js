import React from "react";

const url = 'https://localhost:44314/todo';

class ToDoTask extends React.Component{
  constructor(props) {
    super(props);
    this.state = {
      text: this.props.text,
      status: this.props.status
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

  deleteTask = async () => {
	  let urlWithTaskId = this.getUrlWithTaskId();
	  await this.sendDeleteRequest(urlWithTaskId);
    await this.props.onUpdate();
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
    await this.props.onUpdate();
  }

  getTaskBodyForEdit = () => {
    return { text: this.state.text, isDone: this.state.status };
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

class ToDoList extends React.Component{
  constructor(props) {
    super(props);
    this.state = {
      newTaskText: "",
      newTaskStatus: false,
      toDoList: []
    }
  }

  componentDidMount = async() =>{
    await this.updateToDoList();
  }

  updateToDoList = async() => {
    this.setState({ toDoList: await this.getToDoList() });
  }

  getToDoList = async() => {
	  let response = await fetch(url);
	  let toDoList = await response.json();
	  return toDoList;
  }

  render() {
    return (
      <div>
        <h2>To do list</h2>
        {this.renderAddTaskTable()}    
        {this.renderToDoListTable()}
      </div>
    );
  }

  renderAddTaskTable = () => {
    return(
      <table>
        <tr>
          <td>
            <input type="text" onChange={this.changeTaskText} value={this.state.newTaskText} />
          </td>
          <td>
            <input type="checkbox" onChange={this.changeTaskStatus} checked={this.state.newTaskStatus}/>
          </td>
          <td>
            <input type="button" defaultValue="Add task" onClick={this.addTask}/>
          </td>
        </tr>
      </table>
    )
  }

  changeTaskText = e => {
    this.setState({ newTaskText: e.target.value });
  }

  changeTaskStatus = e => {
    this.setState({ newTaskStatus: e.target.checked });
  }

  renderToDoListTable = () => {
    if (this.state.toDoList.length === 0) {
      return(<h3>Your to do list is empty</h3>);
    }
    return(
      <div>
        <h5>Here is your to do list:</h5>
        <table>
          {this.state.toDoList.map(task => <ToDoTask id={task.id} text={task.text} status={task.isDone} onUpdate={this.updateToDoList}></ToDoTask>)}
        </table>
      </div>
    );
  }

  addTask = async() => {
    let taskBody = this.getTaskBodyForAdd();
	  await this.sendPostRequest(taskBody);
	  this.clearAddFields();
    await this.updateToDoList();
  }

  getTaskBodyForAdd = () => {
	let taskText = this.state.newTaskText;
  let taskStatus = this.state.newTaskStatus;
  return { text: taskText, isDone: taskStatus };
  }

  sendPostRequest = async taskBody => {
	  await fetch(url, {
      method: 'POST',
      headers: {
        'Content-Type' : 'application/json'
      },
      body: JSON.stringify(taskBody)
    });
  }

  clearAddFields = () => {
	  this.setState({
      newTaskText: "",
      newTaskStatus: false
    });
  }
}

export default ToDoList;
