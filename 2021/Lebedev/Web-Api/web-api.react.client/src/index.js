import React from 'react';
import ReactDOM from 'react-dom';
import reportWebVitals from './reportWebVitals';
import ToDoList from './ToDoList';

const url = 'http://localhost:5000/todolist';

async function start() {

  const domContainer = document.getElementById('root');

  const response = await fetch(`${url}`, {
    headers: {
      'Content-Type': 'application/json',
      'mode': 'no-cors'
    },
    method: 'GET'
  });
  const list = await response.json();
  ReactDOM.render(
    <React.StrictMode>
      <ToDoList toDoList={list} />
    </React.StrictMode>
    ,
    domContainer);

  reportWebVitals();
}

start();
// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
