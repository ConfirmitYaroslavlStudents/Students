import React, { Component } from 'react'
import PropTypes from 'prop-types'
import Table from './UIComponentDecorators/table'
import TableSortLabel from './UIComponentDecorators/tableSortLabel'
import SortablePaginatedTableFooter from './sortablePaginatedTableFooter'

class SortablePaginatedTable extends Component {
  handlePageChange = (offset) => {
    this.props.onOffsetChange(offset)
  }

  handleRowsPerPageChange = (newRowsPerPageValue) => {
    if (newRowsPerPageValue !== this.props.rowsPerPage) {
      this.props.onRowsPerPageChange(newRowsPerPageValue)
    }
  }

  handleSortLabelClick = (chosenSortingField) => {
    if (chosenSortingField !== this.props.sortingField) {
      this.props.onSortingFieldChange(chosenSortingField)
    } else {
      this.props.onSortingDirectionChange()
    }
  }

  handleFirstPageButtonClick = () => {
    this.handlePageChange(0)
  }

  handleBackButtonClick = () => {
    this.handlePageChange(Math.max(offset - this.props.rowsPerPage, 0))
  }

  handleNextButtonClick = () => {
    this.handlePageChange(Math.min(offset + this.props.rowsPerPage, this.props.totalCount))
  }

  handleLastPageButtonClick = () => {
    this.handlePageChange(
      this.props.totalCount % this.props.rowsPerPage === 0 ?
        this.props.totalCount - this.props.rowsPerPage
        :
        this.props.totalCount - this.props.totalCount % this.props.rowsPerPage
    )
  }

  render() {
    const { headers, contentRows, offset, rowsPerPage, totalCount, sortingField, sortingDirection } = this.props

    const headerLabels = headers.map((header, index) =>
      header.sortingField ?
        <TableSortLabel
          key={index}
          active={header.sortingField === sortingField}
          direction={sortingDirection}
          onClick={() => {
            this.handleSortLabelClick(header.sortingField)
          }}
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
            onRowsPerPageChange={this.handleRowsPerPageChange}
            onFirstPageButtonClick={this.handleFirstPageButtonClick}
            onBackButtonClick={this.handleBackButtonClick}
            onNextButtonClick={this.handleNextButtonClick}
            onLastPageButtonClick={this.handleLastPageButtonClick}
          />
        }
      />
    )
  }
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
  onSortingDirectionChange: PropTypes.func.isRequired
}

export default SortablePaginatedTable