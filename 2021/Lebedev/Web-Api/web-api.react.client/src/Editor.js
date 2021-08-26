import React from 'react';
import { sendPatchReqest, getToDoItemUpdate } from './Fetcher';

class Editor extends React.Component {
    item;
    fetchSender;
    newTag;

    constructor(props) {
        super(props);
        this.state = { item: {} };
        this.state.item.ToDoItemTag = [];
        this.item = this.state.item;
        this.newTag = "";
        props.connector(this);
        this.newTagChanged = this.newTagChanged.bind(this);
        this.completedChanged = this.completedChanged.bind(this);
        this.deletedChanged = this.deletedChanged.bind(this);
        this.nameChanged = this.nameChanged.bind(this);
    }

    hideEdit() {
        this.newTag = "";
        this.setState({ item: {} });
    }

    completedChanged(e) {
        this.item.completed = e.target.checked;
        this.setState({ prevState: this.state });
    }

    deletedChanged(e) {
        this.item.completed = e.target.checked;
        this.setState({ prevState: this.state });
    }

    nameChanged(e) {
        this.item.name = e.target.value;
        this.setState({ prevState: this.state });
    }

    newTagChanged(e) {
        this.newTag = e.target.value;
        this.setState({ prevState: this.state });
    }

    async deleteTag(tag) {
        let i = this.item.tag.indexOf(tag);
        if (i >= 0) {
            this.item.tag.splice(i, 1);
        }
        await sendPatchReqest(this.item);
        let itemUpdated = await getToDoItemUpdate(this.item.id)
        this.state.item.setState({
            prevState: this.state.item.state,
            item: itemUpdated
        });
        this.hideEdit();
        return;
    }

    async addTag() {
        if (this.item.tag.indexOf(this.newTag) >= 0)
            return;
        this.item.tag.push(this.newTag);
        await sendPatchReqest(this.item);
        let itemUpdated = await getToDoItemUpdate(this.item.id)
        this.state.item.setState({
            prevState: this.state.item.state,
            item: itemUpdated
        });
        this.hideEdit();
        return;
    }

    async patchItem() {
        await sendPatchReqest(this.item);
        let itemUpdated = await getToDoItemUpdate(this.item.id)
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
        else {
            this.item = this.state.item.state.item;
        }
        if (this.item.tag === undefined)
            this.item.tag = [];
        return (
            <div style={{ display: this.item.id === undefined ? 'none' : '' }}>
                <h3>Edit</h3>
                <input type="hidden" value={this.item.id}></input>
                <input type="checkbox" checked={this.item.completed} onChange={this.completedChanged} id="EditComplete"></input>
                <input type="checkbox" checked={this.item.deleted} onChange={this.deletedChanged} id="EditDelete"></input>
                <input type="text" value={this.item.name} onChange={this.nameChanged} id="EditName"></input>
                <input type="text" value={this.newTag} onChange={this.newTagChanged} placeholder="New Tag" id="TagName"></input>
                <input type="button" value="Add" onClick={() => this.addTag()} id="SaveEditButton"></input>
                <table>
                    <tbody>
                        {
                            this.item.tag.map((tag) =>
                            (<tr>
                                <td>
                                    {tag}
                                </td>
                                <td>
                                    <input type="button" value="X" onClick={() => this.deleteTag(tag)}></input>
                                </td>
                            </tr>))
                        }
                    </tbody>
                </table>
                <input type="button" value="Save" onClick={() => this.patchItem()} id="SaveEditButton"></input>
                <input type="button" onClick={() => this.hideEdit()} value="Close"></input>
            </div>);

    }
}

export default Editor;