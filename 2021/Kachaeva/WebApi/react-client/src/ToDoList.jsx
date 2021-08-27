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
      const toDoList=await this.getToDoList();
      this.setState({ toDoList: toDoList });
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
            <tbody>
            <tr>
              <td>
                <input type="text" title="tagsForSearch" onChange={this.changeTagsForSearch} value={this.state.tagsForSearch} />
              </td>
              <td>
                <select title="searchCondition" onChange={this.changeSearchCondition}>
                  <option>AND</option>
                  <option>OR</option>
                </select>
              </td>
              <td>
                <input type="button" defaultValue="Search" onClick={this.getFilteredTasks}/>
              </td>
            </tr>
            </tbody>
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
      const fn = (taskTags) => this.state.searchCondition === "AND" ? this.state.tagsForSearch.every((tag=>taskTags.includes(tag))) : this.state.tagsForSearch.some((tag=>taskTags.includes(tag)));
      const filteredTasks = this.state.toDoList.filter(function(task) {return fn(task.tags)});
      this.setState({filteredTasks: filteredTasks});
    }

    getFilteredTasksWithOp = (toDoList, tagsForSearch,op) => {
      const fn = (tagsForSearch,taskTags) => op === "AND" ? tagsForSearch.every((tag=>taskTags.includes(tag))) : tagsForSearch.some((tag=>taskTags.includes(tag)));
      return toDoList.filter(function(task) {return fn(tagsForSearch,task.tags)});
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
                  <tr key={task.id}>
                    <td>
                      <label>{task.id}</label>
                    </td>
                    <td>
                      <label>{task.text}</label>
                    </td>
                    <td>
                      <input title="status" type='checkbox' checked={task.isDone} readOnly={true}></input>
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
            <tbody>
            <tr>
              <td>
                <input title="newTaskText" type="text" onChange={this.changeTaskText} value={this.state.newTaskText} />
              </td>
              <td>
                <input title="newTaskStatus" type="checkbox" onChange={this.changeTaskStatus} checked={this.state.newTaskStatus}/>
              </td>
              <td>
                <input title="newTaskTags" type="text" onChange={this.changeTaskTags} value={this.state.newTaskTags} />
              </td>
              <td>
                <input type="button" defaultValue="Add task" onClick={this.addTask}/>
              </td>
            </tr>
            </tbody>
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
      const taskBody = this.getTaskBodyForAdd();
      await this.sendPostRequest(taskBody);
      this.clearAddFields();
      await this.updateToDoList();
    }
  
    getTaskBodyForAdd = () => {
      return { text: this.state.newTaskText, isDone: this.state.newTaskStatus, tags: this.state.newTaskTags };
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
                {this.state.toDoList.map(task => <ToDoTask key={task.id} id={task.id} text={task.text} status={task.isDone} tags={task.tags}></ToDoTask>)}
              </tbody>
            </table>
          </font>
        </div>
      );
    }
  }

  export default ToDoList;