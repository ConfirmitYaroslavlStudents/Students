import styled, { css } from 'styled-components'

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
	
	${props => props.deleted && css`
    background: #fafafa;
    border-color: #f44336;
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

export const CommentTextWrapper = styled.div`
  text-align: left;
  word-wrap: break-word;
  overflow: hidden;
  font-size: 96%;
`

export const CommentAuthorName = styled.div`
  display: inline-block; 
  margin:  0 10px 0 1px;
  color: #222;
`

export const CommentMountFooter = styled.div`
  display: block;
  margin: 0;
  
  ${props => props.right && css`
    margin: 0 8px 0 auto;
	`}
`

export const CommentFooter = styled.div`
  display: inline-flex;
  font-size: smaller;
  color: dimgray;
`