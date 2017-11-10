import React from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import Table, { TableBody, TableCell, TableHead, TableRow} from 'material-ui/Table';
import Paper from 'material-ui/Paper';

export default class BasicTable extends React.Component {
  render() {
    let contentRows = this.props.contentRows.map((row, index) =>
      <TableRow key={'tr' + index}>
        {
          row.map((cell, cellIndex) =>
            <TableCell key={'td' + cellIndex}>{cell}</TableCell>
          )}
      </TableRow>
    );
    if (contentRows.size === 0) {
      contentRows = (<TableRow><TableCell><EmptyTable>The table is empty</EmptyTable></TableCell></TableRow>);
    }
    return (
      <Paper style={
        {
          width: '100%',
          overflowX: 'auto',
        }
      }>
        <Table style={{minWidth: 1600}}>
          <TableHead>
            <TableRow className="table-head">
              {
                this.props.heads.map((head, index) =>
                  <TableCell key={'th' + index}>{head}</TableCell>
              )}
            </TableRow>
          </TableHead>
          <TableBody>
            {contentRows}
          </TableBody>
        </Table>
      </Paper>
    );
  }
}

BasicTable.propTypes = {
  heads: PropTypes.array,
  contentRows: PropTypes.oneOfType([PropTypes.array, PropTypes.object]),
};

const EmptyTable = styled.div`
  color: #aaa;
  text-align: center;
  position: absolute;
  width: 100%;
  margin-top: -12px;
`;