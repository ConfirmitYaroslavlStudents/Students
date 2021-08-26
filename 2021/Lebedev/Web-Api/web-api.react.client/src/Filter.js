import React from 'react';

class Filter extends React.Component {
    newTag;
    tags;
    list;

    constructor(props) {
        super(props);
        this.list = props.connector;
        this.tags = [];
        this.newTag = "";
        this.newTagChanged = this.newTagChanged.bind(this);
        this.addTag = this.addTag.bind(this);
    }

    newTagChanged(e) {
        this.newTag = e.target.value;
        this.setState({ prevState: this.state });
    }

    addTag() {
        if (!(this.tags.indexOf(this.newTag) >= 0)) {
            this.tags.push(this.newTag);
            this.list.setState({
                prevState: this.list.state,
                filter: this.tags
            });
        }
    }

    deleteTag(tag) {
        let i = this.tags.indexOf(tag);
        if (i >= 0) {
            this.tags.splice(i, 1);
            this.list.setState({
                prevState: this.list.state,
                filter: this.tags
            });
        }
    }

    render() {
        return (
            <div>
                <h3>Filter</h3>
                <input type="text" value={this.newTag} onChange={this.newTagChanged} placeholder="New Tag" id="TagName"></input>
                <input type="button" value="Add" onClick={this.addTag} id="SaveEditButton"></input>
                <table>
                    <tbody>
                        {
                            this.tags.map((tag) =>
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
            </div>);

    }
}

export default Filter;