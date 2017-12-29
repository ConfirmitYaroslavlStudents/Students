import React, {Component} from 'react';
import PropTypes from 'prop-types';
import Table from './UIComponentDecorators/table';
import TableSortLabel from './UIComponentDecorators/tableSortLabel';

export default class SortableTable extends Component {
  constructor(props) {
    super(props);
    this.state = {orderBy: -1, direction: 'desc', sorting: ''};
    this.sortLabelHandleClick = this.sortLabelHandleClick.bind(this);
    this.sortContentRows = this.sortContentRows.bind(this);
    this.sortByAlphabet = this.sortByAlphabet.bind(this);
    this.sortByTime = this.sortByTime.bind(this);
    this.sortByDate = this.sortByDate.bind(this);
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
      />
    );
  }
}

SortableTable.propTypes = {
  heads: PropTypes.arrayOf(PropTypes.shape({
    title: PropTypes.string,
    sorting: PropTypes.string
  })).isRequired,
  contentRows: PropTypes.oneOfType([PropTypes.array, PropTypes.object]).isRequired,
};