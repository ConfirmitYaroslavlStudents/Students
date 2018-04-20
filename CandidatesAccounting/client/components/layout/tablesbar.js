import React from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../../actions/actions'
import NavLink from '../common/navLink'
import AddCandidateDialog from '../candidates/addCandidateDialog'
import {
  TablesBarWrapper,
  TabsWrapper,
  Tabs,
  Tab,
  AddCandidateButtonWrapper
} from '../common/styledComponents'

function TablesBar(props) {
  const { candidateStatus, changeTable, history } = props

  let selected = 0;
  switch(candidateStatus) {
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

  const handleLinkClick = newCandidateStatus => () => {
    changeTable({newCandidateStatus, history})
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
        <AddCandidateDialog history={props.history} />
      </AddCandidateButtonWrapper>
    </TablesBarWrapper>
  )
}

TablesBar.propTypes = {
  history: PropTypes.object.isRequired,
}

export default connect(state => {
  return {
    candidateStatus: state.candidateStatus
  }
}, actions)(TablesBar)