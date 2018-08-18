import React, { Component } from 'react'
import PropTypes from 'prop-types'
import IconButton from './decorators/iconButton'
import FirstPageIcon from '@material-ui/icons/FirstPage'
import KeyboardArrowLeft from '@material-ui/icons/KeyboardArrowLeft'
import KeyboardArrowRight from '@material-ui/icons/KeyboardArrowRight'
import LastPageIcon from '@material-ui/icons/LastPage'
import SelectInput from './decorators/selectInput'
import RowAmountDisplay from './rowAmountDisplay'
import styled from 'styled-components'

class SortablePaginatedTableFooter extends Component {
  render() {
    const {
      offset,
      rowsPerPage,
      totalCount,
      rowsPerPageOptions,
      onRowsPerPageChange,
      onFirstPageButtonClick,
      onBackButtonClick,
      onNextButtonClick,
      onLastPageButtonClick
    } = this.props

    return (
      <ActionsWrapper>
        <RowsPerPageWrapper>
          <FooterText>Candidates per page: </FooterText>
          <SelectInput
            label=''
            options={rowsPerPageOptions}
            selected={rowsPerPage}
            onChange={onRowsPerPageChange}
          />
        </RowsPerPageWrapper>
        <RowAmountDisplay
          from={Math.min(offset + 1, totalCount)}
          to={Math.min(offset + rowsPerPage, totalCount)}
          total={totalCount}
        />
        <IconButton
          id='to-first-page-button'
          icon={<FirstPageIcon/>}
          onClick={onFirstPageButtonClick}
          disabled={offset === 0}
        />
        <IconButton
          id='to-previous-page-button'
          icon={<KeyboardArrowLeft/>}
          onClick={onBackButtonClick}
          disabled={offset === 0}
        />
        <IconButton
          id='to-next-page-button'
          icon={<KeyboardArrowRight/>}
          onClick={onNextButtonClick}
          disabled={offset + rowsPerPage >= totalCount}
        />
        <IconButton
          id='to-last-page-button'
          icon={<LastPageIcon/>}
          onClick={onLastPageButtonClick}
          disabled={offset + rowsPerPage >= totalCount}
        />
      </ActionsWrapper>
    )
  }
}

SortablePaginatedTableFooter.propTypes = {
  rowsPerPage: PropTypes.number.isRequired,
  offset: PropTypes.number.isRequired,
  totalCount: PropTypes.number.isRequired,
  rowsPerPageOptions: PropTypes.array.isRequired,
  onRowsPerPageChange: PropTypes.func.isRequired,
  onFirstPageButtonClick: PropTypes.func.isRequired,
  onBackButtonClick: PropTypes.func.isRequired,
  onNextButtonClick: PropTypes.func.isRequired,
  onLastPageButtonClick: PropTypes.func.isRequired
}

export default SortablePaginatedTableFooter

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