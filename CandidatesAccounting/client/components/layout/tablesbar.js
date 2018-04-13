import React from 'react'
import PropTypes from 'prop-types'
import NavLink from '../common/navLink'
import AddCandidateDialog from '../candidates/addCandidateDialog'
import {
  TablesBarWrapper,
  TabsWrapper,
  Tabs,
  Tab,
  AddCandidateButtonWrapper
} from '../common/styledComponents'

export default function TablesBar(props) {
  let selected = 0;
  switch(props.candidateStatus) {
    case 'Interviewee':
      selected = 1
      break
    case 'Student':
      selected = 2
      break
    case 'Trainee':
      selected = 3
      break
  }

  const handleLinkClick = candidateStatus => () => {
    const stateChanges = {}
    if (candidateStatus === props.candidateStatus) {
      stateChanges.searchRequest = ''
    } else {
      stateChanges['candidateStatus'] = candidateStatus
    }
    stateChanges.offset = 0
    stateChanges.sortingField = ''
    stateChanges.sortingDirection = 'desc'
    stateChanges.pageTitle = 'Candidate Accounting'
    props.loadCandidates(stateChanges, props.history)
  }

  const addCandidate = candidate => {
    props.addCandidate(candidate);
    props.loadCandidates(
      {
        applicationStatus: 'refreshing',
        offset: props.totalCount - props.totalCount % props.candidatesPerPage
      },
      props.history
    )
  }

  return (
    <TablesBarWrapper>
      <TabsWrapper>
        <Tabs>
          <Tab>
            <NavLink className='table-link' active={selected === 0} onClick={handleLinkClick('')}>All</NavLink>
          </Tab>
          <Tab>
            <NavLink className='table-link' active={selected === 1} onClick={handleLinkClick('Interviewee')}>Interviewees</NavLink>
          </Tab>
          <Tab>
            <NavLink className='table-link' active={selected === 2} onClick={handleLinkClick('Student')}>Students</NavLink>
          </Tab>
          <Tab>
            <NavLink className='table-link' active={selected === 3} onClick={handleLinkClick('Trainee')}>Trainees</NavLink>
          </Tab>
        </Tabs>
      </TabsWrapper>
      <AddCandidateButtonWrapper>
        <AddCandidateDialog
          addCandidate={addCandidate}
          candidateStatus={props.newCandidateDefaultType === '' ? 'Interviewee' : props.newCandidateDefaultType}
          tags={props.tags}
          disabled={props.username === ''}
        />
      </AddCandidateButtonWrapper>
    </TablesBarWrapper>
  )
}

TablesBar.propTypes = {
  username: PropTypes.string.isRequired,
  tags: PropTypes.array.isRequired,
  addCandidate: PropTypes.func.isRequired,
  newCandidateDefaultType: PropTypes.string.isRequired,
  history: PropTypes.object.isRequired,
  loadCandidates: PropTypes.func.isRequired,
  candidateStatus: PropTypes.string.isRequired,
  candidatesPerPage: PropTypes.number.isRequired,
  totalCount: PropTypes.number.isRequired
}