var url = 'https://localhost:44314/todo';

        function GetToDoList() {
            fetch(url).then(function (response) { response.json().then(function (toDoList) { DisplayToDoList(toDoList); }); });
            //fetch(url).then(function (response) { response.json().then(function (data) { console.log(data); }); });
        }

        function DisplayToDoList(toDoList) {
            var table = document.getElementById('toDoTable');
            table.innerHTML = "";
            var caption = document.getElementById('toDoListCaption');
            caption.style.visibility = 'visible';

            if (toDoList.length == 0) {
                caption.innerHTML = "Your to do list is empty";
                return;
            }

            caption.innerHTML = "Here is your to do list:";
            for (var i = 0; i < toDoList.length; i++) {
                var task = toDoList[i];
                var row = table.insertRow();

                var idCell = row.insertCell();
                idCell.align = 'right';
                idCell.innerHTML = task.id;

                var taskTextCell = row.insertCell();
                var taskInput = document.createElement('input');
                taskInput.type = 'text';
                taskInput.value = task.text;
                taskTextCell.appendChild(taskInput);

                var taskStatusCell = row.insertCell();
                var statusCheckbox = document.createElement('input');
                statusCheckbox.type = 'checkbox';
                if (task.isDone == true) {
                    statusCheckbox.checked = true;
                }
                taskStatusCell.appendChild(statusCheckbox);

                var saveCell = row.insertCell();
                var saveButton = document.createElement('button');
                saveButton.innerHTML = "Save changes";
                //saveButton.onClick = EditToDoTask();
                saveCell.appendChild(saveButton);

                var deleteCell = row.insertCell();
                var deleteButton = document.createElement('button');
                deleteButton.innerHTML = "Delete task";
                //var id = task.id;
                //deleteButton.onclick = (function (id) {
                //    return function () { DeleteToDoTask(id); }})(id);
                deleteCell.appendChild(deleteButton);
            }
        }

        function DoThing(id) {
            console.log(id);
        }

        async function AddTask() {
            //получаем новое задание (task status undefined если задаем через конструктор??)
            let newTaskText = document.getElementById('taskTextInput').value;
            let newTaskStatus = document.getElementById('taskStatusInput').checked;
            const newTaskBody = { Text: newTaskText, IsDone: newTaskStatus };

            
            //добавляем задание в список
            let response = await fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type' : 'application/json'
                },
                body: JSON.stringify(newTaskBody)
            });

            //очищаем поля
            document.getElementById('taskTextInput').value = "";
            document.getElementById('taskStatusInput').checked = false;

            //отображаем обновленный список
            GetToDoList();
        }

        async function DeleteToDoTask() {

            let response = await fetch(url + '/' + taskId, {
                method: 'DELETE'
            })
            GetToDoList();
        }

        async function EditToDoTask() {
            //вызываем патч от новых текста и статуса
            GetToDoList();
        }