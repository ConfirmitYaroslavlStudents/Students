import React, {Component} from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import Table from '../common/UIComponentDecorators/table';
import TableSortLabel from '../common/UIComponentDecorators/tableSortLabel';
import IconButton from '../common/UIComponentDecorators/iconButton';
import FirstPageIcon from 'material-ui-icons/FirstPage';
import KeyboardArrowLeft from 'material-ui-icons/KeyboardArrowLeft';
import KeyboardArrowRight from 'material-ui-icons/KeyboardArrowRight';
import LastPageIcon from 'material-ui-icons/LastPage';
import SelectInput from '../common/UIComponentDecorators/selectInput';

export default class SortableTableWithPagination extends Component {
  constructor(props) {
    super(props);
    this.sortLabelHandleClick = this.sortLabelHandleClick.bind(this);
    this.handleFirstPageButtonClick = this.handleFirstPageButtonClick.bind(this);
    this.handleBackButtonClick = this.handleBackButtonClick.bind(this);
    this.handleNextButtonClick = this.handleNextButtonClick.bind(this);
    this.handleLastPageButtonClick = this.handleLastPageButtonClick.bind(this);
    this.handleChangePage = this.handleChangePage.bind(this);
    this.handleChangeRowsPerPage = this.handleChangeRowsPerPage.bind(this);
  }

  sortLabelHandleClick(sortingField) {
    this.props.loadCandidates(
      {
        applicationStatus: 'refreshing',
        sortingField: sortingField,
        sortingDirection : sortingField !== this.props.sortingField ?
          'desc'
          :
          this.props.sortingDirection === 'desc' ?
            'asc' : 'desc'
      },
      this.props.history);
  }

  handleFirstPageButtonClick() {
    this.handleChangePage(0);
  };

  handleBackButtonClick() {
    this.handleChangePage(Math.max(this.props.offset - this.props.rowsPerPage, 0));
  };

  handleNextButtonClick() {
    this.handleChangePage(Math.min(this.props.offset + this.props.rowsPerPage, this.props.totalCount));
  };

  handleLastPageButtonClick() {
    this.handleChangePage(
      this.props.totalCount % this.props.rowsPerPage === 0 ?
        this.props.totalCount - this.props.rowsPerPage
        :
        this.props.totalCount - this.props.totalCount % this.props.rowsPerPage
    );
  };

  handleChangePage(offset) {
    this.props.loadCandidates(
      { offset: offset },
      this.props.history
    );
  };

  handleChangeRowsPerPage(rowsPerPage) {
    if (rowsPerPage !== this.props.rowsPerPage) {
      this.props.loadCandidates(
        { candidatesPerPage: rowsPerPage },
        this.props.history
      );
    }
  };

  render() {
    return (
      <Table
        heads={this.props.heads.map((head, index) =>
          head.sortingField ?
            <TableSortLabel
              key={index}
              active={head.sortingField === this.props.sortingField}
              direction={this.props.sortingDirection}
              onClick={() => {this.sortLabelHandleClick(head.sortingField)}}
            >{head.title}</TableSortLabel>
            :
            <span key={index}>{head.title}</span>)}
        rows={this.props.contentRows}
        footerActions={
          <ActionsWrapper>
            <ItemsPerPageWrapper>
              <FooterText>Candidates per page: </FooterText>
              <SelectInput
                options={[10, 15, 20, 25]}
                selected={this.props.rowsPerPage}
                onChange={this.handleChangeRowsPerPage}
              />
            </ItemsPerPageWrapper>
            <span>
              {Math.min(this.props.offset + 1, this.props.totalCount)}
              -
              {Math.min(this.props.offset + this.props.rowsPerPage, this.props.totalCount)} of {this.props.totalCount}
            </span>
            <IconButton
              onClick={this.handleFirstPageButtonClick}
              disabled={this.props.offset === 0}
              icon={<FirstPageIcon />}
            />
            <IconButton
              onClick={this.handleBackButtonClick}
              disabled={this.props.offset === 0}
              icon={<KeyboardArrowLeft />}
            />
            <IconButton
              onClick={this.handleNextButtonClick}
              disabled={this.props.offset + this.props.rowsPerPage >= this.props.totalCount}
              icon={<KeyboardArrowRight />}
            />
            <IconButton
              onClick={this.handleLastPageButtonClick}
              disabled={this.props.offset + this.props.rowsPerPage >= this.props.totalCount}
              icon={<LastPageIcon />}
            />
          </ActionsWrapper>
        }
      />
    );
  }
}

SortableTableWithPagination.propTypes = {
  heads: PropTypes.arrayOf(PropTypes.shape({
    title: PropTypes.string,
    sortingField: PropTypes.string
  })).isRequired,
  contentRows: PropTypes.oneOfType([PropTypes.array, PropTypes.object]).isRequired,
  offset: PropTypes.number.isRequired,
  rowsPerPage: PropTypes.number.isRequired,
  totalCount: PropTypes.number.isRequired,
  sortingField: PropTypes.string.isRequired,
  sortingDirection: PropTypes.string.isRequired,
  history: PropTypes.object.isRequired,
  loadCandidates: PropTypes.func.isRequired,
};

const ActionsWrapper = styled.div`
  display: flex;
  align-items: center;
  float: right;
  margin-right: -18px;
`;

const ItemsPerPageWrapper = styled.div`
  display: inline-flex;
  align-items: center;
  spacing: 5;
  margin-right: 24px;
`;

const FooterText = styled.span`
  margin-right: 8px;
`;