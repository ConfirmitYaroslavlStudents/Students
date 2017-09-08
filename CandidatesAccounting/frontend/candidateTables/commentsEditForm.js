import React from 'react';
import BasicTable from '../materialUIDecorators/basicTable';
import {Comment} from '../candidates';
import FlatButton from '../materialUIDecorators/flatButton';

export default class CommentEditForm extends React.Component {
  constructor(props) {
    super(props);
    this.setCandidateComment = this.setCandidateComment.bind(this);
  }

  render() {
    return (
      <div>
        <BasicTable
          heads={['Date', 'Author', 'Text', <span className="float-right">Actions</span>]}
          contentRows = {[
            ['04.08.2017', 'Андрей', 'Текст комментария', <div className="float-right"><FlatButton text="edit" color="primary"/>,
              <FlatButton text="remove" color="accent"/></div>],
            ['15.02.2017', 'Ольга', 'Текст другого комментария', <div className="float-right"><FlatButton text="edit" color="primary"/>,
              <FlatButton text="remove" color="accent"/></div>]
          ]}
        />
        <div className="float-right add-btn">
          <FlatButton text="add" color="primary"/>
        </div>
      </div>
    );
  }

  setCandidateComment(index, comment) {
    this.props.setCandidateComment(index, new Comment(' a', 'd', comment));
  }
}