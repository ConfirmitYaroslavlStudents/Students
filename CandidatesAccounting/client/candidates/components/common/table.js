import React from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../../actions'
import { SELECTORS } from '../../../rootReducer'
import SortablePaginatedTable from '../../../commonComponents/sortablePaginatedTable'
import CandidateTableHeaders from './tableHeaders'
import CandidateTableRow from './tableRow'
import IntervieweeTableHeaders from '../interviewees/tableHeaders'
import IntervieweeTableRow from '../interviewees/tableRow'
import StudentTableHeaders from '../students/tableHeaders'
import StudentTableRow from '../students/tableRow'
import TraineeTableHeaders from '../trainees/tableHeaders'
import TraineeTableRow from '../trainees/tableRow'

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
        return CandidateTableHeaders()
      case 'Interviewee':
        return IntervieweeTableHeaders()
      case 'Student':
        return StudentTableHeaders()
      case 'Trainee':
        return TraineeTableHeaders()
    }
  }

  const getRow = (candidate, disabled) => {
    let cells = []
    switch (type)
    {
      case 'Interviewee':
        cells = IntervieweeTableRow({ candidate, disabled})
        break
      case 'Student':
        cells = StudentTableRow({ candidate })
        break
      case 'Trainee':
        cells = TraineeTableRow({ candidate })
        break
      default:
        cells = CandidateTableRow({ candidate })
    }
    return { cells, isDisabled: disabled}
  }

  const handleOffsetCange = offset => {
    setOffset({ offset })
  }

  const handleCandidatesPerPageChange = candidatesPerPage => {
    setCandidatesPerPage({ candidatesPerPage })
  }

  const handleSortingFieldChange = sortingField => {
    setSortingField({ sortingField })
  }

  const candidateHeaders = getHeaders()

  const candidatesArray =  Object.keys(candidates).map(candidateID => candidates[candidateID])

  const candidateRows = candidatesArray.map(candidate =>
    getRow(candidate, fetching || candidate.id === onUpdating || candidate.id === onDeleting))

  return (
    <SortablePaginatedTable
      headers={candidateHeaders}
      contentRows={candidateRows}
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
  sortingDirection: PropTypes.string.isRequired,
  setOffset: PropTypes.func.isRequired,
  setCandidatesPerPage: PropTypes.func.isRequired,
  setSortingField: PropTypes.func.isRequired,
  toggleSortingDirection: PropTypes.func.isRequired
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