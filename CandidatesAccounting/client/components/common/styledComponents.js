import styled, { css } from 'styled-components';

export const AddCandidateButtonWrapper = styled.div`
  display: inline-block;
  position: absolute;
  right: 6px;
  top: 60px;
`

export const AddCommentDialogFormWrapper = styled.div`
  width: 470px;
`

export const AddCommentButtonWrapper = styled.div`
  display: inline-block;
  position: absolute;
  bottom: 0px;
  right: 5px;
`

export const AddCommentPanelButtonsWrapper = styled.div`
  display: inline-flex;
  flex-direction: column;
  align-items: center;
  position: absolute;
  height: 100%;
  right: 2px;
  bottom: 2px;
 `

export const AddCommentPanelWrapper = styled.div`
  box-shadow: 0 0 15px rgba(0, 0, 0, 0.2);
  position: relative;
  width: 100%;
  clear: both;
  background: #FFF;
`

export const AppbarControlsWrapper = styled.div`
  display: inline-flex;
  position: absolute;
  right: 6px;
`

export const AppbarTitleWrapper = styled.div`
  display: inline-flex;
  position: absolute;
  left: 16px;
  font-size: 125%;
`

export const AppbarUsernameWrapper = styled.span`
  margin: 0 5px;
`

export const AppbarWrapper = styled.div`
  display: flex;
  align-items: center;
  width: 100%;
  height: 60px;
`

export const AttachmentFileNameWrapper = styled.div`
  display: inline-flex;
  align-items: center;
  font-size: 80%;
  padding-right: 12px;
  font-weight: bold;
  color: #665;
`

export const CandidateNameWrapper = styled.div`
  display: flex;
  align-items: center;
`

export const CandidateControlsWrapper = styled.div`
  display: flex;
  float: right;
`

export const CandidateFormWrapper = styled.div`
  padding: 15px 15px 10px;
  margin-right: 1px;
`

export const CenteredDiv = styled.div`
  display: flex;
  align-items: center;
`

export const CenteredInlineDiv = styled.div`
  display: flex;
  align-items: center;
`

export const CommentAttachmentWrapper = styled.div`
  color: #42A5F5;
  cursor: pointer;
  display: inline-flex;
  align-items: center;
  
  &:hover {
    color: #64B5F6;   
   }
`

export const CommentAuthorName = styled.div`
  display: inline-block; 
  margin:  0 10px 0 1px;
  color: #222;
`

export const CommentMount = styled.div`
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
`

export const CommentText = styled.div`
  text-align: left;
  word-wrap: break-word;
  overflow: hidden;
  font-size: 96%;
`

export const CommentMountFooter = styled.div`
  display: flex;
  margin: 0 0 0 auto;
  
  ${props => props.right && css`
    margin: 0 0 0 -3px;
	`}
`

export const CommentFooter = styled.div`
  display: inline-flex;
  font-size: smaller;
  color: dimgray;
`

export const CommentPageFooter = styled.div`
  position: fixed;
  bottom: 0;
  width: 100%;
`

export const CommentPageWrapper = styled.div`
  display: flex;
  width: 100%;
  min-height: 100vmin;
  background: #EEE;
  position: absolute;
  top: 0;
  padding-top: 110px;
  padding-bottom: 161px;
  box-sizing: border-box;
`

export const CommentWrapper = styled.div`  
  margin-bottom: 7px;
  padding: 10px 15px 0 15px; 
  
  ${props => props.right && css`
    text-align: right;
	`}
`

export const Date = styled.div`
  white-space: nowrap;
  
  ${props => props.highlighted && css`
    color: #ff4081;
    font-weight: bold;
	`}
`

export const DeleteCommentWrapper = styled.div`
  display: inline-block;
  margin-top: 3px;
  margin-left: 6px;
  margin-bottom: -5px;
`

export const DialogActionsWrapper = styled.div`
  width: 100%;
  text-align: right;
`

export const EmptyTable = styled.div`
  color: #aaa;
  text-align: center;
  position: absolute;
  width: 100%;
  margin-top: -12px;
`

export const ErrorPageWrapper = styled.div`
  width: 100%;
  min-height: 100vmin;
  background: #EEE;
  position: absolute;
  top: 0;
  padding-top: 160px;
  box-sizing: border-box;
  text-align: center;
`

export const ErrorCodeWrapper = styled.div`
  display: inline-block;
  font-size: 220%;
  color: #666;
  margin-right: 10px;
`

export const ErrorMessageWrapper = styled.div`
  display: inline-block;
  font-size: 150%;
  color: #888;
`

export const FlexDiv = styled.div`
  display: flex;
`

export const InlineFlexDiv = styled.div`
  display: inline-flex;
`
export const InputLabel = styled.p`
  margin-top: 16px;
  margin-bottom: ${props => props.marginBottom ? props.marginBottom : '0'};
  color: rgba(0,0,0,0.54);
  font-size: 80%;
`

export const LinearProgressWrapper = styled.div`
  margin: 4px -8px -8px -8px;
`

export const LoadingCandidateInfoWrapper = styled.div`
  height: 517px;
  width: 547px;
  display: flex;
  backgroundColor: #fff;
`

export const LoadingAddCommentPanelWrapper = styled.div`
  height: 164px;
  display: flex;
  background-color: #fff;
  box-shadow: 0 0 15px rgba(0, 0, 0, 0.2);
`

export const LoginFormWrapper = styled.div`
  width: 400px;
`

export const MainWrapper = styled.div`
  margin-top: 108px;
`

