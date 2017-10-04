import React from 'react';
import PropTypes from 'prop-types';
import DialogWindow from '../materialUIDecorators/dialogWindow';
import AddIcon from 'material-ui-icons/Add';
import { Comment } from '../candidatesClasses/index';
import moment from 'moment';
import TextInput from '../materialUIDecorators/textInput';
import styled from 'styled-components';

export default class AddCommentDialog extends React.Component {
  constructor(props) {
    super(props);
    this.state = ({commentText: ''});
  }

  render() {
    const props = this.props;
    let commentText = this.state.commentText;
    return (
      <DialogWindow
        content={
          <FormWrapper>
            <TextInput
              name="comment"
              autoFocus={true}
              multiline={true}
              placeholder="New comment"
              onChange={this.changeCommentText.bind(this)}
              value=""
            />
          </FormWrapper>}
        label="Add new comment"
        openButtonType="icon"
        openButtonContent={<AddIcon/>}
        acceptButtonContent={<div className="button-content"><AddIcon/> <span style={{marginTop: 3}}>add</span></div>}
        accept={function () {
          if (commentText.trim() !== '') {
            props.addComment(props.candidate.id, new Comment(
              'Вы',
              moment().format('H:MM:SS DD MMMM YYYY'),
              commentText));
            return true;
          } else {
            return false;
          }
        }}
      />
    );
  }

  changeCommentText(text) {
    this.setState({commentText: text});
  }
}

AddCommentDialog.propTypes = {
  candidate: PropTypes.object.isRequired,
  addComment: PropTypes.func.isRequired,
};

const FormWrapper = styled.div`
  width: 500px;
  margin: 0px 10px 10px;
`;