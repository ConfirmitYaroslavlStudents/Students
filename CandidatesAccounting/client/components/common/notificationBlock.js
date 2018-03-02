import React from 'react';
import PropTypes from 'prop-types';
import styled, {css} from 'styled-components';
import NavLink from './navLink';
import DeleteIcon from 'material-ui-icons/Cancel';
import formatUserName from '../../utilities/formatUserName';
import {formatDateTime} from '../../utilities/customMoment';
import IconButton from '../common/UIComponentDecorators/iconButton';

export default function NotificationBlock(props) {
  return (
    <NavLink
      onClick={() => {
        props.getCandidate(props.notification.source.id);
        props.history.replace('/' + props.notification.source.status.toLowerCase() + 's/' + props.notification.source.id + '/comments');
        if (props.notification.recent) {
          props.noticeNotification(props.username, props.notification.id);
        }
      }}
    >
      <NotificationWrapper recent={props.notification.recent}>
        <InfoWrapper>
            <CandidateNameWrapper>{props.notification.source.name}</CandidateNameWrapper>
          <ControlsWrapper>
            <DateWrapper>
              {formatDateTime(props.notification.content.date)}
            </DateWrapper>
            <ButtonWrapper>
              <IconButton
                icon={<DeleteIcon />}
                style={{width: 24, height: 24}}
                iconStyle='small-icon'
                onClick={(event) => {
                  console.log('del');
                  props.deleteNotification(props.username, props.notification.id);
                  event.stopPropagation();
                }}
              />
            </ButtonWrapper>
          </ControlsWrapper>
        </InfoWrapper>
        <ContentWrapper>
          <p>{formatUserName(props.notification.content.author)} <ServiceText> has left the comment:</ServiceText></p>
          <MessageWrapper
            dangerouslySetInnerHTML={{__html: props.notification.content.text}}>
          </MessageWrapper>
        </ContentWrapper>
      </NotificationWrapper>
    </NavLink>
  );
}

NotificationBlock.propTypes = {
  notification: PropTypes.object.isRequired,
  noticeNotification: PropTypes.func.isRequired,
  deleteNotification: PropTypes.func.isRequired,
  username: PropTypes.string.isRequired,
  getCandidate: PropTypes.func.isRequired,
  history: PropTypes.object.isRequired,
};

const NotificationWrapper = styled.div`
  display: inline-flex;
  flex-direction: column;
  width: 400px;
  border-bottom: 1px solid rgba(0, 0, 0, 0.25);
  border-left: 5px solid #999;
  padding: 20px 12px;
  background-color: #fafafa;
  cursor: pointer;
  
  ${props => props.recent && css`  
    border-left: 5px solid #42A5F5;
    background-color: #fefefe;
	`}	
  
  &:hover {
    border-left: 5px solid #aaa;    
    background-color: #fff;    
    
    ${props => props.recent && css`    
      border-left: 5px solid #64B5F6;
    `}	
  }
`;

const InfoWrapper = styled.div`
  margin-bottom: 4px;
`;

const CandidateNameWrapper = styled.div`
  display: inline-flex;
`;

const ControlsWrapper = styled.div`
  display: inline-flex;
  float: right;
`;

const DateWrapper = styled.div`
  color: #888;
  font-size: 96%;
`;

const ButtonWrapper = styled.div`
  display: inline-flex;
  z-index: 10;
  margin-left: 4px;
  margin-right: -6px;
  margin-top: -6px;
`;

const ContentWrapper = styled.div`
  color: #000;
`;

const ServiceText = styled.span`
  color: #777;
`;

const MessageWrapper = styled.div`
  background-color: #f3f3f3;
  color: #333;
  padding: 8px;  
  word-wrap: break-word;
  overflow: hidden;
`;