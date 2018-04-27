import React from 'react'
import { connect } from 'react-redux'
import * as candidateActions from '../../actions/candidateActions'
import NavLink from '../common/navLink'
import AddCandidateDialog from '../candidates/addCandidateDialog'
import styled from 'styled-components'

function TablesBar(props) {
  const { candidateStatus, setCandidateStatus } = props

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
    setCandidateStatus({ newCandidateStatus })
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
        <AddCandidateDialog />
      </AddCandidateButtonWrapper>
    </TablesBarWrapper>
  )
}

export default connect(state => {
  return {
    candidateStatus: state.candidates.candidateStatus
  }
}, candidateActions)(TablesBar)

const TablesBarWrapper = styled.div`
  display: flex;
  align-items: center;
  z-index: 110;
  color: rgba(0, 0, 0, 0.87);
  background-color: #f5f5f5;
  width: 100%;
  box-shadow: 0px 2px 4px -1px rgba(0, 0, 0, 0.2), 0px 4px 5px 0px rgba(0, 0, 0, 0.14), 0px 1px 10px 0px rgba(0, 0, 0, 0.12);
  height: 48px;
`

const TabsWrapper = styled.div`
  display: inline-flex;
  position: absolute;
  left: 15%;
  right: 15%;
  height: 48px;
  margin: 0 auto;
`

const Tabs = styled.div`
  display: inline-flex;
  margin: 0 auto;
`

const Tab = styled.div`
  display: inline-flex;
  height: 48px;
  width: 200px;
`

const AddCandidateButtonWrapper = styled.div`
  display: inline-flex;
  position: absolute;
  right: 6px;
`