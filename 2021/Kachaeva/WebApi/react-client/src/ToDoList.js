import React from "react";
import ToDoTask from './ToDoTask';

const url = 'https://localhost:44314/todo';

class ToDoList extends React.Component{
    constructor(props) {
      super(props);
      this.state = {
        newTaskText: "",
        newTaskStatus: false,
        newTaskTags: [],
        tagsForSearch: [],
        searchCondition: "AND",
        filteredTasks: [],
        filteredTasksVisible: false,
        toDoList: []
      }
    }
  
    componentDidMount = async() => {
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
          {this.renderSearchTable()}
          {this.renderFilteredTasksTable()}
          {this.renderAddTaskTable()}    
          {this.renderToDoListTable()}
        </div>
      );
    }

    renderSearchTable = () => {
      return(
        <div style={{background:'#e0ffff'}}>
          <h4>Enter tags: </h4>
          <table>
            <tr>
              <td>
                <input type="text" onChange={this.changeTagsForSearch} value={this.state.tagsForSearch} />
              </td>
              <td>
                <select onChange={this.changeSearchCondition}>
                  <option>AND</option>
                  <option>OR</option>
                </select>
              </td>
              <td>
                <input type="button" defaultValue="Search" onClick={this.getFilteredTasks}/>
              </td>
            </tr>
          </table>
        </div>
      )
    }

    changeTagsForSearch = e => {
      this.setState({tagsForSearch: e.target.value.split(/\s*,\s*/)});
    }

    changeSearchCondition = e => {
      this.setState({searchCondition: e.target.value});
    }

    getFilteredTasks = () => {
      this.setState({filteredTasksVisible: true});
      let toDoList=this.state.toDoList;
      let tagsForSearch=this.state.tagsForSearch;
      let filteredTasks;
      if(this.state.searchCondition==="AND"){
        filteredTasks=this.getFilteredTasksWithAnd(toDoList, tagsForSearch);
      }
      else{
        filteredTasks=this.getFilteredTasksWithOr(toDoList, tagsForSearch);
      }
      this.setState({filteredTasks: filteredTasks});
    }

    getFilteredTasksWithAnd = (toDoList, tagsForSearch) => {
      return toDoList.filter(function(task) {return tagsForSearch.every(tag=>task.tags.includes(tag))});
    }

    getFilteredTasksWithOr = (toDoList, tagsForSearch) => {
      return toDoList.filter(function(task) {return tagsForSearch.some(tag=>task.tags.includes(tag))});
    }

    renderFilteredTasksTable = () => {
      if(!this.state.filteredTasksVisible) {
        return null;
      }
      if (this.state.filteredTasks.length === 0) {
        return(<h4 style={{background:'#e0ffff'}}>No matches!</h4>);
      }
      return(
        <div style={{background:'#e0ffff'}}>
          <h4>Here is your filtered to do list:</h4>
          <font size="2">
            <table>
              <thead align="center" >
                <tr>
                  <th>Id</th>
                  <th>Text</th>
                  <th>Status</th>
                  <th>Tags</th>
                </tr>
              </thead>
              <tbody>
                {this.state.filteredTasks.map(task => 
                  <tr>
                    <td>
                      <label>{task.id}</label>
                    </td>
                    <td>
                      <label>{task.text}</label>
                    </td>
                    <td>
                      <input type='checkbox' checked={task.isDone}></input>
                    </td>
                    <td>
                      <label>{task.tags.join()}</label>
                    </td>
                  </tr>
                )}
              </tbody>
            </table>
          </font>
        </div>
      );
    }
  
    renderAddTaskTable = () => {
      return(
        <div style={{background:'#e6e6fa'}}>
          <h4>Enter a new task: </h4>
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
        </div>
      )
    }
  
    changeTaskText = e => {
      this.setState({ newTaskText: e.target.value });
    }
  
    changeTaskStatus = e => {
      this.setState({ newTaskStatus: e.target.checked });
    }
  
    changeTaskTags = e => {
      this.setState({newTaskTags: e.target.value.split(/\s*,\s*/)});
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
  
    renderToDoListTable = () => {
      if (this.state.toDoList.length === 0) {
        return(<h4 style={{background:'#ffebcd'}}>Your to do list is empty</h4>);
      }
      return(
        <div style={{background:'#ffebcd'}}>
          <h4>Here is your to do list:</h4>
          <font size="2">
            <table>
              <thead align="center" >
                <tr>
                  <th>Id</th>
                  <th>Text</th>
                  <th>Status</th>
                  <th>Tags</th>
                </tr>
              </thead>
              <tbody>
                {this.state.toDoList.map(task => <ToDoTask id={task.id} text={task.text} status={task.isDone} tags={task.tags}></ToDoTask>)}
              </tbody>
            </table>
          </font>
        </div>
      );
    }
  }

  export default ToDoList;