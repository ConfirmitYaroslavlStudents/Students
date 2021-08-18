import React, { Component } from 'react';
import { ReactComponent as DeleteLogo } from '../../assets/deleteLogo.svg';
import { ReactComponent as EditLogo } from '../../assets/editLogo.svg';
import { sendGetRequest, sendPatchRequest, sendPostRequest, sendDeleteRequest } from './serverRequests';
import './toDoList.css'

const toDoItemStatus = { "Not Complete": 0, "Complete": 1 };

export class ToDoList extends Component {

    constructor(props) {
        super(props);

        this.state = {
            todoItems: [],
            modalTitle: "",
            id: 0,
            description: "",
            status: toDoItemStatus.NotComplete,
            isShowModal : false
        }
    }

    async refreshList() {
       const newState = await sendGetRequest();
       if(!!newState) {
           this.setState(newState);
       }
    }

    async componentDidMount() {
        await this.refreshList();
    }

    changeTaskDescription = (e) => {
        this.setState({ description: e.target.value });
    }

    changeStatus = (e) => {
        if(isNaN(e.target.value)){
            alert("You haven't selected the status");
        }
        else this.setState({ status: e.target.value });
    }

    addClick() {
        this.setState({
            isShowModal : true,
            modalTitle: "Add task",
            description: "",
            status: toDoItemStatus.NotComplete,
        });
    }

    editClick(todoItem) {
        this.setState({
            isShowModal : true,
            id: todoItem.id,
            modalTitle: "Edit",
            description: todoItem.description,
            status: todoItem.status
        });
    }

    async createClick() {
       const successResult = await sendPostRequest(this.getToDoItemToAdd());
       if(successResult){
           this.hideModal();
           this.refreshList();  
       }
    }

    hideModal(){
        this.setState({isShowModal : false});
    }

    getToDoItemToAdd() {
        return {
            status: this.state.status,
            description: this.state.description
        }
    }

    async updateClick() {
        const successResult = await sendPatchRequest(this.state.id, this.getPatchRequestBody());
        if(successResult){
            this.hideModal();
            this.refreshList();            
        }
    }

    getPatchRequestBody() {
        return [
            { op: "replace", path: "/Description", value: this.state.description },
            { op: "replace", path: "/Status", value: this.state.status }
        ];
    }

    async deleteClick(id) {
        if (window.confirm('Are you sure?')) {
            const successResult = await sendDeleteRequest(id);
            if(successResult){
                this.refreshList();
            }
        }
    }

    render() {
        const {
            todoItems,
            modalTitle,
            description,
            isShowModal
        } = this.state;

        return (
            <div>
                {this.renderAddTaskButton()}                
                {this.renderToDoTable(todoItems)}
                {this.renderModal(modalTitle, description, isShowModal)}
            </div>
        )
    }

    renderAddTaskButton() {
        return <div className="d-flex justify-content-end">
            <button type="button" className="btn btn-primary mb-4" onClick={() => this.addClick()}>
                Add task
            </button>
        </div>;
    }

    renderToDoTable(todoItems) {
        return <table className="table table-bordered">
            {this.renderToDoTableHead()}
            {this.renderToDoTableBody(todoItems)}
        </table>;
    }

    renderToDoTableHead() {
        return <thead className="thead-dark">
            <tr>
                <th className="text-center">
                    Task
                </th>
                <th className="text-center">
                    Status
                </th>
                <th className="text-center">
                    Options
                </th>
            </tr>
        </thead>;
    }

    renderToDoTableBody(todoItems) {
        return <tbody>
            {todoItems.map(todoItem => 
            this.generateToDoTableRow(todoItem)
            )}
        </tbody>;
    }

    generateToDoTableRow(todoItem) {
        return <tr key={todoItem.id}>
            <td id="descriptionCell">{todoItem.description}</td>
            <td id="statusCell">
                {todoItem.status===toDoItemStatus["Not Complete"]?
                        <span role="img" aria-label="cross">❌</span>
                    :   <span role="img" aria-label="check">✔️</span>
                }
            </td>
            <td>
                <button type="button"
                    className="btn btn-outline-dark mr-2"
                    onClick={() => this.editClick(todoItem)}>
                    <EditLogo />
                </button>

                <button type="button"
                    className="btn btn-outline-danger mr-1"
                    onClick={() => this.deleteClick(todoItem.id)}>
                    <DeleteLogo />
                </button>

            </td>
        </tr>;
    }

    renderModal(modalTitle, description, isShowModal) {
        return isShowModal?
         <div className="modal" id="addEditModal" data-backdrop="static" 
            data-keyboard="false" tabIndex="-1" role="document">
            <div className="modal-dialog modal-dialog-centered">
                <div className="modal-content">
                    {this.generateModalHeader(modalTitle)}
                    <div className="modal-body">
                        <div className="d-flex flex-row">

                            <div className="p-2 flex-fill">
                                {this.renderTaskInputGroup(description)}
                                {this.renderStatusInputGroup()}
                            </div>
                        </div>
                    </div>
                    {this.renderModalFooter(modalTitle)}
                </div>
            </div>
        </div>
        : null
    }

    generateModalHeader(modalTitle) {
        return <div className="modal-header" id="addEditModalHeader">
            <h5 className="modal-title">{modalTitle}</h5>
            <button type="button" className="close" onClick={()=> this.hideModal()} aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>;
    }

    renderStatusInputGroup() {
        return <div className="input-group mb-3 input-group-lg">
            <div className="input-group-prepend">
                <span className="input-group-text">Status</span>
            </div>
            <select className="custom-select" onChange={this.changeStatus}>
                <option defaultValue>Select status</option>
                {Object.entries(toDoItemStatus).map(([key, value]) => <option defaultValue value={value} key={value}>{key}</option>)}
            </select>
        </div>;
    }

    renderTaskInputGroup(description) {
        return <div className="input-group mb-3 input-group-lg">
            <div className="input-group-prepend">
                <span className="input-group-text">Task</span>
            </div>
            <input type="text" className="form-control" placeholder="What do you need to do?"
                value={description}
                onChange={this.changeTaskDescription} />
        </div>;
    }

    renderModalFooter(modalTitle) {
        return <div className="modal-footer">
            <button type="button" className="btn btn-secondary" onClick={() => this.hideModal()} >Close</button>
            {modalTitle === "Add task" ?
                <button type="button" className="btn btn-primary float-start" onClick={() => this.createClick()}>
                    Create
                </button>
                : <button type="button" className="btn btn-primary float-start" onClick={() => this.updateClick()}>
                    Update
                </button>}
        </div>;
    }
}