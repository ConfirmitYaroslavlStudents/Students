import React from 'react'
import PropTypes from 'prop-types'
import Appbar from './appbar'
import Navbar from '../common/UIComponentDecorators/navbar'
import TablesBar from './tablesbar'

export default function CustomNavbar(props) {
  return (
    <Navbar>
      <Appbar
        history={props.history}
      />
      <TablesBar
        newCandidateDefaultType={props.candidateStatus}
        addCandidate={props.addCandidate}
        tags={props.tags}
        username={props.username}
        history={props.history}
        candidatesPerPage={props.candidatesPerPage}
        totalCount={props.totalCount}
        loadCandidates={props.refreshRows}
        candidateStatus={props.candidateStatus}
      />
    </Navbar>
  )
}

CustomNavbar.propTypes = {
  history: PropTypes.object.isRequired
}