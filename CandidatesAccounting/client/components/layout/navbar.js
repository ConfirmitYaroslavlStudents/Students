import React from 'react';
import styled from 'styled-components';
import Appbar from './appbar';
import TablesBar from './tablesbar';

export default function Navbar(props) {
  return (
    <NavbarWrapper>
      <Appbar
        title={props.pageTitle}
        applicationStatus={props.applicationStatus}
        username={props.username}
        login={props.login}
        logout={props.logout}
        notifications={props.notifications}
        noticeNotification={props.noticeNotification}
        deleteNotification={props.deleteNotification}
        history={props.history}
        searchRequest={props.searchRequest}
        loadCandidates={props.loadCandidates}
        getCandidate={props.getCandidate}
        setState={props.setState}
      />
      <TablesBar
        newCandidateDefaultType={props.candidateStatus}
        addCandidate={props.addCandidate}
        tags={props.tags}
        username={props.username}
        history={props.history}
        candidatesPerPage={props.candidatesPerPage}
        totalCount={props.totalCount}
        loadCandidates={props.loadCandidates}
        candidateStatus={props.candidateStatus}
      />
    </NavbarWrapper>
  );
}

const NavbarWrapper = styled.div`
  position: fixed;
  width: 100%;
  top: 0;
  left: 0;
  height: 108px;
  z-index: 100;
`;