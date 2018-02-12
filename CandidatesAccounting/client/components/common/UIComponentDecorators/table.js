import React from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import Table, {
  TableBody,
  TableHead,
  TableRow,
  TableCell,
  TableFooter
} from 'material-ui/Table';
import Paper from 'material-ui/Paper';

export default function CustomTable(props) {
  let heads = props.heads.map((head, index) => <TableCell key={'th' + index}>{head}</TableCell>);
  let contentRows = props.rows.map((row, index) =>
    <TableRow key={'tr' + index}>
      {row.map((cell, cellIndex) => <TableCell key={'td' + cellIndex}>{cell.content}</TableCell>)}
    </TableRow>
  );
  if (contentRows.size === 0 || contentRows.length === 0) {
    contentRows = (<TableRow><TableCell><EmptyTable>The table is empty</EmptyTable></TableCell></TableRow>);
  }
  return (
    <Paper style={
      {
        width: '100%',
        overflowX: 'auto',
      }
    }>
      <Table style={{minWidth: 1240}}>
        <TableHead>
          <TableRow>
            {heads}
          </TableRow>
        </TableHead>
        <TableBody>
          {contentRows}
        </TableBody>
        <TableFooter>
          <TableRow>
            <TableCell colSpan={props.heads.length}>
              {props.footerActions}
            </TableCell>
          </TableRow>
        </TableFooter>
      </Table>
    </Paper>
  );

}

CustomTable.propTypes = {
  heads: PropTypes.oneOfType([PropTypes.array, PropTypes.object]).isRequired,
  rows: PropTypes.oneOfType([PropTypes.array, PropTypes.object]).isRequired,
  footerActions: PropTypes.object.isRequired,
};

const EmptyTable = styled.div`
  color: #aaa;
  text-align: center;
  position: absolute;
  width: 100%;
  margin-top: -12px;
`;