import React from 'react'
import PropTypes from 'prop-types'
import Table from './UIComponentDecorators/table'
import TableSortLabel from './UIComponentDecorators/tableSortLabel'
import SortablePaginatedTableFooter from './sortablePaginatedTableFooter'

export default function SortablePaginatedTable(props) {
  const {
    headers,
    contentRows,
    offset,
    rowsPerPage,
    totalCount,
    sortingField,
    sortingDirection,
    onOffsetChange,
    onRowsPerPageChange,
    onSortingFieldChange,
    onSortingDirectionChange
  } = props

  const handlePageChange = (offset) => {
    onOffsetChange(offset)
  }

  const handleRowsPerPageChange = (newRowsPerPageValue) => {
    if (newRowsPerPageValue !== rowsPerPage) {
      onRowsPerPageChange(newRowsPerPageValue)
    }
  }

  const handleSortLabelClick = (chosenSortingField) => {
    if (chosenSortingField !== sortingField) {
      onSortingFieldChange(chosenSortingField)
    } else {
      onSortingDirectionChange()
    }
  }

  const handleFirstPageButtonClick = () => {
    handlePageChange(0)
  }

  const handleBackButtonClick = () => {
    handlePageChange(Math.max(offset - rowsPerPage, 0))
  }

  const handleNextButtonClick = () => {
    handlePageChange(Math.min(offset + rowsPerPage, totalCount))
  }

  const handleLastPageButtonClick = () => {
    handlePageChange(
      totalCount % rowsPerPage === 0 ?
        totalCount - rowsPerPage
        :
        totalCount - totalCount % rowsPerPage
    )
  }

  const headerLabels = headers.map((header, index) =>
    header.sortingField ?
      <TableSortLabel
        key={index}
        active={header.sortingField === sortingField}
        direction={sortingDirection}
        onClick={() => {handleSortLabelClick(header.sortingField)}}
      >
        {header.title}
      </TableSortLabel>
      :
      <span key={index}>{header.title}</span>)

  const rowsPerPageOptions = [10, 15, 20, 25]

  return (
    <Table
      headers={headerLabels}
      rows={contentRows}
      footer={
        <SortablePaginatedTableFooter
          rowsPerPage={rowsPerPage}
          offset={offset}
          totalCount={totalCount}
          rowsPerPageOptions={rowsPerPageOptions}
          onRowsPerPageChange={handleRowsPerPageChange}
          onFirstPageButtonClick={handleFirstPageButtonClick}
          onBackButtonClick={handleBackButtonClick}
          onNextButtonClick={handleNextButtonClick}
          onLastPageButtonClick={handleLastPageButtonClick}
        />
      }
    />
  )
}

SortablePaginatedTable.propTypes = {
  headers: PropTypes.arrayOf(PropTypes.shape({
    title: PropTypes.string,
    sortingField: PropTypes.string
  })).isRequired,
  contentRows: PropTypes.array.isRequired,
  offset: PropTypes.number.isRequired,
  rowsPerPage: PropTypes.number.isRequired,
  totalCount: PropTypes.number.isRequired,
  sortingField: PropTypes.string.isRequired,
  sortingDirection: PropTypes.string.isRequired,
  onOffsetChange: PropTypes.func.isRequired,
  onRowsPerPageChange: PropTypes.func.isRequired,
  onSortingFieldChange: PropTypes.func.isRequired,
  onSortingDirectionChange: PropTypes.func.isRequired,
}