import { ReactComponent as DeleteLogo } from '../../assets/deleteLogo.svg';
import { ReactComponent as EditLogo } from '../../assets/editLogo.svg';
import { todoItemStatus } from './todoList';

const table = (props) => {
    return <table className="table table-bordered">
                {todoTableHead()}
                {todoTableBody(props.todoItems, props.onUpdateClick, props.deleteTodoItem)}
            </table>;
}

const todoTableHead = () => {
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

const todoTableBody = (todoItems, onUpdateClick, deleteTodoItem) => {
    return  <tbody>
                {todoItems.map(todoItem => 
                    todoTableRow(todoItem, onUpdateClick, deleteTodoItem))}
            </tbody>
}

const todoTableRow = (todoItem, onUpdateClick, deleteTodoItem) => {
    return <tr key={todoItem.id}>
            <td className="todoTableCell">
                {todoItem.description}
                <div className="tagsContainer">
                {!!todoItem.tags
                ?   todoItem.tags.map(tag => 
                    todoItemTag(tag, todoItem.id))
                : null
                }
                </div>
            </td>
            <td className="todoTableCell">
                {statusCell(todoItem.status)}
            </td>
            <td>
                {editButton(todoItem, onUpdateClick)}
                {deleteButton(todoItem, deleteTodoItem)}            
            </td>
        </tr>;
}

const todoItemTag = (tag, id) => {
    return <span className="todoItemTag border border-warning rounded mr-2" key={tag.name+id}>
            {tag.name}
        </span>
}

const statusCell = (status) => {
    return status===todoItemStatus["notCompleted"]
            ?   <span aria-label="question mark">❔</span>
            :   <span aria-label="check">✔️</span>
}

const editButton = (todoItem, onUpdateClick) => {
    return <button type="button"
                className="btn btn-outline-dark mr-2"
                data-description = {todoItem.description}
                data-status = {todoItem.status}
                data-id = {todoItem.id}
                data-tags = {JSON.stringify(todoItem.tags)}
                onClick={onUpdateClick}>
                <EditLogo />
            </button>
}

const deleteButton = (todoItem, deleteToDoItem) => {
    return <button type="button"
                className="btn btn-outline-danger mr-1"
                key={todoItem.id}
                value={todoItem.id}
                onClick={deleteToDoItem}>
                <DeleteLogo />
            </button>
}

export default table;