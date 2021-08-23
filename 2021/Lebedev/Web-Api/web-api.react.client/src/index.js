import React from 'react';
import ReactDOM from 'react-dom';
import reportWebVitals from './reportWebVitals';
import ToDoList from './ToDoList';
import Adder from './Adder';
import Editor from './Editor';

const url = 'http://localhost:5000/todolist';
var adder;
var editor;

class Connector {
  setAdder(adderProp) {
    adder = adderProp;
  }

  getAdder() {
    return adder;
  }
  setEditor(editorProp) {
    editor = editorProp;
  }

  getEditor() {
    return editor;
  }
}

async function start() {

  const domContainer = document.getElementById('root');

  const response = await fetch(`${url}`, {
    headers: {
      'Content-Type': 'application/json',
      'mode': 'no-cors'
    },
    method: 'GET'
  });
  const connector = new Connector();
  const list = await response.json();
  ReactDOM.render(
    <div>
      <h3>ToDoList</h3>
      <Adder connector={connector} />
      <Editor connector={connector} />
      <table border="solid">
        <thead>
          <tr>
            <th>Name</th>
            <th>Completed</th>
            <th>Deleted</th>
            <th></th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <React.StrictMode>
            <ToDoList toDoList={list} connector={connector} />
          </React.StrictMode>
        </tbody>
      </table>
    </div>
    ,
    domContainer);

  reportWebVitals();
}

start();
export default Connector;
// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
