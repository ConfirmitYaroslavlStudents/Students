import styled, { css } from 'styled-components';

export const InlineFlexDiv = styled.div`
  display: inline-flex;
`

export const FlexDiv = styled.div`
  display: flex;
`

export const CandidateFormWrapper = styled.div`
  padding: 15px 15px 10px;
  margin-right: 1px;
`

export const TextFieldLabel = styled.p`
  margin-top: 16px;
  margin-bottom: ${props => props.marginBottom ? props.marginBottom : '0'};;
  color: rgba(0,0,0,0.54);
  font-size: 80%;
`

export const SmallNoteSpan = styled.span`
  color: rgba(0,0,0,0.5);
  fontSize: 80%;
  float: right;
`

export const LoadingCandidateInfoWrapper = styled.div`
  height: 517px;
  width: 547px;
  display: flex;
  backgroundColor: #fff;
`

export const AddCommentDialogFormWrapper = styled.div`
  width: 470px;
`

export const CenteredDiv = styled.div`
  display: flex;
  align-items: center;
`

export const CenteredInlineDiv = styled.div`
  display: flex;
  align-items: center;
`

export const AddCommentPanelWrapper = styled.div`
  box-shadow: 0 0 15px rgba(0, 0, 0, 0.2);
  position: relative;
  width: 100%;
  clear: both;
  background: #FFF;
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

export const SubscribeButtonWrapper = styled.div`
  display: inline-block;
  position: relative;
  top: 11px;
  right: 11px;
`

export const AddCommentButtonWrapper = styled.div`
  display: inline-block;
  position: absolute;
  bottom: 0px;
  right: 5px;
`

export const QuillWrapper = styled.div`
  padding-right: 74px;
`

export const CommentWrapper = styled.div`  
  margin-bottom: 7px;
  padding: 10px 15px 0 15px; 
  
  ${props => props.right && css`
    text-align: right;
	`}
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

export const CommentAttachmentWrapper = styled.div`
  color: #42A5F5;
  cursor: pointer;
  display: inline-flex;
  align-items: center;
  
  &:hover {
    color: #64B5F6;   
   }
`

export const CommentFooter = styled.div`
  display: inline-flex;
  font-size: smaller;
  color: dimgray;
`

export const DeleteCommentWrapper = styled.div`
  display: inline-block;
  margin-top: 3px;
  margin-left: 6px;
  margin-bottom: -5px;
`

export const CommentAuthorName = styled.div`
  display: inline-block; 
  margin:  0 10px 0 1px;
  color: #222;
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

export const NoResultWrapper = styled.div`
  padding: 5px;
  color: #bbb;
  text-align: center;
  margin-bottom: 20px;
`

export const CommentPageFooter = styled.div`
  position: fixed;
  bottom: 0;
  width: 100%;
`

export const LoadingAddCommentPanelWrapper = styled.div`
  height: 164px;
  display: flex;
  background-color: #fff;
  box-shadow: 0 0 15px rgba(0, 0, 0, 0.2);
`