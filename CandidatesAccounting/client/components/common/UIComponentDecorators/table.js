import React from 'react'
import PropTypes from 'prop-types'
import { EmptyTable } from '../styledComponents'
import Paper from 'material-ui/Paper'
import Table, {
  TableBody,
  TableHead,
  TableRow,
  TableCell,
  TableFooter
} from 'material-ui/Table'

export default function CustomTable(props) {
  const headers = props.headers.map((header, index) =>
    <TableCell key={'th' + index}>{ header }</TableCell>)

  let rows = props.rows.map((row, index) =>
    <TableRow key={'tr' + index}>
      {row.cells.map((cell, cellIndex) =>
        <TableCell key={'td' + cellIndex} classes={{root: row.isDisabled ? 'disabled-cell' : ''}}>{cell}</TableCell>)}
    </TableRow>
  )
  if (rows.size === 0 || rows.length === 0) {
    rows = <TableRow><TableCell><EmptyTable>The table is empty</EmptyTable></TableCell></TableRow>
  }

  return (
    <Paper style={{ width: '100%', overflowX: 'auto' }}>
      <Table >
        <TableHead>
          <TableRow>
            {headers}
          </TableRow>
        </TableHead>
        <TableBody>
          {rows}
        </TableBody>
        <TableFooter>
          <TableRow>
            <TableCell colSpan={props.headers.length}>
              {props.footerActions}
            </TableCell>
          </TableRow>
        </TableFooter>
      </Table>
    </Paper>
  )

}

CustomTable.propTypes = {
  headers: PropTypes.oneOfType([PropTypes.array, PropTypes.object]).isRequired,
  rows: PropTypes.oneOfType([PropTypes.array, PropTypes.object]).isRequired,
  footerActions: PropTypes.object.isRequired,
}