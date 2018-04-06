import styled from 'styled-components';

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
  width: 504px;
`

export const CenteredDiv = styled.div`
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