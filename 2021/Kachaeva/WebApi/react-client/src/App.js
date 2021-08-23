import React from "react";

const url = 'https://localhost:44314/todo';

class App extends React.Component{
  constructor(props) {
    super(props);
    this.state = {
      text: "",
      status: false,
      toDoList: []
    };
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
            <input type="text" id="taskTextInput" onChange={this.changeTaskText} value={this.state.text} />
          </td>
          <td>
            <input type="checkbox" id="taskStatusInput" onChange={this.changeTaskStatus} checked={this.state.status}/>
          </td>
          <td>
            <input type="button" defaultValue="Add task" onClick={this.addTask}/>
          </td>
        </tr>
      </table>
    )
  }

  changeTaskText = e => {
    this.setState({ text: e.target.value });
  }

  changeTaskStatus = e => {
    this.setState({ status: e.target.checked });
  }

  renderToDoListTable = () => {
    if (this.state.toDoList.length === 0) {
      return(<h3>Your to do list is empty</h3>);
    }
    return(
      <div>
        <h5>Here is your to do list:</h5>
        <table>
          {this.state.toDoList.map(task => this.renderRow(task))}
        </table>
      </div>
    );
  }

  renderRow = task => {
    return(
      <tr>
        <td align='right'>{task.id}</td>
        <td>
          <input type='text' value={task.text}></input>
        </td>
        <td>
          <input type='checkbox' checked={task.isDone}></input>
        </td>
        <td>
          <button onClick={() => this.editTask(task.id, task.text, task.isDone)}>Save changes</button>
        </td>
        <td>
          <button onClick={() => this.deleteTask(task.id)}>Delete task</button>
        </td>
      </tr>
    )
  }

  addTask = async() => {
    let taskBody = this.getTaskBodyForAdd();
	  await this.sendPostRequest(taskBody);
	  this.clearAddFields();
    await this.updateToDoList();
  }

  getTaskBodyForAdd = () => {
	let taskText = this.state.text;
  let taskStatus = this.state.status;
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
      text: "",
      status: false
    });
  }

  deleteTask = async taskId => {
	  let urlWithTaskId = this.getUrlWithTaskId(taskId);
	  await this.sendDeleteRequest(urlWithTaskId);
    await this.updateToDoList();
  }

  getUrlWithTaskId = taskId => {
	  return url + '/' + taskId;
  }

  sendDeleteRequest = async urlWithTaskId => {
	  await fetch(urlWithTaskId, {
      method: 'DELETE'
    });
  }

  editTask = async (taskId, taskText, taskStatus) => {
    let urlWithTaskId = this.getUrlWithTaskId(taskId);
	  let taskBody = this.getTaskBodyForEdit(taskText, taskStatus);
	  await this.sendPatchRequest(urlWithTaskId, taskBody);
    await this.updateToDoList();
  }

  getTaskBodyForEdit = (taskText, taskStatus) => {
    return { text: taskText, isDone: taskStatus };
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
};

export default App;
