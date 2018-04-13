import React from 'react'
import PropTypes from 'prop-types'
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


export default function CandidatesTable(props) {
  const getHeaders = () => {
    switch (props.type) {
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
        <span style={{whiteSpace: 'nowrap'}}>{candidate.name}</span>
        <TagList
          tags={candidate.tags}
          setSearchRequest={props.setSearchRequest}
          loadCandidates={props.loadCandidates}
          changeURL={props.changeURL}
          history={props.history}
        />
      </CandidateNameWrapper>
    )

    if (props.type === 'Candidate') {
      row.cells.push(candidate.status)
    }

    row.cells.push(candidate.email)
    row.cells.push(
      <Date highlighted={isBirthDate(candidate.birthDate)}>
        {formatDate(candidate.birthDate)}
      </Date>)

    switch (props.type) {
      case 'Interviewee':
        const candidateOnUploading = props.applicationStatus.slice(0, 9) === 'uploading' ? props.applicationStatus.slice(10) : ''
        row.cells.push(
          <Date highlighted={isToday(candidate.interviewDate)}>
            {formatDateTime(candidate.interviewDate)}
          </Date>)
        row.cells.push(<ResumeControls
          interviewee={candidate}
          onUploading={candidate.id === candidateOnUploading}
          enableDownload
          enableUpload
          authorized={props.username !== ''}
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

  const onRefreshing = props.applicationStatus === 'refreshing';
  const candidateOnUpdating = props.applicationStatus.slice(0, 8) === 'updating' ? props.applicationStatus.slice(9) : ''
  const candidateOnDeleting = props.applicationStatus.slice(0, 8) === 'deleting' ? props.applicationStatus.slice(9) : ''

  const candidates = Object.keys(props.candidates).map(candidateID => { return props.candidates[candidateID] })

  return (
    <SortablePaginatedTable
      headers={getHeaders()}
      contentRows={ candidates.map(candidate => {
          return createRow(candidate, onRefreshing || candidate.id === candidateOnUpdating || candidate.id === candidateOnDeleting)
      })}
      history={props.history}
      offset={props.offset}
      rowsPerPage={props.candidatesPerPage}
      totalCount={props.totalCount}
      setOffset={props.setOffset}
      sortingField={props.sortingField}
      setSortingField={props.setSortingField}
      sortingDirection={props.sortingDirection}
      setSortingDirection={props.setSortingDirection}
      loadCandidates={props.loadCandidates}
      changeURL={props.changeURL}
    />
  )
}

CandidatesTable.propTypes = {
  type: PropTypes.string.isRequired
}