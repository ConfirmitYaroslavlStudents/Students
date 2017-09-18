import React from 'react';
import Table, { TableBody, TableCell, TableHead, TableRow } from 'material-ui/Table';
import Paper from 'material-ui/Paper';

export default class BasicTable extends React.Component {
  render() {
    const contentRows = this.props.contentRows.map((row, index) =>
      <TableRow key={'tr' + index}>
        {
          row.map((cell, cellIndex) =>
            <TableCell key={'td' + cellIndex}>{cell}</TableCell>
          )}
      </TableRow>
    );
    return (
      <Paper style={
        {
          width: '100%',
          overflowX: 'auto',
        }
      }>
        <Table>
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
  heads: React.PropTypes.array,
  contentRows: React.PropTypes.array,
};