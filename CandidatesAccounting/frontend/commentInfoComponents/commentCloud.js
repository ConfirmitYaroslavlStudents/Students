import React from 'react';
import PropTypes from 'prop-types';
import AvatarIcon from 'material-ui-icons/AccountCircle';
import IconButton from '../materialUIDecorators/iconButton';
import RemoveIcon from 'material-ui-icons/RemoveCircleOutline';
import styled, { css } from 'styled-components';

export default function CommentCloud(props) {
  return (
    <CommentWrapper>
      <CommentIcons right={props.comment.author === props.userName}>
        <AvatarIcon/>
        {props.comment.author === props.userName ?
          <DeleteComment>
            <IconButton
              icon={<RemoveIcon/>}
              onClick={function () {
                props.deleteComment(props.commentIndex)
              }}
            />
          </DeleteComment>
          :
          ''
        }
      </CommentIcons>
      <CommentMount userComment={props.comment.author === props.userName}>
        <CommentText
          dangerouslySetInnerHTML={{__html: props.comment.text}}>
        </CommentText>
        <CommentFooter>
          {props.comment.date}
          <AuthorName right={props.comment.author === props.userName}>
            {props.comment.author}
          </AuthorName>
        </CommentFooter>
      </CommentMount>
    </CommentWrapper>
  );
}

CommentCloud.propTypes = {
  comment: PropTypes.object.isRequired,
  userName: PropTypes.string.isRequired,
  commentIndex: PropTypes.number.isRequired,
  deleteComment: PropTypes.func.isRequired,
};

const CommentWrapper = styled.div`  
  margin: 0 0 10px;
  overflow: hidden;
  padding: 5px 0;
`;

const CommentIcons = styled.div`
  display: inline-block;
  float: left;
  margin: 0 10px 0 10px;
    
  ${props => props.right && css`
		float: right;
		margin: 0 0 0 10px;
	`}
`;

const CommentMount = styled.div`
  display: inline-block;
  padding: 10px;
  box-shadow: 0 0 15px rgba(0, 0, 0, 0.2);
  max-width: 70%;
  float: left;
  background: #FFF;
  border-radius: 2px 7px 7px 7px;
  
  ${props => props.userComment && css`
		float: right;
		border-radius: 7px 2px 7px 7px;
	`}
`;

const CommentText = styled.div`
  word-wrap: break-word;
  font-size: 96%;
`;

const CommentFooter = styled.div`  
  text-align: left;
  font-size: smaller;
  color: dimgray;
  text-align = left;
  
  ${props => props.right && css`
		text-align: right;
	`}
`;

const DeleteComment = styled.div`  
  margin-left: -12px;
`;

const AuthorName = styled.div`
  float: left;
  margin-right: 20px;
  
  ${props => props.right && css`
    float: right;
    margin-right: 0px;
    margin-left: 20px;
	`}
`;