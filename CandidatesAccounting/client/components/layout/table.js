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
    this.state = {
      orderBy: -1,
      direction: 'desc',
      sorting: '',
    };
    this.sortLabelHandleClick = this.sortLabelHandleClick.bind(this);
    this.sortContentRows = this.sortContentRows.bind(this);
    this.sortByAlphabet = this.sortByAlphabet.bind(this);
    this.sortByTime = this.sortByTime.bind(this);
    this.sortByDate = this.sortByDate.bind(this);
    this.handleFirstPageButtonClick = this.handleFirstPageButtonClick.bind(this);
    this.handleBackButtonClick = this.handleBackButtonClick.bind(this);
    this.handleNextButtonClick = this.handleNextButtonClick.bind(this);
    this.handleLastPageButtonClick = this.handleLastPageButtonClick.bind(this);
    this.handleChangePage = this.handleChangePage.bind(this);
    this.handleChangeRowsPerPage = this.handleChangeRowsPerPage.bind(this);
  }

  sortLabelHandleClick(index, sorting) {
    if (index !== this.state.orderBy) {
      this.setState({orderBy: index, direction: 'desc', sorting: sorting})
    } else {
      if (this.state.direction === 'desc') {
        this.setState({direction: 'asc'})
      } else {
        this.setState({direction: 'desc'})
      }
    }
  }

  sortContentRows() {
    if (this.state.orderBy === -1) {
      return this.props.contentRows;
    } else {
      let sortedRows = [];
      this.props.contentRows.forEach((row) => {
        sortedRows.push(row);
      });
      switch(this.state.sorting) {
        case 'byAlphabet':
          sortedRows.sort(this.sortByAlphabet);
          break;
        case 'byTime':
          sortedRows.sort(this.sortByTime);
          break;
        case 'byDate':
          sortedRows.sort(this.sortByDate);
          break;
      }

      if (this.state.direction === 'asc') {
        sortedRows.reverse();
      }
      return sortedRows;
    }
  }

  sortByAlphabet(first, second) {
    if(first[this.state.orderBy].value > second[this.state.orderBy].value) {
      return 1;
    } else {
      return -1;
   }
  }

  sortByTime(first, second) {
    const sortByDateResult = this.sortByDate(first, second);
    if (sortByDateResult !== 0) {
      return sortByDateResult;
    } else {
      let firstTime = first[this.state.orderBy].value.split(' ')[0].split('.');
      let secondTime = second[this.state.orderBy].value.split(' ')[0].split('.');
      if (firstTime[0] > secondTime[0]) {
        return 1;
      } else {
        if (firstTime[0] < secondTime[0]) {
          return -1;
        } else {
          if (firstTime[1] > secondTime[1]) {
            return 1;
          } else {
            return -1;
          }
        }
      }
    }
  }

  sortByDate(first, second) {
    let firstDate = first[this.state.orderBy].value.split('.');
    let secondDate = second[this.state.orderBy].value.split('.');
    if (parseInt(firstDate[2]) > parseInt(secondDate[2])) {
      return 1;
    } else {
      if (parseInt(firstDate[2]) < parseInt(secondDate[2])) {
        return -1;
      } else {
        if (parseInt(firstDate[1]) > parseInt(secondDate[1])) {
          return 1;
        } else {
          if (parseInt(firstDate[1]) < parseInt(secondDate[1])) {
            return -1;
          } else {
            if (parseInt(firstDate[0]) > parseInt(secondDate[0])) {
              return 1;
            } else {
              if (parseInt(firstDate[0]) === parseInt(secondDate[0])) {
                return 0;
              } else {
                return -1;
              }
            }
          }
        }
      }
    }
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
    this.props.setOffset(offset);
    this.props.loadCandidates(this.props.history);
  };

  handleChangeRowsPerPage(rowsPerPage) {
    this.props.setRowsPerPage(rowsPerPage);
    this.props.loadCandidates(this.props.history);
  };

  render() {
    return (
      <Table
        heads={this.props.heads.map((head, index) =>
          head.sorting ?
            <TableSortLabel
              active={index === this.state.orderBy}
              direction={this.state.direction}
              onClick={() => {this.sortLabelHandleClick(index, head.sorting)}}
            >{head.title}</TableSortLabel>
            :
            <span>{head.title}</span>)}
        rows={this.sortContentRows()}
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
    sorting: PropTypes.string
  })).isRequired,
  contentRows: PropTypes.oneOfType([PropTypes.array, PropTypes.object]).isRequired,
  offset: PropTypes.number.isRequired,
  rowsPerPage: PropTypes.number.isRequired,
  totalCount: PropTypes.number.isRequired,
  setOffset: PropTypes.func.isRequired,
  setRowsPerPage: PropTypes.func.isRequired,
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