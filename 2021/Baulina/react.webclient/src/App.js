import React, { Component } from 'react';
import './App.css';
import { BrowserRouter, Switch, Route } from 'react-router-dom';

import { ToDoList } from './components/todo/toDoList';
import NotFound from './components/errorPages/NotFound';
import BadRequest from './components/errorPages/BadRequest';
import ItemNotFound from './components/errorPages/ItemNotFound';


class App extends Component {
  render() {
    return (
      <div className="App container">
        <h3 className="d-flex justify-content-center mt-3 mb-4">
          To-Do List
        </h3>
      <BrowserRouter>
          <Switch>
            <Route exact path="/" component={ToDoList} />
            <Route path="/error" component = {BadRequest}/>            
            <Route path="/notfound" component = {ItemNotFound}/>
            <Route path="*" component={NotFound} />
          </Switch>
      </BrowserRouter>
      </div>
    );
  }
}

export default App;
