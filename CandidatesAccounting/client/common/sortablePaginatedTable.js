import React from 'react'
import PropTypes from 'prop-types'
import Table from './UIComponentDecorators/table'
import TableSortLabel from './UIComponentDecorators/tableSortLabel'
import IconButton from './UIComponentDecorators/iconButton'
import FirstPageIcon from '@material-ui/icons/FirstPage'
import KeyboardArrowLeft from '@material-ui/icons/KeyboardArrowLeft'
import KeyboardArrowRight from '@material-ui/icons/KeyboardArrowRight'
import LastPageIcon from '@material-ui/icons/LastPage'
import SelectInput from './UIComponentDecorators/selectInput'
import styled from 'styled-components'

export default function SortablePaginatedTable(props) {
  const handlePageChange = (offset) => {
    props.onOffsetChange(offset)
  }

  const handleRowsPerPageChange = (rowsPerPage) => {
    if (rowsPerPage !== props.rowsPerPage) {
      props.onRowsPerPageChange(rowsPerPage)
    }
  }

  const handleSortLabelClick = (sortingField) => {
    if (sortingField !== props.sortingField) {
      props.onSortingFieldChange(sortingField)
    } else {
      props.onSortingDirectionChange()
    }
  }

  const handleFirstPageButtonClick = () => {
    handlePageChange(0)
  }

  const handleBackButtonClick = () => {
    handlePageChange(Math.max(props.offset - props.rowsPerPage, 0))
  }

  const handleNextButtonClick = () => {
    handlePageChange(Math.min(props.offset + props.rowsPerPage, props.totalCount))
  }

  const handleLastPageButtonClick = () => {
    handlePageChange(
      props.totalCount % props.rowsPerPage === 0 ?
        props.totalCount - props.rowsPerPage
        :
        props.totalCount - props.totalCount % props.rowsPerPage
    )
  }

  const headers = props.headers.map((header, index) =>
    header.sortingField ?
      <TableSortLabel
        key={index}
        active={header.sortingField === props.sortingField}
        direction={props.sortingDirection}
        onClick={() => {handleSortLabelClick(header.sortingField)}}
      >{header.title}</TableSortLabel>
      :
      <span key={index}>{header.title}</span>)

  const rowsPerPageOptions = [10, 15, 20, 25]

  return (
    <Table
      headers={headers}
      rows={props.contentRows}
      footerActions={
        <ActionsWrapper>
          <RowsPerPageWrapper>
            <FooterText>Candidates per page: </FooterText>
            <SelectInput
              options={rowsPerPageOptions}
              selected={props.rowsPerPage}
              onChange={handleRowsPerPageChange}
            />
          </RowsPerPageWrapper>
          <span>
            {Math.min(props.offset + 1, props.totalCount)}
            -
            {Math.min(props.offset + props.rowsPerPage, props.totalCount)} of {props.totalCount}
          </span>
          <IconButton
            onClick={handleFirstPageButtonClick}
            disabled={props.offset === 0}
            icon={<FirstPageIcon />}
          />
          <IconButton
            onClick={handleBackButtonClick}
            disabled={props.offset === 0}
            icon={<KeyboardArrowLeft />}
          />
          <IconButton
            onClick={handleNextButtonClick}
            disabled={props.offset + props.rowsPerPage >= props.totalCount}
            icon={<KeyboardArrowRight />}
          />
          <IconButton
            onClick={handleLastPageButtonClick}
            disabled={props.offset + props.rowsPerPage >= props.totalCount}
            icon={<LastPageIcon />}
          />
        </ActionsWrapper>
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

const ActionsWrapper = styled.div`
  display: flex;
  align-items: center;
  float: right;
  margin-right: -18px;
`

const RowsPerPageWrapper = styled.div`
  display: inline-flex;
  align-items: center;
  spacing: 5;
  margin-right: 24px;
`

const FooterText = styled.span`
  margin-right: 8px;
`