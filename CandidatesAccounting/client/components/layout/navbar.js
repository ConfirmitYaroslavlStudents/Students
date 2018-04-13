import React from 'react'
import Appbar from '../common/appbar'
import Navbar from '../common/UIComponentDecorators/navbar'
import TablesBar from './tablesbar'

export default function CustomNavbar(props) {
  return (
    <Navbar>
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
    </Navbar>
  )
}