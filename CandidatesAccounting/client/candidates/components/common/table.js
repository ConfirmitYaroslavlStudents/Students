import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../../actions'
import { SELECTORS } from '../../../rootReducer'
import SortablePaginatedTable from '../../../commonComponents/sortablePaginatedTable'
import CandidateTableHeaders from './tableHeaders'
import getCandidateTableCells from './getCandidateTableRow'
import IntervieweeTableHeaders from '../interviewees/tableHeaders'
import getIntervieweeTableCells from '../interviewees/getTableCells'
import StudentTableHeaders from '../students/tableHeaders'
import getStudentTableCells from '../students/getTableCells'
import TraineeTableHeaders from '../trainees/tableHeaders'
import getTraineeTableCells from '../trainees/getTableCells'

class CandidatesTable extends Component {
  getHeaders = (type) => {
    switch (type) {
      case 'Candidate':
        return CandidateTableHeaders
      case 'Interviewee':
        return IntervieweeTableHeaders
      case 'Student':
        return StudentTableHeaders
      case 'Trainee':
        return TraineeTableHeaders
    }
  }

  getRow = (type, candidate, disabled) => {
    let cells = []
    switch (type) {
      case 'Interviewee':
        cells = getIntervieweeTableCells(candidate, disabled)
        break
      case 'Student':
        cells = getStudentTableCells(candidate)
        break
      case 'Trainee':
        cells = getTraineeTableCells(candidate)
        break
      default:
        cells = getCandidateTableCells(candidate)
    }
    return { cells, isDisabled: disabled }
  }

  handleOffsetCange = (offset) => {
    this.props.setOffset({ offset })
  }

  handleCandidatesPerPageChange = (candidatesPerPage) => {
    this.props.setCandidatesPerPage({ candidatesPerPage })
  }

  handleSortingFieldChange = (sortingField) => {
    this.props.setSortingField({ sortingField })
  }


  render() {
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
      toggleSortingDirection
    } = this.props

    const candidateHeaders = this.getHeaders(type)

    const candidatesArray = Object.keys(candidates).map(candidateID => candidates[candidateID])

    const candidateRows = candidatesArray.map(candidate =>
      this.getRow(type, candidate, fetching || candidate.id === onUpdating || candidate.id === onDeleting))

    return (
      <SortablePaginatedTable
        headers={candidateHeaders}
        contentRows={candidateRows}
        totalCount={totalCount}
        offset={offset}
        rowsPerPage={candidatesPerPage}
        sortingField={sortingField}
        sortingDirection={sortingDirection}
        onOffsetChange={this.handleOffsetCange}
        onRowsPerPageChange={this.handleCandidatesPerPageChange}
        onSortingFieldChange={this.handleSortingFieldChange}
        onSortingDirectionChange={toggleSortingDirection}
      />
    )
  }
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