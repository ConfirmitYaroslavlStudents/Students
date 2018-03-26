import React from 'react';
import PropTypes from 'prop-types';
import IconButton from '../common/UIComponentDecorators/iconButton';
import RemoveIcon from 'material-ui-icons/Delete';
import AttachIcon from 'material-ui-icons/AttachFile';
import FileUploader from 'react-input-files';
import styled, {css} from 'styled-components';
import {formatDateTime} from '../../utilities/customMoment';
import formatUserName from '../../utilities/formatUserName';

export default function CommentCloud(props) {
  return (
    <CommentWrapper right={props.isCurrentUserComment}>
      <CommentMount markerColor={props.markerColor} isSystem={props.isSystem} right={props.isCurrentUserComment}>
        {
          props.isSystem ?
            <svg height="24px" width="24px" fill="#FF8F00" focusable="false" viewBox="0 0 24 24" style={{margin: 'auto 5px auto 0'}}>'+'
              <path
                d="M11 17h2v-6h-2v6zm1-15C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8zM11 9h2V7h-2v2z"/>
              '+'
            </svg> : ''
        }
        <CommentText
          dangerouslySetInnerHTML={{__html: props.comment.text}}>
        </CommentText>
        {
          props.comment.attachment ?
            <MountFooter right={!props.isCurrentUserComment}>
              <AttachmentWrapper onClick={() => {
                window.open(window.location.origin + '/' + props.candidate.status.toLowerCase() + 's/' + props.candidate.id + '/comments/' + props.comment.id + '/attachment', '_self');
              }}>
                <AttachIcon style={{width: 16, height: 16}}/>{props.comment.attachment}
              </AttachmentWrapper>
            </MountFooter>
            : ''
        }
      </CommentMount>
      <div> </div>
      <CommentFooter>
        {
          !props.isSystem && !props.isCurrentUserComment ?
            <AuthorName>
              {formatUserName(props.comment.author)}
            </AuthorName> : ''
        }
      {formatDateTime(props.comment.date)}
      {
        props.isCurrentUserComment ?
          <DeleteComment>
            <IconButton
              icon={<RemoveIcon style={{height: 20, width: 20}}/>}
              size="small"
              onClick={props.deleteComment}
              style={{height: 24, width: 24}}
            />
          </DeleteComment> : ''
      }
    </CommentFooter>
    </CommentWrapper>
  );
}

CommentCloud.propTypes = {
  comment: PropTypes.object.isRequired,
  deleteComment: PropTypes.func.isRequired,
  candidate: PropTypes.object.isRequired,
  markerColor: PropTypes.string,
  isSystem: PropTypes.bool,
  isCurrentUserComment: PropTypes.bool
};

const CommentWrapper = styled.div`  
  margin-bottom: 7px;
  padding: 10px 15px 0 15px; 
  
  ${props => props.right && css`
    text-align: right;
	`}
`;

const CommentMount = styled.div`
  display: inline-flex;
  flex-direction: column;
  padding: 11px 11px 11px 7px;
  box-shadow: 2px 2px 3px 1px rgba(0, 0, 0, 0.20);
  max-width: 85%;
  background: #FFF;
  border-radius: 2px;
  margin-bottom: 1px;
  border-left: 5px solid #999;
  border-color: ${props => props.markerColor};
	
	${props => props.right && css`
    border-left: none;
    border-right: 5px solid #999;
    border-color: #3949AB;
    padding: 11px 7px 11px 11px;
    margin-bottom: 0;
	`}
	
	${props => props.isSystem && css`
	  flex-direction: row;
	  color: #905600;
    background-color: #FFF3E0;
    border-color: #FF9800;
    border-radius: 0px;
    padding: 13px 13px 13px 7px;
	`}
`;

const CommentText = styled.div`
  text-align: left;
  word-wrap: break-word;
  overflow: hidden;
  font-size: 96%;
`;

const MountFooter = styled.div`
  display: flex;
  margin: 0 0 0 auto;
  
  ${props => props.right && css`
    margin: 0 0 0 -3px;
	`}
`;

const AttachmentWrapper = styled.div`
  color: #42A5F5;
  cursor: pointer;
  display: inline-flex;
  align-items: center;
  
  &:hover {
    color: #64B5F6;   
   }
`;

const CommentFooter = styled.div`
  display: inline-flex;
  font-size: smaller;
  color: dimgray;
`;

const DeleteComment = styled.div`
  display: inline-block;
  margin-top: 3px;
  margin-left: 6px;
  margin-bottom: -5px;
`;

const AuthorName = styled.div`
  display: inline-block; 
  margin:  0 10px 0 1px;
  color: #222;
`;