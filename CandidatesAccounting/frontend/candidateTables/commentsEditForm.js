import React from 'react';
import Avatar from 'material-ui-icons/AccountCircle';
import TextInput from '../materialUIDecorators/textInput';
import IconButton from 'material-ui/IconButton';
import AddIcon from 'material-ui-icons/Add';

export default class CommentEditForm extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <div style={{display: 'inline-block', width: 400, padding: 20}}>
        <div className="comment">
          <Avatar />
          <p>Павел <span className="comment-date">13:23 07.08.17</span></p>
          <p>Какой-то комментарий от Павла</p>
        </div>
        <div className="comment user-comment">
          <Avatar />
          <p>Вы <span className="comment-date">15:43 07.08.17</span></p>
          <p>Ваше замечание насчёт кандидата</p>
        </div>
        <div className="comment">
          <Avatar />
          <p>Ольга <span className="comment-date">15:48 07.08.17</span></p>
          <p>Комментарий Ольги</p>
        </div>
        <div style={{display:'inline-block', width: '86%'}}>
          <TextInput
            name="comment"
            placeholder="New comment"
            autoFocus={true}
            multiline={true}
          />
        </div>
        <IconButton><AddIcon /></IconButton>
      </div>
    );
  }
}