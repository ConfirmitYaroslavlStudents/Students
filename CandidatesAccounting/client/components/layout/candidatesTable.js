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
import { CandidateNameWrapper, CandidateControlsWrapper } from '../common/styledComponents'
import { Date } from '../common/styledComponents'

function CandidatesTable(props) {
  const {
    candidates,
    authorized,
    type,
    setSearchRequest,
    search,
    history,
    fetching,
    inactiveCandidateId,
    offset,
    candidatesPerPage,
    totalCount,
    sortingField,
    sortingDirection,
    changeTableOptions
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
          enableDownload
          enableUpload
          authorized={authorized}
          uploadResume={props.uploadResume}/>)
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
        <CandidateControls candidate={candidate} {...props}/>
      </CandidateControlsWrapper>)

    return row
  }

  return (
    <SortablePaginatedTable
      headers={getHeaders()}
      contentRows={ candidates.map(candidate => {
        return createRow(candidate, fetching || inactiveCandidateId === candidate.id)
      })}
      offset={offset}
      rowsPerPage={candidatesPerPage}
      totalCount={totalCount}
      sortingField={sortingField}
      sortingDirection={sortingDirection}
      changeTableOptions={changeTableOptions}
      history={history}
    />
  )
}

CandidatesTable.propTypes = {
  type: PropTypes.string.isRequired,
  history: PropTypes.object.isRequired,
}

export default connect(state => {
  return {
    authorized: state.authorized,
    candidates: Object.keys(state.candidates).map(candidateID => { return state.candidates[candidateID] }),
    searchRequest: state.searchRequest,
    fetching: state.fetching,
    inactiveCandidateId: state.inactiveCandidateId,
    offset: state.offset,
    candidatesPerPage: state.candidatesPerPage,
    totalCount: state.totalCount,
    sortingField: state.sortingField,
    sortingDirection: state.sortingDirection,
  }
}, actions)(CandidatesTable)