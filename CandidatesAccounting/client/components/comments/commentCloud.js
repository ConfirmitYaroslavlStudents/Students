import React from 'react';
import PropTypes from 'prop-types';
import IconButton from '../common/UIComponentDecorators/iconButton';
import RemoveIcon from 'material-ui-icons/Delete';
import styled, {css} from 'styled-components';
import {formatDateTime} from '../../utilities/customMoment';

export default function CommentCloud(props) {
  let isCurrentUserComment = props.comment.author === props.userName;
  return (
    <CommentWrapper right={isCurrentUserComment}>
      <CommentMount highlighted={props.highlighted}>
        <CommentText
          dangerouslySetInnerHTML={{__html: props.comment.text}}>
        </CommentText>
      </CommentMount>
      <div> </div>
      {isCurrentUserComment ?
        <CommentFooter>
          {formatDateTime(props.comment.date)}
          <AuthorName right>
            {'Вы'}
          </AuthorName>
          <DeleteComment>
            <IconButton
              icon={<RemoveIcon/>}
              iconStyle="small-icon"
              onClick={props.deleteComment}
              style={{height: 24, width: 24}}
            />
          </DeleteComment>
        </CommentFooter>
        :
        <CommentFooter>
          <AuthorName>
            {props.comment.author}
          </AuthorName>
          {formatDateTime(props.comment.date)}
        </CommentFooter>
      }
    </CommentWrapper>
  );
}

CommentCloud.propTypes = {
  comment: PropTypes.object.isRequired,
  userName: PropTypes.string.isRequired,
  commentIndex: PropTypes.number.isRequired,
  deleteComment: PropTypes.func.isRequired,
  highlighted: PropTypes.bool,
};

const CommentWrapper = styled.div`  
  margin-bottom: 7px;
  padding: 10px 15px 0 15px; 
  
  ${props => props.right && css`
    text-align: right;
	`}
`;

const CommentMount = styled.div`
  display: inline-block;
  padding: 10px;
  box-shadow: 0 0 15px rgba(0, 0, 0, 0.15);
  max-width: 70%;
  background: #FFF;
  border-radius: 7px 7px 7px 7px;
  margin-bottom: 1px;
  
	${props => props.highlighted && css`
		background: #B3E5FC;
		box-shadow: 0 0 15px rgba(0, 0, 0, 0.3);
	`}
`;

const CommentText = styled.div`
  word-wrap: break-word;
  font-size: 96%;
`;

const CommentFooter = styled.div`
  display: inline-flex;
  font-size: smaller;
  color: dimgray;
`;

const DeleteComment = styled.div`
  display: inline-block;
  margin-top: 3px;
`;

const AuthorName = styled.div`
  display: inline-block; 
  margin:  0 15px 0 1px;
  color: #222;
  font-size: normal;
  
  ${props => props.right && css`
    margin:  0 12px;
	`}
`;