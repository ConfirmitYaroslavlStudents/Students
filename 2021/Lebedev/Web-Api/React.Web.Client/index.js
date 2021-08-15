const create = React.createElement;

class ToDoListPrinter extends React.Component {

  constructor(props) {
    super(props);
    this.state = { items: [] };
    this.state.items = props.toDoList;
  }

  async deleteItem(id) {
    const list = await sendDelete(id);
    this.setState({items:list})
  }

  editItem(item) {
    document.getElementById('EditName').value = item.name;
    document.getElementById('EditId').value = item.id;
    document.getElementById('EditDelete').checked = item.deleted,
    document.getElementById('EditComplete').checked = item.completed;
    document.getElementById('EditForm').style.display = 'block';
  }

  async addItem() {
    const addNameTextbox = document.getElementById('AddName');
    const list = await sendAddItem(addNameTextbox.value.trim());
    this.setState({items:list})
    addNameTextbox.value = '';
  }

  async patchItem() {
    const itemId = document.getElementById('EditId').value;
    const item = {
      id: parseInt(itemId, 10),
      name: document.getElementById('EditName').value.trim(),
      completed: document.getElementById('EditComplete').checked,
      deleted: document.getElementById('EditDelete').checked
    };
    const list = await sendPatchReqest(item)
    this.setState({items:list})
    hideEdit();
  }

  createInput(props) {
    return create(
      'input',
      props,
      null
    )
  }

  createTableItem(props) {
    return create(
      'td',
      props.props,
      props.children
    );
  }

  createToDoItem(item) {
    return [
      this.createTableItem({
        props:{key:`${item.id*6+1}`},
        children:`${item.name}`
      }),
      this.createTableItem({
        props:{key:`${item.id*6+2}`},
        children:
          this.createInput({
            type:"checkbox",
            checked:item.completed, 
            disabled:true
          })
      }),
      this.createTableItem({
        props:{key:`${item.id*6+3}`},
        children:
          this.createInput({
            type:"checkbox",
            checked:item.deleted, 
            disabled:true
          })
      }),
      this.createTableItem({
        props:{key:`${item.id*6+4}`},
        children:
          this.createInput({
            onClick:()=>this.editItem(item),
            type:"button",
            value:"Edit"
          })
      }),
      this.createTableItem({
        props:{key:`${item.id*6+5}`},
        children:
          this.createInput({
            onClick:()=>this.deleteItem(item.id),
            type:"button",
            value:"Delete"
          })
      })
    ];
  }

  render() {
    document.getElementById('SaveEditButton').onclick = ()=>this.patchItem();
    document.getElementById('AddItemButton').onclick = ()=>this.addItem();
    return this.state.items.map((item) => create('tr',{key:item.id},this.createToDoItem(item)));
  }
}
