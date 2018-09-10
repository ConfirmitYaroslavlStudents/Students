import React from 'react'
import styled from 'styled-components'

const PagePlaceholder = () => {
  return (
    <React.Fragment>
      <AppbarPlaceholder />
      <TablebarPlaceholder />
      <ContentPlaceholder>
        CandidateAccounting is loading...
      </ContentPlaceholder>
    </React.Fragment>
  )
}

const AppbarPlaceholder = styled.div`
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 60px;
  background-color: #3F51B5;
  z-index: 10;
`

const TablebarPlaceholder = styled.div`
  position: fixed;
  top: 60px;
  left: 0;
  width: 100%;
  height: 48px;
  background-color: #f5f5f5;
  box-shadow: 0 0 4px 4px rgba(0,0,0,0.2);
  z-index: 5;
`

const ContentPlaceholder = styled.div`
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: #ccc;
  font-size: 180%;
  font-weight: bold;
  color: #888;
  font-family: sans-serif;
  text-align: center;
  padding-top: 185px;
`

export default PagePlaceholder