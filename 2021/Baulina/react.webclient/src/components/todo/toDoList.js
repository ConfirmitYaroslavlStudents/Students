import React, { Component } from 'react';
import Enumerable from 'linq';
import { getAllItems, editTodoItem, addTodoItem, deleteTodoItem } from './serverRequests';
import './todoPage.css';
import AddTaskBtn from './addTaskButton';
import Table from './todoTable';
import Modal from './addEditModal'  
import SearchSelect from './searchByTagBar';

export const todoItemStatus = { notCompleted: 0, completed: 1 };

export class TodoList extends Component {

    constructor(props) {
        super(props);

        this.state = {
            todoItems: [],
            id: Number,
            tags: [],
            description: String,
            status: todoItemStatus.notCompleted,
            isShowModal : false,
            isEditMode : false,
            filteredItems: [],
            isSearchModeOr : false
        }
    }

    refreshList = async () => {
       const newState = await getAllItems();
       if(!!newState) {
           this.setState(newState);
       }
    }

    componentDidMount = async () => {
        await this.refreshList();
    }

    changeTaskDescription = (e) => {
        this.setState({ description: e.target.value });
    }

    changeStatus = (e) => {
        this.setState({ status: e.target.value });
    }

    changeTags = (e) => {
        var selectedTags = this.getTagsFromSelect(e);
        this.setState({tags: selectedTags});
    }

    getTagsFromSelect = (e) => {
        return Enumerable.from(e).select(e => e.value).toArray();
    }

    onCreateClick = () => {
        this.setState({
            isEditMode : false,
            isShowModal : true,
            description: "",
            status: todoItemStatus.notCompleted,
            tags: []
        });
    }

    onUpdateClick = (e) => {
        const todoItem = e.currentTarget.dataset;
        this.setState({
            isEditMode : true,
            isShowModal : true,
            id: todoItem.id,
            description: todoItem.description,
            status: todoItem.status,
            tags: JSON.parse(todoItem.tags)
        });
    }

    createNewTodoItem = async () => {
       const isSuccess = await addTodoItem(this.getTodoItemToAdd());
       if(isSuccess){
           this.hideModal();
           this.refreshList();  
       }
    }

    hideModal = () => {
        this.setState({isShowModal : false});
    }

    getTodoItemToAdd = () => {
        return {
            status: this.state.status,
            description: this.state.description,
            tags : this.state.tags.map(tag => ({name: tag}))
        }
    }

    editTodoItem = async () => {
        const isSuccess = await editTodoItem(this.state.id, this.getPatchRequestBody());
        if(isSuccess){
            this.hideModal();
            await this.refreshList();            
        }
    }

    getPatchRequestBody = () => {
        return [
            { op: "replace", path: "/Description", value: this.state.description },
            { op: "replace", path: "/Status", value: this.state.status },
            { op: "replace", path: "/Tags", value: this.state.tags.map(tag => ({name: tag}))}
        ];
    }

    deleteTodoItem = async (e) => {
        if (window.confirm('Are you sure?')) {
            const id = parseInt(e.currentTarget.value);
            const isSuccess = await deleteTodoItem(id);
            if(isSuccess){
                await this.refreshList();
            }
        }
    }

    getAllTags = () => {
        const todoItemsWithNotNullTags = this.state.todoItems.filter(t=> t.tags && t.tags.length>0);
        const allTags = todoItemsWithNotNullTags.flatMap(i => i.tags).map(t => t.name);
        return Enumerable.from(allTags).distinct().toArray();
    }

    searchByTags = async (e) => {
        const tagsToSearchBy = e.map(o => o.value);
        if(tagsToSearchBy.length===0){
            this.setState({filteredItems: []});
            await this.refreshList();
            return;
        }
        if(this.state.isSearchModeOr)
            return this.getItemsMatchingAnyOfTags(tagsToSearchBy);
        return this.getItemsMatchingAllTags(tagsToSearchBy);
    }

    getItemsMatchingAnyOfTags = (tagsToSearchBy) => {
        const result = this.state.todoItems.filter(t => t.tags
            && tagsToSearchBy.some(i => t.tags.map(t => t.name).includes(i)));
        this.setState({filteredItems: result});
    }

    getItemsMatchingAllTags = (tagsToSearchBy) => {
        const result = this.state.todoItems.filter(t => t.tags
            && tagsToSearchBy.every(i => t.tags.map(t => t.name).includes(i)));
        this.setState({filteredItems: result});
    }

    changeSearchMode = (data) => {
        this.setState({isSearchModeOr: !this.state.isSearchModeOr}, () => {
            this.searchByTags(data)
        })
    }

    render() {
        const {
            todoItems,
            filteredItems
        } = this.state;
        const allTags = this.getAllTags();
        const todoItemsToDisplay = filteredItems.length===0 ? todoItems : filteredItems;

        return (
            <div>
                <AddTaskBtn onClick={this.onCreateClick}/>     
                <SearchSelect allTags = {allTags} searchByTags={this.searchByTags} changeSearchMode={this.changeSearchMode}/>
                <Table todoItems={todoItemsToDisplay} onUpdateClick={this.onUpdateClick} 
                    deleteTodoItem ={this.deleteTodoItem}/>
                <Modal {...this.state} hideModal={this.hideModal} editTodoItem = {this.editTodoItem} 
                    createNewTodoItem = {this.createNewTodoItem} changeStatus = {this.changeStatus} 
                    changeTaskDescription = {this.changeTaskDescription} changeTags = {this.changeTags}
                    allTags = {allTags}/>
            </div>
        )
    }
}