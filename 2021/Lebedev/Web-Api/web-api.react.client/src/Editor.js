import React from 'react';
import fetcher from './fetcher';

class Editor extends React.Component {
    item;
    fetchSender

    constructor(props) {
        super(props);
        this.fetchSender = new fetcher();
        this.state = { item: {} };
        this.item = {};
        props.connector.setEditor(this);
    }

    hideEdit() {
        this.setState({ item: {} });
    }

    completedChanged() {
        this.item.completed = (!this.item.completed);
        this.setState({ prevState: this.state });
    }

    deletedChanged() {
        this.item.deleted = (!this.item.deleted);
        this.setState({ prevState: this.state });
    }

    async deleteTag(tag) {
        let i = this.item.tag.indexOf(tag);
        if(i >= 0) {
            this.item.tag.splice(i,1);
        }
        await this.fetchSender.sendPatchReqest(this.item);
        let itemUpdated = await this.fetchSender.getToDoItemUpdate(this.item.id)
        this.state.item.setState({
            prevState: this.state.item.state,
            item: itemUpdated
        });
        this.hideEdit();
        return;
    }

    async addTag() {
        this.item.tag.push(this.getElementById("EditName").value)
        await this.fetchSender.sendPatchReqest(this.item);
        let itemUpdated = await this.fetchSender.getToDoItemUpdate(this.item.id)
        this.state.item.setState({
            prevState: this.state.item.state,
            item: itemUpdated
        });
        this.hideEdit();
        return;
    }

    async patchItem() {
        this.item.name = this.getElementById("EditName").value;
        await this.fetchSender.sendPatchReqest(this.item);
        let itemUpdated = await this.fetchSender.getToDoItemUpdate(this.item.id)
        this.state.item.setState({
            prevState: this.state.item.state,
            item: itemUpdated
        });
        this.hideEdit();
        return;
    }

    render() {
        if (this.state.item.state === undefined)
            this.item = {};
        else
            this.item = this.state.item.state.item;
        return (
            <div style={{ display: this.item.id === undefined ? 'none' : '' }}>
                <h3>Edit</h3>
                <input type="hidden" value={this.item.id}></input>
                <input type="checkbox" checked={this.item.completed} onChange={() => this.completedChanged()} id="EditComplete"></input>
                <input type="checkbox" checked={this.item.deleted} onChange={() => this.deletedChanged()} id="EditDelete"></input>
                <input type="text" value={this.item.name} id="EditName"></input>
                <input type="text" value="" id="TagName"></input>
                <input type="button" value="Add" onClick={() => this.addTag()} id="SaveEditButton"></input>
                <table>
        {
          this.state.ToDoItemTag.map((tag)=>
          (<tr>
            <td>
              {tag.name}
            </td>
            <td>
            <input type="button" value="X" onClick={() => this.deleteTag(tag)}></input>
            </td>
          </tr>))
        }
      </table>
                <input type="button" value="Save" onClick={() => this.patchItem()} id="SaveEditButton"></input>
                <input type="button" onClick={() => this.hideEdit()} value="Close"></input>
            </div>);
    }
}

export default Editor;