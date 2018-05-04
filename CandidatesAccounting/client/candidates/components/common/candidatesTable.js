import React from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../../actions'
import { SELECTORS } from '../../../rootReducer'
import SortablePaginatedTable from '../../../common/sortablePaginatedTable'
import getCandidateTableHeaders from './candidateTableHeaders'
import getIntervieweeTableHeaders from '../interviewees/intervieweeTableHeaders'
import getStudentTableHeaders from '../students/studentTableHeaders'
import getTraineeTableHeaders from '../trainees/traineeTableHeaders'
import CandidateControls from './candidateControls'
import { formatDateTime, formatDate, isToday, isBirthDate } from '../../../utilities/customMoment'
import TagList from '../../../tags/components/tagList'
import ResumeControls from '../interviewees/resumeControls'
import styled, { css } from 'styled-components'

function CandidatesTable(props) {
  const {
    candidates,
    type,
    fetching,
    onUpdating,
    onDeleting,
    offset,
    candidatesPerPage,
    totalCount,
    sortingField,
    sortingDirection,
    setOffset,
    setCandidatesPerPage,
    setSortingField,
    toggleSortingDirection
  } = props

  const getHeaders = () => {
    switch (type) {
      case 'Candidate':
        return getCandidateTableHeaders()
      case 'Interviewee':
        return getIntervieweeTableHeaders()
      case 'Student':
        return getStudentTableHeaders()
      case 'Trainee':
        return getTraineeTableHeaders()
    }
  }

  const createRow = (candidate, disabled) => {
    const row = { cells: [], isDisabled: disabled }

    row.cells.push(
      <CandidateNameWrapper>
        <span className='nowrap'>{candidate.name}</span>
        <TagList candidateTags={candidate.tags} />
      </CandidateNameWrapper>
    )

    if (type === 'Candidate') {
      row.cells.push(candidate.status)
    }

    row.cells.push(candidate.email)
    row.cells.push(
      <Date highlighted={isBirthDate(candidate.birthDate)}>
        {formatDate(candidate.birthDate)}
      </Date>)

    switch (type) {
      case 'Interviewee':
        row.cells.push(
          <Date highlighted={isToday(candidate.interviewDate)}>
            {formatDateTime(candidate.interviewDate)}
          </Date>)
        row.cells.push(<ResumeControls
          interviewee={candidate}
          disabled={disabled}
          downloadingEnabled/>)
        break

      case 'Student':
        row.cells.push(candidate.groupName)
        row.cells.push(<Date>{formatDate(candidate.startingDate)}</Date>)
        row.cells.push(<Date>{formatDate(candidate.endingDate)}</Date>)
        break

      case 'Trainee':
        row.cells.push(<Date>{candidate.mentor}</Date>)
        break
    }

    row.cells.push(
      <CandidateControlsWrapper>
        <CandidateControls candidate={candidate}/>
      </CandidateControlsWrapper>)

    return row
  }

  const handleOffsetCange = offset => {
    setOffset({ offset })
  }

  const handleCandidatesPerPageChange = candidatesPerPage => {
    setCandidatesPerPage({candidatesPerPage})
  }

  const handleSortingFieldChange = sortingField => {
    setSortingField({sortingField})
  }

  const headers = getHeaders()

  const candidatesArray =  Object.keys(candidates).map(candidateID => candidates[candidateID])

  const contentRows = candidatesArray.map(candidate =>
    createRow(candidate, fetching || candidate.id === onUpdating || candidate.id === onDeleting))

  return (
    <SortablePaginatedTable
      headers={headers}
      contentRows={contentRows}
      totalCount={totalCount}
      offset={offset}
      rowsPerPage={candidatesPerPage}
      sortingField={sortingField}
      sortingDirection={sortingDirection}
      onOffsetChange={handleOffsetCange}
      onRowsPerPageChange={handleCandidatesPerPageChange}
      onSortingFieldChange={handleSortingFieldChange}
      onSortingDirectionChange={toggleSortingDirection}
    />
  )
}

CandidatesTable.propTypes = {
  type: PropTypes.string.isRequired,
  fetching: PropTypes.bool.isRequired,
  candidates: PropTypes.object.isRequired,
  onUpdating: PropTypes.string.isRequired,
  onDeleting: PropTypes.string.isRequired,
  offset: PropTypes.number.isRequired,
  candidatesPerPage: PropTypes.number.isRequired,
  totalCount: PropTypes.number.isRequired,
  sortingField: PropTypes.string.isRequired,
  sortingDirection: PropTypes.string.isRequired
}

export default connect(state => ({
    fetching: SELECTORS.APPLICATION.FETCHING(state),
    candidates: SELECTORS.CANDIDATES.CANDIDATES(state),
    onUpdating: SELECTORS.CANDIDATES.ONUPDATING(state),
    onDeleting: SELECTORS.CANDIDATES.ONDELETING(state),
    offset: SELECTORS.CANDIDATES.OFFSET(state),
    candidatesPerPage: SELECTORS.CANDIDATES.CANDIDATESPERPAGE(state),
    totalCount: SELECTORS.CANDIDATES.TOTALCOUNT(state),
    sortingField: SELECTORS.CANDIDATES.SORTINGFIELD(state),
    sortingDirection: SELECTORS.CANDIDATES.SORTINGDIRECTION(state)
  }
), {...actions})(CandidatesTable)

const CandidateNameWrapper = styled.div`
  display: flex;
  align-items: center;
`

const CandidateControlsWrapper = styled.div`
  display: flex;
  float: right;
`

const Date = styled.div`
  white-space: nowrap;
  
  ${props => props.highlighted && css`
    color: #ff4081;
    font-weight: bold;
	`}
`