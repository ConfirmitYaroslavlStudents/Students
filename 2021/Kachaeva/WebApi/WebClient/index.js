const url = 'https://localhost:44314/todo';

        function GetToDoList() {
            fetch(url).then(function (response) { response.json().then(function (toDoList) { DisplayToDoList(toDoList); }); });
        }

        function DisplayToDoList(toDoList) {
            var table = document.getElementById('toDoTable');
            table.innerHTML = "";
            var caption = document.getElementById('toDoListCaption');

            if (toDoList.length == 0) {
                caption.innerHTML = "Your to do list is empty";
                return;
            }

            caption.innerHTML = "Here is your to do list:";
            for (var i = 0; i < toDoList.length; i++) {
                var task = toDoList[i];
                var id = task.id;
                var row = table.insertRow();

                var idCell = row.insertCell();
                idCell.align = 'right';
                idCell.innerHTML = task.id;

                var taskTextCell = row.insertCell();
                var taskInput = document.createElement('input');
                taskInput.id = 'text' + id;
                taskInput.type = 'text';
                taskInput.value = task.text;
                taskTextCell.appendChild(taskInput);

                var taskStatusCell = row.insertCell();
                var statusCheckbox = document.createElement('input');
                statusCheckbox.id = 'status' + id;
                statusCheckbox.type = 'checkbox';
                if (task.isDone == true) {
                    statusCheckbox.checked = true;
                }
                taskStatusCell.appendChild(statusCheckbox);

                var saveCell = row.insertCell();
                var saveButton = document.createElement('button');
                saveButton.innerHTML = "Save changes";
                saveButton.onclick = (function (id) {
                    return function () { EditToDoTask(id); }
                })(id);
                saveCell.appendChild(saveButton);

                var deleteCell = row.insertCell();
                var deleteButton = document.createElement('button');
                deleteButton.innerHTML = "Delete task";
                deleteButton.onclick = (function (id) {
                    return function () { DeleteToDoTask(id); }})(id);
                deleteCell.appendChild(deleteButton);
            }
        }

        async function AddTask() {
            var newTaskText = document.getElementById('taskTextInput').value;
            var newTaskStatus = document.getElementById('taskStatusInput').checked;
            var newTaskBody = { Text: newTaskText, IsDone: newTaskStatus };

            let response = await fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type' : 'application/json'
                },
                body: JSON.stringify(newTaskBody)
            });

            document.getElementById('taskTextInput').value = "";
            document.getElementById('taskStatusInput').checked = false;

            GetToDoList();
        }

        async function DeleteToDoTask(taskId) {

            let response = await fetch(url + '/' + taskId, {
                method: 'DELETE'
            })

            GetToDoList();
        }

        async function EditToDoTask(taskId) {
            var taskText = document.getElementById('text'+taskId).value;
            var taskStatus = document.getElementById('status'+taskId).checked;
            var taskBody = { Text: taskText, IsDone: taskStatus };

            let response = await fetch(url + '/' + taskId, {
                method: 'PATCH',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(taskBody)
            })

            GetToDoList();
        }