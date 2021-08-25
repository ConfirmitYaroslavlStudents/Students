const addButton = ({onClick}) => {
    return <div className="addTask-container">
                <button type="button" className="btn btn-primary mb-2" id="addTaskBtn" onClick={onClick}>
                    Add task
                </button>
            </div>
}

export default addButton;