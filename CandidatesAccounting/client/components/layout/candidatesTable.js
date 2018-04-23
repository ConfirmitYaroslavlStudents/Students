import React from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../../actions/actions'
import SortablePaginatedTable from '../common/sortablePaginatedTable'
import getCandidateTableHeaders from '../candidates/candidateTableHeaders'
import getIntervieweeTableHeaders from '../interviewees/intervieweeTableHeaders'
import getStudentTableHeaders from '../students/studentTableHeaders'
import getTraineeTableHeaders from '../trainees/traineeTableHeaders'
import CandidateControls from '../candidates/candidateControls'
import { formatDateTime, formatDate, isToday, isBirthDate } from '../../utilities/customMoment'
import TagList from '../tags/tagList'
import ResumeControls from '../interviewees/resumeControls'
import styled, { css } from 'styled-components'

function CandidatesTable(props) {
  const {
    candidates,
    type,
    setSearchRequest,
    search,
    history,
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
    setSortingDirection
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
        <TagList
          tags={candidate.tags}
          setSearchRequest={setSearchRequest}
          search={search}
          history={history}
        />
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
        <CandidateControls candidate={candidate} history={history}/>
      </CandidateControlsWrapper>)

    return row
  }

  const handleOffsetCange = (offset) => {
    setOffset({offset, history})
  }

  const handleCandidatesPerPageChange = (candidatesPerPage) => {
    setCandidatesPerPage({candidatesPerPage, history})
  }

  const handleSortingFieldChange = (sortingField) => {
    setSortingField({sortingField, history})
  }

  const handleSortingDirectionChange = () => {
    setSortingDirection({history})
  }

  const headers = getHeaders()

  const contentRows = candidates.map(candidate => {
    return createRow(candidate, fetching || candidate.id === onUpdating || candidate.id === onDeleting)
  })

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
      onSortingDirectionChange={handleSortingDirectionChange}
    />
  )
}

CandidatesTable.propTypes = {
  type: PropTypes.string.isRequired,
  history: PropTypes.object.isRequired,
}

export default connect(state => {
  return {
    candidates: Object.keys(state.candidates).map(candidateID => { return state.candidates[candidateID] }),
    searchRequest: state.searchRequest,
    fetching: state.fetching,
    onUpdating: state.onUpdating,
    onDeleting: state.onDeleting,
    offset: state.offset,
    candidatesPerPage: state.candidatesPerPage,
    totalCount: state.totalCount,
    sortingField: state.sortingField,
    sortingDirection: state.sortingDirection,
  }
}, actions)(CandidatesTable)

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