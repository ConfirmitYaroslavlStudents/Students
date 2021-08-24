import React from "react";
import ToDoTask from './ToDoTask';

const url = 'https://localhost:44314/todo';

class ToDoList extends React.Component{
    constructor(props) {
      super(props);
      this.state = {
        newTaskText: "",
        newTaskStatus: false,
        newTaskTags:"",
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
              <input type="text" onChange={this.changeTaskTags} value={this.state.newTaskTags} />
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
  
    changeTaskTags = e => {
      this.setState({newTaskTags: e.target.value});
    }
  
    renderToDoListTable = () => {
      if (this.state.toDoList.length === 0) {
        return(<h3>Your to do list is empty</h3>);
      }
      return(
        <div>
          <h5>Here is your to do list:</h5>
          <table>
            {this.state.toDoList.map(task => <ToDoTask id={task.id} text={task.text} status={task.isDone} tags={task.tags} update={this.updateToDoList}></ToDoTask>)}
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
    let taskTags=this.state.newTaskTags;
    return { text: taskText, isDone: taskStatus, tags: taskTags };
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
        newTaskStatus: false,
        newTaskTags: ""
      });
    }
  }

  export default ToDoList;