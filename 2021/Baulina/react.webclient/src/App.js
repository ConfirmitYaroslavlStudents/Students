import React, { Component } from 'react';
import './App.css';
import { BrowserRouter, Switch, Route } from 'react-router-dom';
import { Toaster } from 'react-hot-toast';

import { TodoList } from './components/todo/todoList';
import NotFound from './components/errorPages/NotFound';
import BadRequest from './components/errorPages/BadRequest';


class App extends Component {
  render() {
    return (
      <div className="App container">
        <Toaster/>
        <h3 className="App-header">
          To-Do List
        </h3>
      <BrowserRouter>
          <Switch>
            <Route exact path="/" component={TodoList} />
            <Route path="/error" component = {BadRequest}/>      
            <Route path="*" component={NotFound} />
          </Switch>
      </BrowserRouter>
      </div>
    );
  }
}

export default App;