export const NoResultWrapper = styled.div`
  padding: 5px;
  color: #bbb;
  text-align: center;
  margin-bottom: 20px;
`

export const NotificationCenterWrapper = styled.div`
  display: flex;
  flex-direction: column;
`

export const NotificationCenterHeaderWrapper = styled.div`
  padding: 4px;
  box-shadow: 0 0 3px 3px rgba(0, 0, 0, 0.25);
  z-index: 2;
`

export const NotificationCenterControlsWrapper = styled.div`
  display: inline-flex;
  padding-top: 2px;
  float: right;
`

export const NotificationCenterButtonWrapper = styled.div`
  display: inline-flex;
  margin-left: 10px;
`

export const NotificationCenterNoNotificationsWrapper = styled.div`
  display: inline-flex;
  flex-direction: column;
  width: 400px;
  padding: 24px;
  color: #777;
  font-size: 110%;
  text-align: center;
`

export const NotificationCenterTitleWrapper = styled.div`
  display: inline-block;
  color: #636363;
  margin: 6px 0 0 8px;
`

export const NotificationAttachmentWrapper = styled.div`  
  display: inline-flex;
  align-items: center;
  color: #777;
`

export const NotificationButtonWrapper = styled.div`
  display: inline-flex;
  z-index: 10;
  margin-left: 4px;
  margin-right: -6px;
  margin-top: -6px;
`

export const NotificationCandidateNameWrapper = styled.div`
  display: inline-flex;
`

export const NotificationControlsWrapper = styled.div`
  display: inline-flex;
  float: right;
`

export const NotificationContentWrapper = styled.div`
  color: #000;
`

export const NotificationDateWrapper = styled.div`
  color: #888;
  font-size: 96%;
`

export const NotificationInfoWrapper = styled.div`
  margin-bottom: 4px;
`

export const NotificationMessageWrapper = styled.div`
  background-color: #f3f3f3;
  color: #333;
  padding: 8px;  
  word-wrap: break-word;
  overflow: hidden;
`

export const NotificationServiceText = styled.span`
  color: #777;
`

export const NotificationWrapper = styled.div`
  display: inline-flex;
  flex-direction: column;
  width: 400px;
  border-bottom: 1px solid rgba(0, 0, 0, 0.25);
  border-left: 5px solid #999;
  padding: 20px 12px;
  background-color: #fefefe;
  cursor: pointer;
  
  ${props => props.recent && css`  
    border-left: 5px solid #42A5F5;
    background-color: #fff;
	`}	
  
  &:hover {
    border-left: 5px solid #aaa;    
    background-color: #fff;    
    
    ${props => props.recent && css`    
      border-left: 5px solid #64B5F6;
    `}	
  }
`

export const PasswordInputWrapper = styled.div`
  margin-top: 24px;
`

export const QuillWrapper = styled.div`
  padding-right: 74px;
`

export const ResumeWrapper = styled.div`
  display: inline-flex;
  content-align: center;
  align-items: center;
`

export const ResumeFileName = styled.span`
  color: rgba(0, 0, 0, 0.8);
  margin-right: 4px;
  white-space: nowrap;
`

export const ResumeNotLoaded = styled.span`
  padding-left: 4px;
  color: rgba(0, 0, 0, 0.5);
`

export const SmallNoteSpan = styled.span`
  color: rgba(0,0,0,0.5);
  fontSize: 80%;
  float: right;
`

export const SortableTableActionsWrapper = styled.div`
  display: flex;
  align-items: center;
  float: right;
  margin-right: -18px;
`

export const SortableTableRowsPerPageWrapper = styled.div`
  display: inline-flex;
  align-items: center;
  spacing: 5;
  margin-right: 24px;
`

export const SortableTableFooterText = styled.span`
  margin-right: 8px;
`

export const SubscribeButtonWrapper = styled.div`
  display: inline-block;
  position: relative;
  top: 11px;
  right: 11px;
`

export const TablesBarWrapper = styled.div`
  display: flex;
  z-index: 110;
  color: rgba(0, 0, 0, 0.87);
  background-color: #f5f5f5;
  width: 100%;
  box-shadow: 0px 2px 4px -1px rgba(0, 0, 0, 0.2), 0px 4px 5px 0px rgba(0, 0, 0, 0.14), 0px 1px 10px 0px rgba(0, 0, 0, 0.12);
  height: 48px;
`

export const TabsWrapper = styled.div`
  display: inline-flex;
  position: absolute;
  left: 15%;
  right: 15%;
  height: 48px;
  margin: 0 auto;
`

export const Tabs = styled.div`
  display: inline-flex;
  margin: 0 auto;
`

export const Tab = styled.div`
  display: inline-flex;
  height: 48px;
  width: 200px;
`

export const TagWrapper = styled.div`
  display: inline-block;
  padding-top: 3px;
  min-width: 300px;
`

export const Tag = styled.div`
  cursor: pointer;
  display: inline-block;
  padding: 0px 4px;
  background: #f2f9fc;
  border: 1px solid #c9e6f2;
  border-radius: 2px;
  color: #08c;
  vertical-align: middle;
  margin-left: 3px;
  margin-bottom: 3px;
  &:hover {
    color: #2196F3;
  }
`

export const TextFieldLabel = styled.p`
  margin-top: 16px;
  margin-bottom: ${props => props.marginBottom ? props.marginBottom : '0'};;
  color: rgba(0,0,0,0.54);
  font-size: 80%;
`